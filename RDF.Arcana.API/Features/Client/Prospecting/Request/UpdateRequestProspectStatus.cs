using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]

public class UpdateRequestProspectStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateRequestProspectStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateRequestProspectStatusCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        // public string Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateRequestProspectStatusCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateRequestProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.Approvals.FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.IsApproved == false && x.ApprovalType =="Approver Approval", cancellationToken);

            if (existingRequestedProspect is null)
            {
                throw new ClientIsNotFound(request.ClientId);
            }

            existingRequestedProspect.IsActive = !existingRequestedProspect.IsActive;
            // existingRequestedProspect.Reason = request.Reason;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    [HttpPatch("UpdateRequestedProspectStatus/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateRequestProspectStatusCommand
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