using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class AddNewListingFee : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<AddNewListingFeeCommand> _validator;

    public AddNewListingFee(IMediator mediator, IValidator<AddNewListingFeeCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpPost("AddNewListingFee")] 
    public async Task<IActionResult> AddNewListingFeeRequest([FromBody] AddNewListingFeeCommand command)
    {
        try
        {
            var result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.RequestedBy = userId;
            }

            var response = await _mediator.Send(command);

            if (response.IsFailure)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class AddNewListingFeeCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public int RequestedBy { get; set; }
        public decimal Total { get; set; }
        public ICollection<ListingFeeItem> ListingItems { get; set; }

        public class ListingFeeItem
        {
            public int ItemId { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddNewListingFeeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewListingFeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Clients.AnyAsync(client => client.Id == request.ClientId, cancellationToken))
            {
                return ClientErrors.NotFound();
            }
            
            foreach (var item in request.ListingItems)
            {
                var existingRequest = await _context.ListingFeeItems
                    .Include(x => x.Item)
                    .Include(f => f.ListingFee)
                    .Where(f => f.ItemId == item.ItemId && f.ListingFee.ClientId == request.ClientId &&
                                f.ListingFee.Status != Status.Rejected)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingRequest != null)
                {
                    return ListingFeeErrors.AlreadyRequested(existingRequest.Item.ItemDescription);
                }
            }

            var approvers = await _context.Approvers
                .Where(x => x.ModuleName == Modules.RegistrationApproval)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);
                
            if (!approvers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
            }

            var newRequest = new Request(
                Modules.ListingFeeApproval,
                request.RequestedBy,
                approvers.First().UserId,
                Status.UnderReview
            );
                
            await _context.Requests.AddAsync(newRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
                     {
                         ApproverId = approver.UserId,
                         RequestId = newRequest.Id,
                         Level = approver.Level,
                     }))
            {
                _context.RequestApprovers.Add(newRequestApprover);
            }

            var listingFee = new ListingFee
            {
                ClientId = request.ClientId,
                RequestId = newRequest.Id,
                Status = Status.UnderReview,
                RequestedBy = request.RequestedBy,
                Total = request.Total
            };

            await _context.ListingFees.AddAsync(listingFee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var listingFeeItem in request.ListingItems.Select(items => new ListingFeeItems
                     {
                         ListingFeeId = listingFee.Id,
                         ItemId = items.ItemId,
                         Sku = items.Sku,
                         UnitCost = items.UnitCost
                     }))
            {
                await _context.ListingFeeItems.AddAsync(listingFeeItem, cancellationToken);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}