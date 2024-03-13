using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Price_Mode.GetPriceChangeForPriceModeByPriceModeItemId;

namespace RDF.Arcana.API.Features.Price_Mode;

[Route("api/price-change"), ApiController]

public class ExportPriceChange : ControllerBase
{
    private readonly IMediator _mediator;

    public ExportPriceChange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] ExportPriceChangeQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch(Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    public class ExportPriceChangeQuery : IRequest<Result>
    {
        public DateTime EffectivityDate { get; set; }
    }

    public class Handler : IRequestHandler<ExportPriceChangeQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ExportPriceChangeQuery request, CancellationToken cancellationToken)
        {

            var priceChanges = await _context.PriceModeItems
                .Include(x => x.ItemPriceChanges)
                .Select(priceChanges => new
                {
                    priceChanges.Id,
                    priceChanges.ItemId,
                    CurrentPrice = priceChanges.ItemPriceChanges
                        .FirstOrDefault(pc => pc.EffectivityDate == request.EffectivityDate)
                    //.Select(pc => new PriceChandesResult.PriceChangeHistory
                    //{
                    //    Id = pc.Id,
                    //    Price = pc.Price,
                    //    EffectivityDate = pc.EffectivityDate.ToString("MM/dd/yyyy HH:mm:ss")
                    //}),
                }).ToListAsync();

            return Result.Success(priceChanges);
        }
    }
}
