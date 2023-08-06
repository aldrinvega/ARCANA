using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Domain.New_Doamin;

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
        public List<Freebie> Freebies { get; set; }
        
        public class Freebie
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
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
                 await _context.FreebieRequests.FirstOrDefaultAsync(x =>
                     x.ClientId == request.ClientId &&
                        x.IsDelivered == true,
             cancellationToken);
             if (validateClient is not null)
             {
                 throw new Exception("Delivered na yan ayy");
             }
             
             var newApproval = new Approvals
             {
                 ClientId = request.ClientId,
                 ApprovalType = "For Freebie Approval",
                 IsApproved = false,
                 IsActive = true
             };
             _context.Approvals.Add(newApproval);
             await _context.SaveChangesAsync(cancellationToken);
             
             var transactionNumber = GenerateTransactionNumber();
             var freebieRequest = new FreebieRequest
             {
                 ClientId = request.ClientId,
                 TransactionNumber = transactionNumber,
                 ApprovalId = newApproval.Id,
                 IsDelivered = false
             };
             _context.FreebieRequests.Add(freebieRequest);
             await _context.SaveChangesAsync(cancellationToken);
         
             foreach (var freebie in request.Freebies)
             {
                 var freebieItem = new FreebieItems
                 {
                     RequestId = freebieRequest.Id,
                     ItemId = freebie.ItemId,
                     Quantity = freebie.Quantity
                 };
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
    
    [HttpPost("RequestFreebies")]
    public async Task<IActionResult> Add(RequestFreebiesCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

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