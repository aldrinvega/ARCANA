using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/Uom")]
[ApiController]

public class UpdateUomStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUomStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateUomStatusCommand : IRequest<Result>
    {
        public int UomId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateUomStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateUomStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);

            if (existingUom is null)
            {
                return UomErrors.NotFound();
            }

            existingUom.IsActive = !existingUom.IsActive;
            existingUom.ModifiedBy = request.ModifiedBy;
            existingUom.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateUomStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateUomStatusCommand
            {
                UomId = id,
                ModifiedBy = User.Identity?.Name
            };
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
}