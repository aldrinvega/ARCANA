using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UOM.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/[controller]")]
[ApiController]

public class UpdateUom : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUom(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateUomCommand : IRequest<Unit>
    {
        public int UomId { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateUomCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUomCommand request, CancellationToken cancellationToken)
        {
            var existingUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);

            if (existingUom is null)
            {
                throw new UomNotFoundException();
            }

            var isUomAlreadyExist = await _context.Uoms.AnyAsync(x => x.Id != request.UomId && x.UomCode == request.UomCode, cancellationToken);

            if (isUomAlreadyExist)
            {
                throw new UomAlreadyExistException();
            }

            if (existingUom.UomCode == request.UomCode && existingUom.UomDescription == request.UomDescription)
            {
                throw new Exception("No changes");
            }

            existingUom.UomCode = request.UomCode;
            existingUom.UomDescription = request.UomDescription;
            existingUom.ModifiedBy = request.ModifiedBy;
            existingUom.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateUom/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateUomCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UomId = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("UOM has been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}