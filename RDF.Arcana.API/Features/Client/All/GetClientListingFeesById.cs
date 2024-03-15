using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.All
{
    [Route("api/listingfee"), ApiController]
    public class GetClientListingFeesById : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetClientListingFeesById(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = new GetClientListingFeesByIdQuery
                {
                    ClientId = id,
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        public class GetClientListingFeesByIdQuery : IRequest<Result>
        {
            public int ClientId { get; set; }
        }
        public class ClientListingFees
        {
            public IEnumerable<ClientListingFee> ListingFees { get; set; }
            public class ClientListingFee
            {
                public int Id { get; set; }
                public int RequestId { get; set; }
                public decimal Total { get; set; }
                public string Status { get; set; }
                public string ApprovalDate { get; set; }
                public IEnumerable<ListingItems> ListingItems { get; set; }
            }
            public class ListingItems
            {
                public int? Id { get; set; }
                public string ItemCode { get; set; }
                public int Sku { get; set; }
                public string ItemDescription { get; set; }
                public string Uom { get; set; }
                public decimal? UnitCost { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetClientListingFeesByIdQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetClientListingFeesByIdQuery request, CancellationToken cancellationToken)
            {
                var listingFees = await _context.ListingFees
                    .Include(lfi => lfi.ListingFeeItems)
                    .ThenInclude(i => i.Item)
                    .ThenInclude(u => u.Uom)
                    .Where(cl => cl.ClientId == request.ClientId)
                    .ToListAsync(cancellationToken: cancellationToken);

                var listingFee =  new ClientListingFees
                {
                    ListingFees =  listingFees.Select(lf => new ClientListingFees.ClientListingFee
                                    {
                                        Id = lf.Id,
                                        RequestId = lf.RequestId,
                                        Total = lf.Total,
                                        Status = lf.Status,
                                        ApprovalDate = lf.ApprovalDate.ToString("MM/dd/yyyy HH:mm:ss"),
                                        ListingItems = lf.ListingFeeItems.Select(lfi => new ClientListingFees.ListingItems
                                        {
                                            Id = lfi.Id,
                                            ItemCode = lfi.Item.ItemCode,
                                            ItemDescription = lfi.Item.ItemDescription,
                                            Sku = lfi.Sku,
                                            UnitCost = lfi.UnitCost,
                                            Uom = lfi.Item.Uom.UomCode
                                        })
                                    })
                };

                return Result.Success(listingFee);
            }
        }
    }
}