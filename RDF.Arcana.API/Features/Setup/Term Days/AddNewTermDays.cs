using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

[Route("api/TermDays")]
[ApiController]

public class AddNewTermDays : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewTermDays(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewTermDaysCommand : IRequest<Result>
    {
        public int Days { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewTermDaysCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewTermDaysCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Days == request.Days, cancellationToken);

            if (existingTermDays is not null)
            {
                return TermDaysErrors.AlreadyExist(request.Days);
            }

            var termDays = new TermDays
            {
                Days = request.Days,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.TermDays.AddAsync(termDays, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPost("AddNewTermDays")]
    public async Task<IActionResult> Add(AddNewTermDaysCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}