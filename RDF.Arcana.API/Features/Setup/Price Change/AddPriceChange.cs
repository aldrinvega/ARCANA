using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Items;

namespace RDF.Arcana.API.Features.Setup.Price_Change;
[Route("api/PriceChange"), ApiController]

public class AddPriceChange : ControllerBase
{
    private readonly IMediator _mediator;

    public AddPriceChange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewPriceChange")]
    public async Task<IActionResult> Add([FromBody]AddPriceChangeCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class AddPriceChangeCommand : IRequest<Result>
    {
            public int PriceModeItemId { get; set; }
            public decimal Price { get; set; }
            public DateTime EffectivityDate { get; set; }
        
    }

    public class Handler : IRequestHandler<AddPriceChangeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddPriceChangeCommand request, CancellationToken cancellationToken)
        { 
                var validateItem = await _context.PriceModeItems
                .Include(x => x.Item).FirstOrDefaultAsync(pmitem =>
                pmitem.Id == request.PriceModeItemId, cancellationToken);

                if (validateItem is null)
                {
                    return ItemErrors.NotFound(request.PriceModeItemId);
                }

                // Check if the latest recorded price change before the specified EffectivityDate has the same price
                var previousPriceChange = await _context.ItemPriceChanges
                    .Where(pc => pc.PriceModeItemId == request.PriceModeItemId && pc.EffectivityDate < request.EffectivityDate)
                    .OrderByDescending(pc => pc.EffectivityDate)
                    .FirstOrDefaultAsync(cancellationToken);

                // Check if the latest recorded price change after the specified EffectivityDate has the same price
                var nextPriceChange = await _context.ItemPriceChanges
                    .Where(pc => pc.PriceModeItemId == request.PriceModeItemId && pc.EffectivityDate > request.EffectivityDate)
                    .OrderBy(pc => pc.EffectivityDate)
                    .FirstOrDefaultAsync(cancellationToken);

                if ((previousPriceChange != null && previousPriceChange.Price == request.Price) ||
                    (nextPriceChange != null && nextPriceChange.Price == request.Price))
                {
                    // Return an error result indicating that the new price is the same as the adjacent recorded price
                    return PriceChangeErrors.PriceAlreadyAdded(validateItem.Item.ItemCode);
                }

                
                // Check if there's an existing price change with the same effectivity date
                var existingPriceChange = await _context.ItemPriceChanges
                    .Where(pc => pc.PriceModeItemId == request.PriceModeItemId && pc.EffectivityDate == request.EffectivityDate)
                    .FirstOrDefaultAsync(cancellationToken);

            if (existingPriceChange != null)
            {
                // Update the existing price change instead of adding a new one
                existingPriceChange.Price = request.Price;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // Add new price change
                var newPriceChange = new ItemPriceChange
                {
                    Price = request.Price,
                    PriceModeItemId = request.PriceModeItemId,
                    EffectivityDate = request.EffectivityDate
                };
                await _context.AddAsync(newPriceChange, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Result.Success();
        }
    }
}