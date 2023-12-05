using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Approved;

[Route("api/Prospecting")]
[ApiController]
public class UpdateApprovedProspectStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateApprovedProspectStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateApprovedProspectStatus/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var command = new UpdateApprovedProspectStatusCommand
            {
                ClientId = id
            };

            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateApprovedProspectStatusCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateApprovedProspectStatusCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateApprovedProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.Approvals
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(
                        x => x.ClientId == request.ClientId &&
                             x.IsApproved == true
                        , cancellationToken);

            if (existingRequestedProspect is null)
            {
                throw new ClientIsNotFound(request.ClientId);
            }

            existingRequestedProspect.Client.IsActive = !existingRequestedProspect.Client.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}