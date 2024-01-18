using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;
using RDF.Arcana.API.Features.Requests_Approval;

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
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            command.ClientId = id;

            var freebies = await _mediator.Send(command);
            if (freebies.IsFailure)
            {
                return BadRequest(freebies);
            }
            return Ok(freebies);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class RequestFreebiesCommand : IRequest<Result>
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
        public string EmailAddress { get; set; }
        public string StoreType { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int AddedBy { get; set; }
        public IEnumerable<FreebieCollection> Freebies { get; set; }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class FreebieCollection
        {
            public int FreebieRequestId { get; set; }
            public string Status { get; set; }
            public int TransactionNumber { get; set; }
            public ICollection<FreebieItemForDirectClient> FreebieItems { get; set; }
        }

        public class FreebieItemForDirectClient
        {
            public int? Id { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string UOM { get; set; }
            public int? Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<RequestFreebiesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RequestFreebiesCommand request,
            CancellationToken cancellationToken)
        {
            //Validate if the client is exist
            var client = await _context.Clients
                             .Include(storeType => storeType.StoreType)
                             .Include(x => x.OwnersAddress)
                             .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken) ??
                         throw new ClientIsNotFound(request.ClientId);

            var clientFreebies = new List<RequestFreebiesResult.FreebieItemForDirectClient>();

            var freebieResult = new List<RequestFreebiesResult.FreebieCollection>();
            // Check if client has previously requested for freebies
            var previousRequestCount =
                await _context.FreebieRequests.CountAsync(f => f.ClientId == request.ClientId && f.Status != Status.Rejected,
                    cancellationToken);

            // Check if the client has recent request. Succeeding request will subject to approval
            var withRecentRequest = await _context.FreebieRequests.FirstOrDefaultAsync(
                x => x.ClientId == request.ClientId &&
                     (x.Status == Status.ForReleasing || x.Status == Status.ApproverApproval),
                cancellationToken);

            /*var freebiesResult = IList<RequestFreebiesResult.FreebieCollection>(); */

            if (withRecentRequest != null)
            {
                return FreebieErrors.WithRecentRequest(withRecentRequest.Status);
            }

            // This will be true if client is requesting freebies for the first time, and will be false for any subsequent requests
            var isFirstRequest = previousRequestCount == 0;

            var status = isFirstRequest ? Status.ForReleasing : Status.UnderReview;

            if (request.Freebies.Count > 5)
            {
                return FreebieErrors.Exceed5Items();
            }

            if (request.Freebies.Select(x => x.ItemId).Distinct().Count() != request.Freebies.Count)
            {
               return FreebieErrors.CannotBeRepeated();
            }

            //Validate if the Item is already requested | 1 item per client
            foreach (var item in request.Freebies)
            {
                var existingRequest = await _context.FreebieItems
                    .Include(x => x.Items)
                    .Include(f => f.FreebieRequest)
                    .Where(f => f.ItemId == item.ItemId && f.FreebieRequest.ClientId == request.ClientId &&
                                f.FreebieRequest.Status != Status.Rejected)
                    .FirstOrDefaultAsync(cancellationToken);

                if (existingRequest != null)
                {
                    return FreebieErrors.AlreadyRequested(existingRequest.Items.ItemDescription);
                }
            }

            // Create new freebie request
            var freebieRequest = new FreebieRequest
            {
                ClientId = request.ClientId,
                /*ApprovalsId = newApproval.Id,*/
                Status = status,
                IsDelivered = false,
                RequestedBy = request.AddedBy
            };
            _context.FreebieRequests.Add(freebieRequest);

            
            //Get the approver for Freebies module

            if (isFirstRequest == false)
            {
                var approver = await _context.Approvers
                    .Where(x => x.ModuleName == Modules.FreebiesApproval && x.Level == 1)
                    .FirstOrDefaultAsync(cancellationToken);

                if (approver is null)
                {
                    return ApprovalErrors.NoApproversFound(Modules.FreebiesApproval);
                }
                var newRequest = new Request(
                    Modules.FreebiesApproval,
                    request.AddedBy,
                    approver.UserId,
                    Status.UnderReview
                );

                await _context.Requests.AddAsync(newRequest, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                freebieRequest.RequestId = newRequest.Id;
            }

            
            // Add the items requested
            foreach (var freebieItem in request.Freebies.Select(freebie => new FreebieItems
                     {
                         FreebieRequestId = freebieRequest.Id,
                         ItemId = freebie.ItemId,
                         Quantity = 1
                     }))
            {
                await _context.FreebieItems.AddAsync(freebieItem, cancellationToken);

                //Get the items details that has been requested
                //Get the item details inserted by Item Id
                var itemDetails = await _context.Items
                    .Include(x => x.Uom)
                    .Where(i => i.Id == freebieItem.ItemId)
                    .Select(i => new { i.ItemCode, i.ItemDescription, i.Uom.UomCode })
                    .FirstOrDefaultAsync(cancellationToken);
                        
                //Add the item details to be return
                clientFreebies.Add(new RequestFreebiesResult.FreebieItemForDirectClient
                {
                    Id = freebieItem.Id,
                    ItemId = freebieItem.ItemId,
                    ItemCode = itemDetails.ItemCode,
                    ItemDescription = itemDetails.ItemDescription,
                    UOM = itemDetails.UomCode,
                    Quantity = freebieItem.Quantity
                });
            }
            
            //Result for the added freebies

            freebieResult.Add( new RequestFreebiesResult.FreebieCollection
            {
                FreebieRequestId = freebieRequest.Id,
                Status = freebieRequest.Status,
                TransactionNumber = freebieRequest.Id,
                FreebieItems = clientFreebies,
            });

            var notification = new Domain.Notification
            {
                UserId = request.AddedBy,
                Status = Status.ForReleasing
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            //Return the result on the client info including the request.
            var result =  new RequestFreebiesResult
            {
                Id = client.Id,
                OwnersName = client.Fullname,
                EmailAddress = client.EmailAddress,
                StoreType = client.StoreType.StoreTypeName,
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
                Freebies = freebieResult,
                AddedBy = client.AddedBy,
            };

            return Result.Success(result);
        }
    }
}                                                                        