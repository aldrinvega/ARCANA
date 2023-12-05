using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Rejected;

[Route("api/Prospecting")]
[ApiController]

public class UpdateRejectProspectStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateRejectProspectStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateRejectProspectStatusCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        // public string Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateRejectProspectStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateRejectProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.Approvals.FirstOrDefaultAsync(
                    x => x.ClientId == request.ClientId &&
                    x.Client.RegistrationStatus == Status.Rejected &&
                    x.IsApproved == false
                    , cancellationToken);

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

    [HttpPatch("UpdateRejectProspectStatus/{id:int}")]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var command = new UpdateRejectProspectStatusCommand
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
            return BadRequest(e.Message);
        }
    }
}