using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/price-mode-items")]
    public class DeletePriceModeItem : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeletePriceModeItem(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                var command = new DeletePriceModeItemCommand { PriceModeItemId = id };
                var result = await _mediator.Send(command);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }
                return Ok(result);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public class DeletePriceModeItemCommand : IRequest<Result>
        {
            public int PriceModeItemId { get; set; }
        }

        public class Handler : IRequestHandler<DeletePriceModeItemCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(DeletePriceModeItemCommand request, CancellationToken cancellationToken)
            {
                var priceModeItem = await _context.PriceModeItems
                    .Include(pc => pc.ItemPriceChanges)
                    .FirstOrDefaultAsync(pi => 
                    pi.Id == request.PriceModeItemId, 
                    cancellationToken: cancellationToken);

                if(priceModeItem == null)
                {
                    return PriceModeItemsErrors.NotFound();
                }

                // Remove ItemPriceChanges from context
                _context.ItemPriceChanges.RemoveRange(priceModeItem.ItemPriceChanges);

                _context.Remove(priceModeItem);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
