using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/Clients"), ApiController]
public class VoidClientRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public VoidClientRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("VoidClientRegistration/{clientId:int}")]
    public async Task<IActionResult> VoidClient(int clientId)
    {
        try
        {
            var command = new VoidClientCommand
            {
                ClientId = clientId
            };

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

    public class VoidClientCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class VoidClientResult
    {
        public int ClientId { get; set; }
        public string Fullname { get; set; }
        public string BusinessName { get; set; }
    }

    public class Handler : IRequestHandler<VoidClientCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(VoidClientCommand request,
            CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                .Include(ap => ap.Request)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

            if (existingClient == null)
            {
                return ClientErrors.NotFound();
            }

            if (existingClient.RegistrationStatus == Status.Voided)
            {
                return ClientErrors.AlreadyRejected(existingClient.BusinessName);
            }

            existingClient.RegistrationStatus = Status.Voided;
            existingClient.Request.Status = Status.Voided;

            await _context.SaveChangesAsync(cancellationToken);

            var result = new VoidClientResult
            {
                ClientId = existingClient.Id,
                Fullname = existingClient.Fullname,
                BusinessName = existingClient.BusinessName
            };

            return Result.Success(result);
        }
    }
}