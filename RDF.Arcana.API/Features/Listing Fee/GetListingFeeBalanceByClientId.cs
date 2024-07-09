using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
using static RDF.Arcana.API.Features.Client.All.GetClientOtherExpensesById;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/listing-fee"), ApiController]
public class GetListingFeeBalanceByClientId : ControllerBase
{
    private readonly IMediator _mediator;
    public GetListingFeeBalanceByClientId(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        
            var query = new GetClientOtherExpensesByIdQuery
            {
                ClientId = id
            };

            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        
        //catch (Exception ex)
        //{
        //    return BadRequest(ex.Message);
        //}
    }

    public class GetListingFeeBalanceByClientIdQuery : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class GetListingFeeBalanceByClientQueryResult
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public class Handler : IRequestHandler<GetListingFeeBalanceByClientIdQuery, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetListingFeeBalanceByClientIdQuery request, CancellationToken cancellationToken)
        {
            var listingFees = await _context.ListingFees
                .Include(c => c.Client)
                .Where(lf => lf.ClientId == request.ClientId && 
                             lf.Status == Status.Approved)
                .ToListAsync();

            if (!listingFees.Any())
            {
                return ListingFeeErrors.NotFound();
            }

            var result = listingFees.Select(x => new GetListingFeeBalanceByClientQueryResult
            {
                ClientId = request.ClientId,
                FullName = x.Client.Fullname,
                RemainingBalance = x.Total
            });

            return Result.Success("testdev");
        }
    }
}
