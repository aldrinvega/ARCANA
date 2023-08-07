using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Approved;

[Route("api/Prospecting")]
[ApiController]

public class ApproveProspectRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveProspectRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class ApprovedProspectRequestCommand : IRequest<Unit>
    {
        public int ProspectId { get; set; }
    }
    public class Handler : IRequestHandler<ApprovedProspectRequestCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

       public async Task<Unit> Handle(ApprovedProspectRequestCommand request, CancellationToken cancellationToken)
         {
             // Validate the approved by user
             // if (request.ApprovedBy < 1)
             // {
             //     throw new ArgumentException("Invalid user ID");
             // }
         
             var requestedClients =
                 await _context.Approvals
                     .Include(x => x.Client)
                     .FirstOrDefaultAsync(
                     x => x.ClientId == request.ProspectId && 
                          x.ApprovalType == "Approver Approval" &&
                          x.IsActive == true &&
                          x.IsApproved == false &&
                          x.Client.RegistrationStatus == "Requested",
                     cancellationToken);
         
             if (requestedClients is null)
             {
                 throw new NoProspectClientFound();
             }

             if (requestedClients.IsApproved)
             {
                 throw new System.Exception("This client is already approved");
             }

             requestedClients.IsApproved = true;
             await _context.SaveChangesAsync(cancellationToken);
             return Unit.Value;
         }
    }

    [HttpPut("ApproveProspectRequest/{id}")]
    public async Task<IActionResult> Approved( int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new ApprovedProspectRequestCommand
            {
                ProspectId = id,
            };
            
            // if (User.Identity is ClaimsIdentity identity
            //     && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            // {
            //     command.ApprovedBy = userId;
            // };

            await _mediator.Send(command);
            
            response.Messages.Add("Prospect has been approved successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}