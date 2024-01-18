/*using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.Prospecting.Rejected;

[Route("api/Prospecting")]
[ApiController]

public class RejectProspectRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectProspectRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class RejectProspectRequestCommand : IRequest<Result>
    {
        public int ProspectId { get; set; }
        public int RejectedBy { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectProspectRequestCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectProspectRequestCommand request, CancellationToken cancellationToken)
        {
            // Fetch the requested client by the prospectId
            var requestedClient =
                await _context.Approvals
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(
                    x => x.ClientId == request.ProspectId && 
                         x.ApprovalType == "Approver Approval" && 
                         x.IsApproved == false && 
                         x.IsActive == true,
                    cancellationToken);

            // If no matching client is found, throw an exception
            if (requestedClient is null)
            {
                return ClientErrors.NotFound();
            }

            if (requestedClient.Client.RegistrationStatus == Status.Rejected)
            {
                return ClientErrors.AlreadyRejected(requestedClient.Client.BusinessName);
            }

            // Set the status to "rejected" or an equivalent indicator for rejection in your system
            // requestedClient.ApprovalType = "Rejected";
            requestedClient.Reason = request.Reason;
            requestedClient.Client.RegistrationStatus = "Rejected";
            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

    [HttpPut("RejectProspectRequest/{id:int}")]
    public async Task<IActionResult> Reject([FromRoute] int id, [FromBody]RejectProspectRequestCommand command)
    {
        try
        {
            command.ProspectId = id;
                
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.RejectedBy = userId;
            }

            var result =  await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }
}*/