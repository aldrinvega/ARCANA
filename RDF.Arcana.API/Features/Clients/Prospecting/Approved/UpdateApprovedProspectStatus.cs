using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Approved;

[Route("api/Prospecting")]
[ApiController]

public class UpdateApprovedProspectStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateApprovedProspectStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateApprovedProspectStatusCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateApprovedProspectStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateApprovedProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.ApprovedClients.FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.IsActive == true, cancellationToken);

            if (existingRequestedProspect is null)
            {
                throw new ClientIsNotFound();
            }

            existingRequestedProspect.IsActive = !existingRequestedProspect.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    [HttpPatch("UpdateApprovedProspectStatus/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateApprovedProspectStatusCommand
            {
                ClientId = id
            };

            await _mediator.Send(command);
            response.Messages.Add("Client status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}