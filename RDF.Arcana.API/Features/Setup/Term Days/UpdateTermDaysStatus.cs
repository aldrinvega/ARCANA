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

    public class UpdateTermDaysStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateTermDaysStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateTermDaysStatusCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingTermDays is null)
            {
                return TermDaysErrors.NotFound();
            }

            existingTermDays.IsActive = !existingTermDays.IsActive;
            existingTermDays.UpdatedAt = DateTime.Now;
            existingTermDays.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateTermDaysStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateTermDaysStatusCommand
            {
                Id = id,
                ModifiedBy = User.Identity?.Name
            };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}