using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/Uom")]
[ApiController]
public class UpdateUom : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUom(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateUom/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateUomCommand command)
    {
        try
        {
            command.UomId = id;
            command.ModifiedBy = User.Identity?.Name;
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

    public class UpdateUomCommand : IRequest<Result>
    {
        public int UomId { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUomCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateUomCommand request, CancellationToken cancellationToken)
        {
            var existingUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);

            if (existingUom is null)
            {
                return UomErrors.NotFound();
            }

            var isUomAlreadyExist =
                await _context.Uoms.AnyAsync(x => x.Id != request.UomId && x.UomCode == request.UomCode,
                    cancellationToken);

            if (isUomAlreadyExist)
            {
                return UomErrors.AlreadyExist(request.UomCode);
            }
            
            existingUom.UomCode = request.UomCode;
            existingUom.UomDescription = request.UomDescription;
            existingUom.ModifiedBy = request.ModifiedBy;
            existingUom.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}