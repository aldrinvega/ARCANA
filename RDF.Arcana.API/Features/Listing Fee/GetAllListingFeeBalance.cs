
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Listing_Fee
{
    [Route("api/ListingFee"), ApiController]
    public class GetAllListingFeeBalance : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllListingFeeBalance(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllListingFeeBalance")]
        public async Task<IActionResult> GetAll([FromQuery] GetAllListingFeeTotalsQuery query)
        {            
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetAllListingFeeTotalsQuery : IRequest<Result> 
        {
            public string BusinessName { get; set; }
        }

        public class GetAllListingFeesTotalsResult
        {            
            public string BusinessName{ get; set; }
            public string FullName { get; set; }
            public decimal RemainingBalance { get; set; }
        }

        public class Handler : IRequestHandler<GetAllListingFeeTotalsQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllListingFeeTotalsQuery request, CancellationToken cancellationToken)
            {
                var listingFees = await _context.ListingFees
                    .Include(c => c.Client)
                    .Where(lf => lf.Status == Status.Approved &&
                                 lf.IsActive)
                    .GroupBy(lf => new { lf.Client.Fullname, lf.Client.BusinessName })
                    .Select(lf => new GetAllListingFeesTotalsResult 
                            {                                
                                BusinessName = lf.Key.BusinessName,
                                FullName = lf.Key.Fullname,
                                RemainingBalance = lf.Sum(lf => lf.Total)
                            })
                    .OrderBy(lf => lf.BusinessName)
                    .ToListAsync(cancellationToken);

                if (request.BusinessName is not null)
                {                   
                    listingFees = listingFees
                        .Where(lf => lf.BusinessName.ToLower().Contains(request.BusinessName.ToLower())) 
                        .ToList();
                }

                return Result.Success(listingFees);
            }
        }
    }
}
