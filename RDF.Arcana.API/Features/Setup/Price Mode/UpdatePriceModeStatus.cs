using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Price_Mode
{
    [Route("api/price-mode"), ApiController]
    public class UpdatePriceModeStatus : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdatePriceModeStatus(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id)
        {
            try
            {
                var command = new UpdatePriceModeStatusCommand
                {
                    Id = id
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

        public class UpdatePriceModeStatusCommand : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<UpdatePriceModeStatusCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdatePriceModeStatusCommand request, CancellationToken cancellationToken)
            {
                var priceMode = await _context.PriceMode
                    .Include(cl => cl.Clients)
                    .FirstOrDefaultAsync(pm => pm.Id == request.Id, cancellationToken);

                if(priceMode is null)
                {
                    return PriceModeErrors.NotFound();
                }

                if(priceMode.Clients.Any(cl => cl.PriceModeId == request.Id))
                {
                    return PriceModeErrors.InUse();
                }

                priceMode.IsActive = !priceMode.IsActive;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
