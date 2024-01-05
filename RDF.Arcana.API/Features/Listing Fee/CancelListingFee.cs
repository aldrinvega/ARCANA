using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Listing_Fee.Errors;

namespace RDF.Arcana.API.Features.Listing_Fee;
[Route("api/ListingFee"), ApiController]

public class CancelListingFee : ControllerBase
{

    private readonly IMediator _mediator;

    public CancelListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var command = new CancelListingFeeCommand
            {
                ListingFeeId = id
            };

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

    public class CancelListingFeeCommand : IRequest<Result>
    {
        public int ListingFeeId { get; set; }
    }
    public class Handler : IRequestHandler<CancelListingFeeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public  async Task<Result> Handle(CancelListingFeeCommand request, CancellationToken cancellationToken)
        {
            var availableListingFee = await _context.ListingFees
                .Include(x => x.ListingFeeItems)
                .Include(x => x.Request)
                .ThenInclude(x => x.Approvals)
                .Include(x => x.Request)
                .FirstOrDefaultAsync(x => x.Id == request.ListingFeeId, cancellationToken);

            if (availableListingFee is null)
            {
                return ListingFeeErrors.NotFound();
            }
            _context.ListingFees.Remove(availableListingFee);

            foreach (var listingFee in availableListingFee.ListingFeeItems)
            {
                _context.ListingFeeItems.Remove(listingFee);
            }

            foreach (var approval in availableListingFee.Request.Approvals)
            {
                _context.Approval.Remove(approval);
            }
            
            _context.Requests.Remove(availableListingFee.Request);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}