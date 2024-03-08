using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Relational;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/item-price-change")]
    public class GetPriceChangeForPriceModeByPriceModeItemId : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetPriceChangeForPriceModeByPriceModeItemId(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] GetPriceChangeForPriceModeByPriceModeItemIdQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);

                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetPriceChangeForPriceModeByPriceModeItemIdQuery : IRequest<Result>
        {
            public int PriceModeId { get; set; }
            public int ItemId { get; set; }
        }

        public class PriceChandesResult
        {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public IEnumerable<PriceChangeHistory> PriceChangeHistories { get; set; }
            public IEnumerable<FuturePriceChange> FuturePriceChanges { get; set; }

            public class PriceChangeHistory
            {
                public int Id { get; set; }
                public decimal Price { get; set; }
                public string EffectivityDate { get; set; }
            }

            public class FuturePriceChange
            {
                public int Id { get; set; }
                public decimal Price { get; set; }
                public string EffectivityDate { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetPriceChangeForPriceModeByPriceModeItemIdQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetPriceChangeForPriceModeByPriceModeItemIdQuery request, CancellationToken cancellationToken)
            {
                var now = DateTime.Now;
                var priceChanges = await _context.PriceModeItems
                    .Include(x => x.ItemPriceChanges)
                    .FirstOrDefaultAsync(pc => pc.PriceModeId == request.PriceModeId && pc.ItemId == request.ItemId, cancellationToken);

                var result = new PriceChandesResult
                {
                    Id = priceChanges.Id,
                    ItemId = priceChanges.ItemId,
                    PriceChangeHistories = priceChanges.ItemPriceChanges
                            .Where(pc => pc.EffectivityDate <= now)
                            .OrderByDescending(p => p.EffectivityDate)
                            .Select(pc => new PriceChandesResult.PriceChangeHistory
                            {
                                Id = pc.Id,
                                Price = pc.Price,
                                EffectivityDate = pc.EffectivityDate.ToString("MM/dd/yyyy HH:mm:ss")
                            }),
                    FuturePriceChanges = priceChanges.ItemPriceChanges
                            .Where(p => p.EffectivityDate > now)
                            .OrderBy(p => p.EffectivityDate)
                            .Select(pc => new PriceChandesResult.FuturePriceChange
                            {
                                Id = pc.Id,
                                Price = pc.Price,
                                EffectivityDate = pc.EffectivityDate.ToString("MM/dd/yyyy HH:mm:ss")
                            })
                };

                return Result.Success(result);
            }
        }
    }
}
