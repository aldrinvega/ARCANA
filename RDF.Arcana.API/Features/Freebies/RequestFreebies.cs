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

    public class RequestFreebiesCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public List<UpdateFreebie> Freebies { get; set; }
        public int AddedBy { get; set; }

        public class UpdateFreebie
        {
            public int ItemId { get; set; }
        }
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
            var existingApproval = await _context.Approvals
                .FirstOrDefaultAsync(a => a.ClientId == request.ClientId && a.ApprovalType == "For Freebie Request",
                    cancellationToken);

            // Check if client has previously requested for freebies
            var previousRequestCount =
                await _context.FreebieRequests.CountAsync(f => f.ClientId == request.ClientId, cancellationToken);

            // This will be true if client is requesting freebies for the first time, and will be false for any subsequent requests
            var isFirstRequest = previousRequestCount == 0;

            var status = isFirstRequest ? Status.ForReleasing : Status.ApproverApproval;

            //Check if the client is existing
            var existingClient =
                await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

            if (request.Freebies.Count > 5)
            {
                throw new Exception("Freebie request is not exceeding to 5 items");
            }

            if (request.Freebies.Select(x => x.ItemId).Distinct().Count() != request.Freebies.Count)
            {
                throw new Exception("Items cannot be repeated.");
            }

            //Validate if the Item is already requested | 1 item per client
            foreach (var item in request.Freebies)
            {
                var existingRequest = await _context.FreebieItems
                    .Include(x => x.Items)
                    .Include(f => f.FreebieRequest)
                    .Where(f => f.ItemId == item.ItemId && f.FreebieRequest.ClientId == request.ClientId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingRequest != null)
                {
                    throw new Exception(
                        $"Item with ID {existingRequest.Items.ItemDescription} has already been requested.");
                }
            }

            var newApproval = new Approvals
            {
                ClientId = request.ClientId,
                ApprovalType = "For Freebie Approval",
                IsApproved = isFirstRequest,
                IsActive = true,
                RequestedBy = request.AddedBy,
                ApprovedBy = request.AddedBy
            };
            await _context.Approvals.AddAsync(newApproval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var transactionNumber = GenerateTransactionNumber();
            var freebieRequest = new FreebieRequest
            {
                ClientId = request.ClientId,
                TransactionNumber = transactionNumber,
                ApprovalId = newApproval.Id,
                Status = status,
                IsDelivered = false,
                RequestedBy = request.AddedBy
            };
            _context.FreebieRequests.Add(freebieRequest);
            await _context.SaveChangesAsync(cancellationToken);

            existingClient.RegistrationStatus = "For Releasing";

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
}