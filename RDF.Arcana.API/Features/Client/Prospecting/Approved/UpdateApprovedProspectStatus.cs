using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
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

    public class UpdateApprovedProspectStatusCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateApprovedProspectStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateApprovedProspectStatusCommand request, CancellationToken cancellationToken)
        {
            var existingRequestedProspect =
                await _context.Clients
                    .FirstOrDefaultAsync(
                        x => x.Id == request.ClientId, cancellationToken);

            if (existingRequestedProspect is null)
            {
                return ClientErrors.NotFound();
            }

            existingRequestedProspect.IsActive = !existingRequestedProspect.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}