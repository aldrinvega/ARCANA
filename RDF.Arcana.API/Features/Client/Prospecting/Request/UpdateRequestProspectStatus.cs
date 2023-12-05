using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]

public class UpdateRequestProspectStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateRequestProspectStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateRequestProspectStatusCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        // public string Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateRequestProspectStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateRequestProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.Approvals.FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.IsApproved == false && x.ApprovalType =="Approver Approval", cancellationToken);

            if (existingRequestedProspect is null)
            {
                return ClientErrors.NotFound();
            }

            existingRequestedProspect.IsActive = !existingRequestedProspect.IsActive;
            // existingRequestedProspect.Reason = request.Reason;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }

    [HttpPatch("UpdateRequestedProspectStatus/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var command = new UpdateRequestProspectStatusCommand
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
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}