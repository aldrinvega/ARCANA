using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

[Route("api/[controller]")]
[ApiController]

public class UpdateMeatTypeStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateMeatTypeStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateMeatTypeStatusCommand : IRequest<Unit>
    {
        public int MeatTypeId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateMeatTypeStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMeatTypeStatusCommand request, CancellationToken cancellationToken)
        {
            var existingMeatType =
                await _context.MeatTypes.FirstOrDefaultAsync(x => x.Id == request.MeatTypeId, cancellationToken);

            if (existingMeatType is null)
            {
                throw new MeatTypeNotFoundException();
            }

            existingMeatType.IsActive = !existingMeatType.IsActive;
            existingMeatType.UpdatedAt = DateTime.Now;
            existingMeatType.ModifiedBy = request.ModifiedBy ?? "Admin";
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateMeatTypeStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateMeatTypeStatusCommand
            {
                MeatTypeId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Meat type status updated successfully.");
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(ex.Message);
            return Conflict(response);
        }
    }
}