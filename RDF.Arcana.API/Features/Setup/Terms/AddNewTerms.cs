using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Terms.Exceptions;

namespace RDF.Arcana.API.Features.Setup.Terms;

[Route("api/Terms")]
[ApiController]
public class AddNewTerms : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewTerms(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewTerm")]
    public async Task<IActionResult> AddNewTerm([FromBody] AddNewTermsCommand command)
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
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add($"{command.TermType} is added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class AddNewTermsCommand : IRequest<Unit>
    {
        public string TermType { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewTermsCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewTermsCommand request, CancellationToken cancellationToken)
        {
            var validateTerm =
                await _context.Terms.FirstOrDefaultAsync(x => x.TermType == request.TermType, cancellationToken);

            if (validateTerm != null)
            {
                throw new TermAlreadyExistException(request.TermType);
            }

            var terms = new Domain.Terms
            {
                TermType = request.TermType,
                AddedBy = request.AddedBy
            };

            await _context.Terms.AddAsync(terms, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}