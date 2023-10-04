using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register;

[Route("api/Registration")]
[ApiController]
public class RegisterClient : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterClient(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RegisterClient/{id}")]
    public async Task<IActionResult> Register([FromBody] RegisterClientCommand request, [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            request.ClientId = id;
            await _mediator.Send(request, cancellationToken);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status404NotFound;
            response.Messages.Add(ex.Message);

            return Conflict(response);
        }
    }

    public class RegisterClientCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string BusinessAddress { get; set; }
        public string AuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativePosition { get; set; }
        public int Cluster { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public class Handler : IRequestHandler<RegisterClientCommand, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _context.Clients
                    .Where(x => x.RegistrationStatus == "Released")
                    .FirstOrDefaultAsync(client => client.Id == request.ClientId, cancellationToken);

                if (existingClient == null)
                {
                    throw new ClientIsNotFound(request.ClientId);
                }

                existingClient.BusinessAddress = request.BusinessAddress;
                existingClient.RepresentativeName = request.AuthorizedRepresentative;
                existingClient.RepresentativePosition = request.AuthorizedRepresentativePosition;
                existingClient.Cluster = request.Cluster;
                existingClient.Longitude = request.Longitude;
                existingClient.Latitude = request.Latitude;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}