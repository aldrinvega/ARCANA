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

    public class UpdateTermDaysCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateTermDaysCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateTermDaysCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingTermDays is null)
            {
                throw new TermDaysNotFoundException();
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

            return Result.Success();
        }
    }
    
    [HttpPut("UpdateTermDays/{id:int}")]
    public async Task<IActionResult> Update([FromBody]UpdateTermDaysCommand command, [FromRoute] int id)
    {
        try
        {
            command.Id = id;
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
}
