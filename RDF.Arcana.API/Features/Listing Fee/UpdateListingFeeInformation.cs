using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
using RDF.Arcana.API.Features.Listing_Fee.Exception;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class UpdateListingFeeInformation : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<UpdateListingFeeInformationCommand> _validator;

    public UpdateListingFeeInformation(IMediator mediator, IValidator<UpdateListingFeeInformationCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPut("UpdateListingFeeItems/{id:int}")]
    public async Task<IActionResult> UpdateListingFeeItems([FromBody] UpdateListingFeeInformationCommand command,
        [FromRoute] int id, [FromQuery] int listingFeeId)
    {
        try
        {
            var validate = await _validator.ValidateAsync(command);

            if (!validate.IsValid)
            {
                return BadRequest(validate);
            }
            command.ClientId = id;
            command.ListingFeeId = listingFeeId;
           var result =  await _mediator.Send(command);
           
            return Ok(result);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class UpdateListingFeeInformationCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public int ListingFeeId { get; set; }
        public decimal Total { get; set; }
        public List<ListingItem> ListingItems { get; set; }

        public class ListingItem
        {
            public int ItemId { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateListingFeeInformationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateListingFeeInformationCommand command, CancellationToken cancellationToken)
        {
            var listingFee = await _context.ListingFees
                .Include(x => x.ListingFeeItems)
                .Where(lf => lf.ClientId == command.ClientId && lf.Id == command.ListingFeeId)
                .Include(x => x.Request)
                .ThenInclude(x => x.Approvals)
                .Include(x => x.Request)
                .ThenInclude(x => x.UpdateRequestTrails)
                .FirstOrDefaultAsync(cancellationToken);

            if (listingFee == null)
            {
                return ListingFeeErrors.NotFound();
            }

            var listingFeeItems = command.ListingItems.Select(x => x.ItemId).ToList();
            var existingItemList = listingFee.ListingFeeItems.Select(x => x.ItemId).ToList();
            var toRemove = existingItemList.Except(listingFeeItems);

            foreach (var itemId in toRemove)
            {
                var forRemove = listingFee.ListingFeeItems.First(i => i.ItemId == itemId);
                listingFee.ListingFeeItems.Remove(forRemove);
            }

            // Check if there are no listing fee items left, and delete the request if true
            if (!listingFee.ListingFeeItems.Any())
            {
                // Remove update trails
                _context.UpdateRequestTrails.RemoveRange(listingFee.Request.UpdateRequestTrails);

                // Remove the listing fee entity
                _context.ListingFees.Remove(listingFee);

                // Remove associated approvals
                _context.Approval.RemoveRange(listingFee.Request.Approvals);

                _context.Requests.Remove(listingFee.Request);

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }

            foreach (var item in command.ListingItems)
            {
                var listingFeeItemToAdd = listingFee.ListingFeeItems.FirstOrDefault(x => x.ItemId == item.ItemId);

                if (listingFeeItemToAdd != null)
                {
                    listingFeeItemToAdd.Sku = item.Sku;
                    listingFeeItemToAdd.UnitCost = item.UnitCost;
                    listingFeeItemToAdd.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    listingFee.ListingFeeItems.Add(new ListingFeeItems
                    {
                        ListingFeeId = command.ListingFeeId,
                        ItemId = item.ItemId,
                        UnitCost = item.UnitCost,
                        Sku = item.Sku
                    });
                }
            }

            listingFee.Status = Status.UnderReview;
            listingFee.Total = command.Total;

            // Determine the appropriate approvers based on the new Total using ApproverByRange entity
            var applicableApprovers = await _context.ApproverByRange
                .Where(ar => ar.ModuleName == Modules.ListingFeeApproval && ar.IsActive && Math.Ceiling(command.Total) >= ar.MinValue)
                .OrderBy(ar => ar.Level)
                .ToListAsync(cancellationToken);

            if (!applicableApprovers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.ListingFeeApproval);
            }

            // Identify the levels of approvers
            var maxLevelApprover = applicableApprovers.OrderByDescending(a => a.Level).First();
            var approverLevels = applicableApprovers
                .Where(a => a.Level <= maxLevelApprover.Level)
                .OrderBy(a => a.Level).ToList();

            // Create or update RequestApprovers for all levels
            var existingRequestApprovers = await _context.RequestApprovers
                .Where(x => x.RequestId == listingFee.RequestId)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            foreach (var requestApprover in existingRequestApprovers)
            {
                _context.RequestApprovers.Remove(requestApprover);
            }

            var newRequestApprovers = new List<RequestApprovers>();
            foreach (var approver in approverLevels)
            {
                var requestApprover = new RequestApprovers
                {
                    ApproverId = approver.UserId,
                    RequestId = listingFee.RequestId,
                    Level = approver.Level
                };

                newRequestApprovers.Add(requestApprover);

                var notificationForApprover = new Domain.Notification
                {
                    UserId = approver.UserId,
                    Status = Status.PendingListingFee
                };

                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            }

            _context.RequestApprovers.AddRange(newRequestApprovers);

            listingFee.Request.Status = Status.UnderReview;
            listingFee.Request.CurrentApproverId = approverLevels.First().UserId;

            var newUpdateHistory = new UpdateRequestTrail(
                listingFee.RequestId,
                Modules.ListingFeeApproval,
                DateTime.Now,
                listingFee.RequestedBy);

            await _context.UpdateRequestTrails.AddAsync(newUpdateHistory, cancellationToken);

            var notification = new Domain.Notification
            {
                UserId = listingFee.RequestedBy,
                Status = Status.PendingListingFee
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            // Return result with all approvers
            var result = new
            {
                approvalHistories = newRequestApprovers.Select(a => new
                {
                    name = a.ApproverId, // Assuming User has a Fullname property
                    level = a.Level
                }).ToList(),
                updateHistories = listingFee.Request.UpdateRequestTrails.Select(trail => new
                {
                    module = trail.ModuleName,
                    updatedAt = trail.UpdatedAt
                }).ToList(),
                approvers = newRequestApprovers.Select(a => new
                {
                    name = a.ApproverId, // Assuming User has a Fullname property
                    level = a.Level
                }).ToList()
            };

            return Result.Success(result);
        }
    }

}