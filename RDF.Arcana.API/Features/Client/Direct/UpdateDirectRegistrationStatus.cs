using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.Direct;

[Route("api/DirectRegistration")]
[ApiController]
public class UpdateDirectRegistrationStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDirectRegistrationStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateDirectRegisteredClientStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateDirectRegisteredClientStatusCommand
            {
                ClientId = id
            };
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class UpdateDirectRegisteredClientStatusCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDirectRegisteredClientStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDirectRegisteredClientStatusCommand request,
            CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId,
                cancellationToken: cancellationToken);

            if (existingClient == null)
            {
                throw new Exception("No clients found");
            }

            existingClient.IsActive = !existingClient.IsActive;

            return Unit.Value;
        }
    }
}