using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

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
        try 
        {
            var query = new GetListingFeeBalanceByClientIdQuery
            {
                ClientId = id
            };

            var result = await _mediator.Send(query);

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

    public class GetListingFeeBalanceByClientIdQuery : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class GetListingFeeBalanceByClientQueryResult
    {
        public string BusinessName { get; set; }
        public decimal TotalBalance { get; set; }
        public IEnumerable<ListingFee> ListingFees { get; set; }
        public class ListingFee
        {
            public DateTime CreatedAt { get; set; }
            public DateTime ApprovalDate { get; set; }
            public string RequestedByFullname { get; set; }
            public decimal Total { get; set; }
        }
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
                .Include(u => u.RequestedByUser)
                .Where(lf => lf.ClientId == request.ClientId &&
                             lf.Status == Status.Approved)
                .ToListAsync();

            var listingFeeResults = listingFees.Select(lf => new GetListingFeeBalanceByClientQueryResult.ListingFee
            {
                CreatedAt = lf.CratedAt,
                ApprovalDate = lf.ApprovalDate,
                RequestedByFullname = lf.RequestedByUser.Fullname,
                Total = lf.Total
            }).ToList();            

            var result = new GetListingFeeBalanceByClientQueryResult
            {
                BusinessName = listingFees.First().Client.BusinessName,
                TotalBalance = listingFeeResults.Sum(lf => lf.Total),
                ListingFees = listingFeeResults
            };

            return Result.Success(result);
        }
    }
}
