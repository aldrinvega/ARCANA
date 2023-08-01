using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;


namespace RDF.Arcana.API.Features.Setup.Term_Days;

[Route("api/TermDays")]
[ApiController]

public class UpdateTermDays : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateTermDays(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateTermDaysCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateTermDaysCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTermDaysCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingTermDays is null)
            {
                throw new TermDaysNotFoundException();
            }

            if (existingTermDays.Days == request.Days)
            {
                throw new Exception("No changes");
            }

            var isTermDaysAlreadyExist = await _context.TermDays
                .AnyAsync(x => x.Id != request.Id && x.Days == request.Days, cancellationToken);

            if (isTermDaysAlreadyExist)
            {
                throw new TermDaysAlreadyExist();
            }

            existingTermDays.Days = request.Days;
            existingTermDays.ModifiedBy = request.ModifiedBy;
            existingTermDays.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateTermDays/{id:int}")]
    public async Task<IActionResult> Update([FromBody]UpdateTermDaysCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Term days has been updated successfully");
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
