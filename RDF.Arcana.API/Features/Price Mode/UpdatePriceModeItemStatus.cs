using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/price-change-items")]
    public class UpdatePriceModeItemStatus : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdatePriceModeItemStatus(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> Archive(int id)
        {
            try
            {
                var command = new UpdatePriceModeItemStatusCommand
                {
                    PriceModeItemId = id
                };

                var result = await _mediator.Send(command);
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

        public class UpdatePriceModeItemStatusCommand : IRequest<Result>
        {
            public int PriceModeItemId { get; set; }
        }

        public class Handler : IRequestHandler<UpdatePriceModeItemStatusCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdatePriceModeItemStatusCommand request, CancellationToken cancellationToken)
            {
                var priceModeItems = await _context.PriceModeItems.FirstOrDefaultAsync(i => i.Id == request.PriceModeItemId, cancellationToken);

                if (priceModeItems == null)
                {
                    return PriceModeItemsErrors.NotFound();
                }

                priceModeItems.IsActive = !priceModeItems.IsActive;

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
