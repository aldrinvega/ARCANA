using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class RejectListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectListingFee/{id:int}")]
    public async Task<IActionResult> RejectListingFees([FromRoute] int id, [FromQuery] int listingFeeId)
    {
        try
        {
            var command = new RejectListingFeeCommand
            {
                ClientId = id,
                ListingFeeId = listingFeeId
            };

            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.RejectedBy = userId;
            }

            await _mediator.Send(command);
            return Ok();
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class RejectListingFeeCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int ListingFeeId { get; set; }
        public int RejectedBy { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectListingFeeCommand, Unit>
    {
        private const string FOR_LISTING_FEE_APPROVAL = "For listing fee approval";
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RejectListingFeeCommand request, CancellationToken cancellationToken)
        {
            var existingListingFee = await _context.Approvals
                .Include(x => x.ListingFee)
                .ThenInclude(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(
                    x => x.IsActive &&
                         x.ClientId == request.ClientId &&
                         x.IsApproved == false &&
                         x.ApprovalType == FOR_LISTING_FEE_APPROVAL, cancellationToken);

            if (existingListingFee is null)
            {
                throw new System.Exception("No Listing Fee found");
            }

            // Locate the specific FreebieRequest to be Rejected
            var listingFeeToReject = existingListingFee.ListingFee
                .FirstOrDefault(lf => lf.Id == request.ListingFeeId);

            if (listingFeeToReject == null)
            {
                throw new System.Exception("Freebie Request not found");
            }

            // Reject the specific FreebieRequest
            listingFeeToReject.Status = "Rejected";
            existingListingFee.Reason = request.Reason;
            existingListingFee.IsApproved = false;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}