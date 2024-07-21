using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class ApproveListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ApproveListingFee/{id:int}")]
    public async Task<IActionResult> ApproveListingFeeRequest([FromRoute] int id)
    {
        try
        {
            var command = new ApproveListingFeeCommand
            {
                RequestId = id
            };
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.ApprovedBy = userId;
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class ApproveListingFeeCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class ApprovedListingFeeResult
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public IEnumerable<ListingFeeItem> ListingFeeItems { get; set; }

        public class ListingFeeItem
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public decimal Total { get; set; }
        }
    }

    public class Handler : IRequestHandler<ApproveListingFeeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ApproveListingFeeCommand request,
            CancellationToken cancellationToken)
        {

            var listingFees = await _context.Requests
                .Include(listing => listing.ListingFee)
                .Where(lf => lf.Id == request.RequestId)
                .FirstOrDefaultAsync(cancellationToken);

            var approvers = await _context.RequestApprovers
                .Where(module => module.RequestId == request.RequestId)
                .ToListAsync(cancellationToken);
            var currentApproverLevel = approvers
                .FirstOrDefault(approver => 
                    approver.ApproverId == listingFees.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.ListingFeeApproval);
            }

            if (listingFees.CurrentApproverId != request.ApprovedBy)
            {
                return ApprovalErrors.NotAllowed(Modules.ListingFeeApproval);
            }

            var newApproval = new Approval(
                listingFees.Id,
                listingFees.CurrentApproverId,
                Status.Approved,
                null,
                true
            );
            
            var nextLevel = currentApproverLevel.Value + 1;
            var nextApprover = approvers
                .FirstOrDefault(approver => approver.Level == nextLevel);

            var suceedingApprover = approvers.FirstOrDefault(ap => ap.Level == nextLevel + 1);

            if (suceedingApprover == null)
            {
                listingFees.NextApproverId = null;
            }

            if (nextApprover == null)
            {
                listingFees.Status = Status.Approved;
                listingFees.ListingFee.Status = Status.Approved;
                listingFees.ListingFee.ApprovalDate = DateTime.Now;
                listingFees.NextApproverId = null;
                
                var notificationForApprover = new Domain.Notification
                {
                    UserId = listingFees.CurrentApproverId,
                    Status = Status.ApprovedListingFee
                };
                
                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
                
                var notification = new Domain.Notification
                {
                    UserId = listingFees.RequestorId,
                    Status = Status.ApprovedListingFee
                };
                
                await _context.Notifications.AddAsync(notification, cancellationToken);
            }
            else
            {
                listingFees.CurrentApproverId = nextApprover.ApproverId;

                var notificationForNextApprover = new Domain.Notification
                {
                    UserId = nextApprover.ApproverId,
                    Status = Status.PendingListingFee
                };
                
                await _context.Notifications.AddAsync(notificationForNextApprover, cancellationToken);
            }
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}