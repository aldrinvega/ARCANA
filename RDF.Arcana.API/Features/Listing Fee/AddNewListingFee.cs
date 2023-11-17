using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;
using RDF.Arcana.API.Features.Listing_Fee.Errors;

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

    public class AddNewListingFeeCommand : IRequest<Result<Unit>>
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

    public class Handler : IRequestHandler<AddNewListingFeeCommand, Result<Unit>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(AddNewListingFeeCommand request, CancellationToken cancellationToken)
        {
            if (!await _context.Clients.AnyAsync(client => client.Id == request.ClientId, cancellationToken))
            {
                return Result<Unit>.Failure(ClientErrors.NotFound());
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
                    return Result<Unit>.Failure(ListingFeeErrors.AlreadyRequested(existingRequest.Item.ItemDescription));
                }
            }

            var approval = new Approvals
            {
                ClientId = request.ClientId,
                ApprovalType = Status.ForListingFeeApproval,
                IsActive = true,
                RequestedBy = request.RequestedBy,
            };

            await _context.Approvals.AddAsync(approval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var listingFee = new ListingFee
            {
                ClientId = request.ClientId,
                ApprovalsId = approval.Id,
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
            return Result<Unit>.Success(Unit.Value, "Listing Fee requested successfully");
        }
    }
}