using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.Prospecting;

[Route("api/Prospecting"), ApiController]

public class VoidProspectingRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public VoidProspectingRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("VoidProspectingRequest/{id:int}")]
    public async Task<IActionResult> Void([FromRoute] int id)
    {
        var command = new VoidProspectingRequestCommand
        {
            ClientId = id
        };

        var result = await _mediator.Send(command);

        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    public class VoidProspectingRequestCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
    }
    
    public class Handler : IRequestHandler<VoidProspectingRequestCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(VoidProspectingRequestCommand request, CancellationToken cancellationToken)
        {
            var validateClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

            if (validateClient is null)
            {
                return ClientErrors.NotFound();
            }

            validateClient.RegistrationStatus = Status.Voided;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();

        }
    }
}