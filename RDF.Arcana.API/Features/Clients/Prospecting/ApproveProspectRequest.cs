using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting;

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
        public int ApprovedBy { get; set; }
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
             if (request.ApprovedBy < 1)
             {
                 throw new ArgumentException("Invalid user ID");
             }
         
             var requestedClients =
                 await _context.RequestedClients.FirstOrDefaultAsync(x => x.ClientId == request.ProspectId && x.Status == 1, cancellationToken);
         
             if (requestedClients is null)
             {
                 throw new NoProspectClientFound();
             }

             if (requestedClients.Status == 2)
             {
                 throw new System.Exception("This client is already approved");
             }

             await using (var transaction = await _context.Database.BeginTransactionAsync(cancellationToken))
             {
                 // Modify client ID according to explanation above
                 var approvedClient = new ApprovedClient
                 {
                     ClientId = requestedClients.ClientId,
                     ApprovedBy = request.ApprovedBy,
                     DateApproved = DateTime.Now,
                     IsActive = true,
                     Status = 2
                 };
         
                 await _context.ApprovedClients.AddAsync(approvedClient, cancellationToken);
                
                 requestedClients.Status = 2;
                 
                 await _context.SaveChangesAsync(cancellationToken);
                 
                 // Only commit the transaction if all operations completed successfully
                 await transaction.CommitAsync(cancellationToken);
             }
         
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
            
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.ApprovedBy = userId;
            };

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