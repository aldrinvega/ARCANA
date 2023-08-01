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

    public class AddNewTermDaysCommand : IRequest<Unit>
    {
        public int Days { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewTermDaysCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewTermDaysCommand request, CancellationToken cancellationToken)
        {
            var existingTermDays =
                await _context.TermDays.FirstOrDefaultAsync(x => x.Days == request.Days, cancellationToken);

            if (existingTermDays is not null)
            {
                throw new TermDaysAlreadyExist();
            }

            var termDays = new TermDays
            {
                Days = request.Days,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.TermDays.AddAsync(termDays, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewTermDays")]
    public async Task<IActionResult> Add(AddNewTermDaysCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Term day has been added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}