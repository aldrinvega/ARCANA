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

    public class UpdateUomStatusCommand : IRequest<bool>
    {
        public int UomId { get; set; }
        public string ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateUomStatusCommand, bool>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUomStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUom = await _context.Uoms.FirstOrDefaultAsync(x => x.Id == request.UomId, cancellationToken);

            if (existingUom is null)
            {
                throw new UomNotFoundException();
            }

            existingUom.IsActive = !existingUom.IsActive;
            existingUom.ModifiedBy = request.ModifiedBy;
            existingUom.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
    
    [HttpPatch("UpdateUomStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateUomStatusCommand
            {
                UomId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("UOM status haas been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Ok(response);
        }
    }
}