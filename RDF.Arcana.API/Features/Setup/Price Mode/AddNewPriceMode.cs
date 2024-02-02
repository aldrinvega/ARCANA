using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Setup.Price_Mode
{
    [Route("api/price-mode"), ApiController]
    public class AddNewPriceMode : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddNewPriceMode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddNewPriceModeCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AddedBy = userId;
                }
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

        public class AddNewPriceModeCommand : IRequest<Result>
        {
            public string PriceMode { get; set; }
            public string PriceModeDescription { get; set; }
            public int AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<AddNewPriceModeCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddNewPriceModeCommand request, CancellationToken cancellationToken)
            {
                var existingPriceMode = await _context.PriceMode
                     .FirstOrDefaultAsync(pm => pm.PriceModeCode == request.PriceMode);

                if (existingPriceMode != null)
                {
                    return PriceModeErrors.AlreadyExisit(request.PriceMode);
                }

                var priceMode = new PriceMode
                {
                    PriceModeCode = request.PriceMode,
                    PriceModeDescription = request.PriceModeDescription,
                    AddedBy = request.AddedBy
                };

                await _context.PriceMode.AddAsync(priceMode, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
