using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

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

    public class UpdateDirectRegisteredClientStatusCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDirectRegisteredClientStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateDirectRegisteredClientStatusCommand request,
            CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId,
                cancellationToken: cancellationToken);

            if (existingClient == null)
            {
                return ClientErrors.NotFound();
            }

            existingClient.IsActive = !existingClient.IsActive;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}