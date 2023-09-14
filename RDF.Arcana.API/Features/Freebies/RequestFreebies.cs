using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]

public class RequestFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class RequestFreebiesCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public List<UpdateFreebie> Freebies { get; set; }
        
        public class UpdateFreebie
        {
            public int ItemId { get; set; }
        }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<RequestFreebiesCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RequestFreebiesCommand request, CancellationToken cancellationToken)
         {
             var validateClient =
                 await _context.FreebieRequests
                     .Include(x => x.Clients)
                     .Include(x => x.FreebieItems)
                     .FirstOrDefaultAsync(x =>
                     x.ClientId == request.ClientId &&
                        x.IsDelivered == true,
             cancellationToken);

            var existingClient = await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

             if (validateClient is not null)
             {
                 throw new Exception("Delivered na yan ayy");
             }

             if (request.Freebies.Count > 5)
             {
                 throw new Exception("Freebie request is not exceeding to 5 items");
             }

             if (request.Freebies.Select(x => x.ItemId).Distinct().Count() != request.Freebies.Count)
             {
                 throw new Exception("Items cannot be repeated.");
             }

             var newApproval = new Approvals
             {
                 ClientId = request.ClientId,
                 ApprovalType = "For Freebie Approval",
                 IsApproved = false,
                 IsActive = true,
                 RequestedBy = request.AddedBy
             };
             await _context.Approvals.AddAsync(newApproval, cancellationToken);
             await _context.SaveChangesAsync(cancellationToken);
             
             var transactionNumber = GenerateTransactionNumber();
             var freebieRequest = new FreebieRequest
             {
                 ClientId = request.ClientId,
                 TransactionNumber = transactionNumber,
                 ApprovalId = newApproval.Id,
                 Status = "Requested",
                 IsDelivered = false,
                 RequestedBy = request.AddedBy
             };
             _context.FreebieRequests.Add(freebieRequest);
             await _context.SaveChangesAsync(cancellationToken);

            existingClient.RegistrationStatus = "Freebie Requested";

            foreach (var freebieItem in request.Freebies.Select(freebie => new FreebieItems
                      {
                          RequestId = freebieRequest.Id,
                          ItemId = freebie.ItemId,
                          Quantity = 1
                      }))
             {
                 await _context.FreebieItems.AddAsync(freebieItem, cancellationToken);
             }
             await _context.SaveChangesAsync(cancellationToken);
             
             return Unit.Value;
         }

        private static string GenerateTransactionNumber()
        {
            var random = new Random();
            return random.Next(1_000_000, 10_000_000).ToString("D7");
        }
    }
    
    [HttpPost("RequestFreebies/{id}")]
    public async Task<IActionResult> Add(RequestFreebiesCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            command.ClientId = id;

            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Freebie is requested successfully");
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}