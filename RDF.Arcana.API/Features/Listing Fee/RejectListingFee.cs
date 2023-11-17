using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Listing_Fee.Errors;

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
    public async Task<IActionResult> RejectListingFees([FromRoute] int id, [FromBody] RejectListingFeeCommand command)
    {
        try
        {
            command.ApprovalId = id;

            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.RejectedBy = userId;
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

    public class RejectListingFeeCommand : IRequest<Result<Unit>>
    {
        public int ApprovalId { get; set; }
        public int RejectedBy { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectListingFeeCommand, Result<Unit>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(RejectListingFeeCommand request, CancellationToken cancellationToken)
        {
            var existingApprovalsForListingFee = await _context.Approvals
                .Include(listingFee => listingFee.ListingFee)
                .ThenInclude(listingFeeItems => listingFeeItems.ListingFeeItems)
                .ThenInclude(X => X.Item)
                .ThenInclude(X => X.Uom)
                .FirstOrDefaultAsync(approval => approval.Id == request.ApprovalId, cancellationToken);

            if (existingApprovalsForListingFee == null)
            {
                return Result<Unit>.Failure(ListingFeeErrors.NotFound()); 
            }
            foreach (var listingFee in existingApprovalsForListingFee.ListingFee)
            {
                listingFee.Status = Status.Rejected;
            }
            
            existingApprovalsForListingFee.Reason = request.Reason;
            existingApprovalsForListingFee.IsApproved = false;

            await _context.SaveChangesAsync(cancellationToken);
            return Result<Unit>.Success(Unit.Value, "Listing fee rejected successfully");
        }
    }
}