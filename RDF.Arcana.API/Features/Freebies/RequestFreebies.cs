using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

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
        var response = new QueryOrCommandResult<RequestFreebiesResult>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            command.ClientId = id;

            var freebies = await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Data = freebies;
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

    public class RequestFreebiesCommand : IRequest<RequestFreebiesResult>
    {
        public int ClientId { get; set; }
        public List<Freebie> Freebies { get; set; }
        public int AddedBy { get; set; }

        public class Freebie
        {
            public int ItemId { get; set; }
        }
    }

    public class RequestFreebiesResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int AddedBy { get; set; }
        public IEnumerable<Freebie> Freebies { get; set; }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class Freebie
        {
            public int FreebieRequestId { get; set; }
            public string Status { get; set; }
            public int TransactionNumber { get; set; }
            public ICollection<FreebieItem> FreebieItems { get; set; }
        }

        public class FreebieItem
        {
            public int? Id { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string UOM { get; set; }
            public int? Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<RequestFreebiesCommand, RequestFreebiesResult>
    {
        private const string REJECTED = "Rejected";
        private const string FOR_FREEBIE_APPROVAL = "For Freebie Approval";
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<RequestFreebiesResult> Handle(RequestFreebiesCommand request,
            CancellationToken cancellationToken)
        {
            //Validate if the client is exist
            var client = await _context.Clients
                             .Include(x => x.OwnersAddress)
                             .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken) ??
                         throw new ClientIsNotFound(request.ClientId);

            var clientFreebies = new List<RequestFreebiesResult.Freebie>();

            // Check if client has previously requested for freebies
            var previousRequestCount =
                await _context.FreebieRequests.CountAsync(f => f.ClientId == request.ClientId && f.Status != REJECTED,
                    cancellationToken);

            // Check if the client has recent request. Succeeding request will subject to approval
            var withRecentRequest = await _context.FreebieRequests.FirstOrDefaultAsync(
                x => x.ClientId == request.ClientId &&
                     (x.Status == Status.ForReleasing || x.Status == Status.ApproverApproval),
                cancellationToken);

            if (withRecentRequest != null)
            {
                throw new Exception($"Client has {withRecentRequest.Status.ToLower()} freebies");
            }

            // This will be true if client is requesting freebies for the first time, and will be false for any subsequent requests
            var isFirstRequest = previousRequestCount == 0;

            var status = isFirstRequest ? Status.ForReleasing : Status.ApproverApproval;

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
                    .Where(f => f.ItemId == item.ItemId && f.FreebieRequest.ClientId == request.ClientId &&
                                f.FreebieRequest.Status != REJECTED)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingRequest != null)
                {
                    throw new Exception(
                        $"{existingRequest.Items.ItemDescription} has already been requested.");
                }
            }

            // Create new approval for the freebie
            var newApproval = new Approvals
            {
                ClientId = request.ClientId,
                ApprovalType = FOR_FREEBIE_APPROVAL,
                IsApproved = isFirstRequest,
                IsActive = true,
                RequestedBy = request.AddedBy,
                ApprovedBy = request.AddedBy
            };
            await _context.Approvals.AddAsync(newApproval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Create new freebie request
            var freebieRequest = new FreebieRequest
            {
                ClientId = request.ClientId,
                ApprovalsId = newApproval.Id,
                Status = status,
                IsDelivered = false,
                RequestedBy = request.AddedBy
            };
            _context.FreebieRequests.Add(freebieRequest);
            await _context.SaveChangesAsync(cancellationToken);

            var freebieRequestObject = new RequestFreebiesResult.Freebie
            {
                FreebieRequestId = freebieRequest.Id,
                Status = freebieRequest.Status,
                TransactionNumber = freebieRequest.Id,
                FreebieItems = new List<RequestFreebiesResult.FreebieItem>()
            };

            // Add the items requested
            foreach (var freebieItem in request.Freebies.Select(freebie => new FreebieItems
                     {
                         RequestId = freebieRequest.Id,
                         ItemId = freebie.ItemId,
                         Quantity = 1
                     }))
            {
                await _context.FreebieItems.AddAsync(freebieItem, cancellationToken);

                //Get the items details that has been requested
                var itemDetails = await _context.Items
                    .Include(x => x.Uom)
                    .Where(i => i.Id == freebieItem.ItemId)
                    .Select(i => new { i.ItemCode, i.ItemDescription, i.Uom.UomCode })
                    .FirstOrDefaultAsync(cancellationToken);

                freebieRequestObject.FreebieItems.Add(new RequestFreebiesResult.FreebieItem
                {
                    Id = freebieItem.Id,
                    ItemId = freebieItem.ItemId,
                    ItemCode = itemDetails.ItemCode,
                    ItemDescription = itemDetails.ItemDescription,
                    UOM = itemDetails.UomCode,
                    Quantity = freebieItem.Quantity
                });

                clientFreebies.Add(new RequestFreebiesResult.Freebie
                {
                    FreebieRequestId = freebieRequest.Id,
                    Status = freebieRequest.Status,
                    TransactionNumber = freebieRequest.Id,
                    FreebieItems = new List<RequestFreebiesResult.FreebieItem>
                    {
                        new()
                        {
                            Id = freebieItem.Id,
                            ItemId = freebieItem.ItemId,
                            ItemCode = itemDetails.ItemCode,
                            ItemDescription = itemDetails.ItemDescription,
                            UOM = itemDetails.UomCode,
                            Quantity = freebieItem.Quantity
                        }
                    }
                });
            }

            clientFreebies.Add(freebieRequestObject);

            await _context.SaveChangesAsync(cancellationToken);

            //Return the result on the client info including the request.
            return new RequestFreebiesResult
            {
                Id = client.Id,
                OwnersName = client.Fullname,
                OwnersAddress = new RequestFreebiesResult.OwnersAddressCollection
                {
                    HouseNumber = client.OwnersAddress.HouseNumber,
                    StreetName = client.OwnersAddress.StreetName,
                    BarangayName = client.OwnersAddress.Barangay,
                    City = client.OwnersAddress.City,
                    Province = client.OwnersAddress.Province
                },
                PhoneNumber = client.PhoneNumber,
                BusinessName = client.BusinessName,
                Freebies = clientFreebies,
                AddedBy = client.AddedBy,
            };
        }
    }
}