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
                await _context.ApprovedClients.FirstOrDefaultAsync(x =>
                    x.ClientId == request.ClientId && x.Status == 3, cancellationToken: cancellationToken);
            if (validateClient is not null)
            {
                throw new Exception("User is rejected, cannot proceed request!");
            }

            var transactionNumber = GenerateTransactionNumber();

            var freebieRequest = new FreebieRequest
            {
                ClientId = request.ClientId,
                TransactionNumber = transactionNumber,
                StatusId = 1,
                IsActive = true,
                AddedBy = request.AddedBy
            };
    
            foreach (var freebies in request.Freebies.Select(freebieItem => new Domain.Freebies
                     {
                         ItemId = freebieItem.ItemId,
                         Quantity = freebieItem.Quantity,
                         FreebieRequest = freebieRequest
                     }))
            {
                await _context.Freebies.AddAsync(freebies, cancellationToken);
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