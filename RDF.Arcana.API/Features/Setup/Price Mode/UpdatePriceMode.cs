using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using SQLitePCL;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Setup.Price_Mode
{
    [Route("api/price-mode"), ApiController]
    public class UpdatePriceMode : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdatePriceMode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}/information")]
        public async Task<IActionResult> Update(UpdatePriceModeCommand command, [FromRoute] int  id)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.ModifiedBy = userId;
                }

                command.Id = id;

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
        public class UpdatePriceModeCommand : IRequest<Result>
        {
            public int Id { get; set; }
            public string PriceModeDescription { get; set; }
            public int ModifiedBy { get; set; }
        }
        public class Hadnler : IRequestHandler<UpdatePriceModeCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Hadnler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdatePriceModeCommand request, CancellationToken cancellationToken)
            {
                var existingPriceMode = await _context.PriceMode.FirstOrDefaultAsync(pm => pm.Id == request.Id, cancellationToken); 

                if (existingPriceMode is null) 
                {
                    return PriceModeErrors.NotFound();
                }

                if (await _context.PriceMode.AnyAsync(pm => pm.Id != request.Id && pm.PriceModeDescription == request.PriceModeDescription, cancellationToken))
                {
                    return PriceModeErrors.DescriptionAlreadyExisit(request.PriceModeDescription);
                }

                existingPriceMode.UpdatedAt = DateTime.Now;
                existingPriceMode.PriceModeDescription = request.PriceModeDescription;
                existingPriceMode.ModifiedBy = request.ModifiedBy;

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
