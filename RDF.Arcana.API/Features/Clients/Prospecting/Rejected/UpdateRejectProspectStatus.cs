using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Rejected;

[Route("api/Prospecting")]
[ApiController]

public class UpdateRejectProspectStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateRejectProspectStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateRejectProspectStatusCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateRejectProspectStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRejectProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.Approvals.FirstOrDefaultAsync(
                    x => x.ClientId == request.ClientId &&
                    x.IsActive == true &&
                    x.Client.RegistrationStatus == "Rejected" &&
                    x.IsApproved == false
                    , cancellationToken);

            if (existingRequestedProspect is null)
            {
                throw new ClientIsNotFound();
            }

            existingRequestedProspect.IsActive = !existingRequestedProspect.IsActive;
            existingRequestedProspect.Reason = request.Reason;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    [HttpPatch("UpdateRejectProspectStatus/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateRejectProspectStatusCommand
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