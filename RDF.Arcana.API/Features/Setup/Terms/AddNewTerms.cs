using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Term_Days;
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

    public class AddNewTermsCommand : IRequest<Result>
    {
        public string TermType { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewTermsCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewTermsCommand request, CancellationToken cancellationToken)
        {
            var validateTerm =
                await _context.Terms.FirstOrDefaultAsync(x => x.TermType == request.TermType, cancellationToken);

            if (validateTerm != null)
            {
                return TermErrors.AlreadyExist(request.TermType);
            }

            var terms = new Domain.Terms
            {
                TermType = request.TermType,
                AddedBy = request.AddedBy
            };

            await _context.Terms.AddAsync(terms, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}