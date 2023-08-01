using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

[Route("api/TermDays")]
[ApiController]

public class UpdateTermDaysStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateTermDaysStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateTermDaysStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateTermDaysStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTermDaysStatusCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingTermDays is null)
            {
                throw new TermDaysNotFoundException();
            }

            existingTermDays.IsActive = !existingTermDays.IsActive;
            existingTermDays.UpdatedAt = DateTime.Now;
            existingTermDays.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateTermDaysStatus/{id:int}")]
    public async Task<IActionResult> Update([FromQuery] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateTermDaysStatusCommand
            {
                Id = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Messages.Add("Term Days status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
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