using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Listing_Fee.Errors;

namespace RDF.Arcana.API.Features.Listing_Fee;
[Route("api/ListingFee"), ApiController]

public class VoidListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public VoidListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("VoidListingFee/{id:int}")]
    public async Task<IActionResult> Void([FromRoute] int id)
    {
        try
        {
            var command = new VoidListingFeeCommand
            {
                ListingFeeId = id
            };
            
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

    public class VoidListingFeeCommand : IRequest<Result<Unit>>
    {
        public int ListingFeeId { get; set; }
        public int RejectedBy { get; set; }
    }
    public class Handler : IRequestHandler<VoidListingFeeCommand, Result<Unit>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(VoidListingFeeCommand request, CancellationToken cancellationToken)
        {
            var existingListingFee = await _context.ListingFees
                .Include(x => x.Approvals)
                .Where(approval => 
                    approval.Approvals.IsActive &&
                    approval.Approvals.IsApproved == false)
                .FirstOrDefaultAsync(x => x.Id == request.ListingFeeId, cancellationToken);

           
            if (existingListingFee == null)
            {
                return Result<Unit>.Failure(ListingFeeErrors.NotFound());
            }
            
            if (existingListingFee.Approvals.RequestedBy != request.RejectedBy)
            {
                return Result<Unit>.Failure(ListingFeeErrors.Unauthorized());
            }

            if (existingListingFee.Status == Status.Voided &&
                !existingListingFee.IsActive)
            {
                return Result<Unit>.Failure(ListingFeeErrors.AlreadyVoided());
            }

            existingListingFee.Status = Status.Voided;
            existingListingFee.IsActive = false;
            existingListingFee.Approvals.IsActive = false;
            existingListingFee.Approvals.IsApproved = false;
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result<Unit>.Success(Unit.Value, "Listing fee voided successfully");
        }
    }
}