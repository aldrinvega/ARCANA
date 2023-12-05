using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebie")]
[ApiController]
public class UpdateFreebiesInformation : ControllerBase

{
    private readonly IMediator _mediator;

    public UpdateFreebiesInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateFreebieInformation/{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateFreebiesInformationCommand command, [FromRoute] int id,
        [FromQuery] int freebieId)
    {
        try
        {
            command.ClientId = id;
            command.FreebieRequestId = freebieId;
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

    public class UpdateFreebiesInformationCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public int FreebieRequestId { get; set; }
        public List<FreebieCollection> Freebies { get; set; }

        public class FreebieCollection
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class UpdateFreebieInformationResult
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

    public class Handler : IRequestHandler<UpdateFreebiesInformationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateFreebiesInformationCommand request,
            CancellationToken cancellationToken)
        {
            var clientFreebies = new List<UpdateFreebieInformationResult.Freebie>();

            var client = await _context.Clients
                .Include(x => x.OwnersAddress)
                .FirstOrDefaultAsync(x => x.Id == request.ClientId, cancellationToken);

            var approvals = await _context.Approvals
                .Include(x => x.Client)
                .Include(x => x.FreebieRequest) // adjusted this line
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .FirstOrDefaultAsync(
                    x => x.ClientId == request.ClientId &&
                         x.ApprovalType == Status.ForFreebieApproval &&
                         x.FreebieRequest.Any(x => x.Id == request.FreebieRequestId),
                    cancellationToken);

            if (approvals is null)
            {
                return FreebieErrors.NoFreebieApprovalFound();
            }

            var freebieRequestToUpdate = approvals.FreebieRequest
                .FirstOrDefault(fr => fr.Id == request.FreebieRequestId && !fr.IsDelivered);

            if (freebieRequestToUpdate == null)
            {
                return FreebieErrors.NoFreebieFound();
            }

            var requestItemIds = request.Freebies.Select(f => f.ItemId).ToList();
            var existingItemIds = freebieRequestToUpdate.FreebieItems.Select(i => i.ItemId).ToList();
            var itemsToRemove = existingItemIds.Except(requestItemIds);
            foreach (var itemId in itemsToRemove)
            {
                var itemToRemove = freebieRequestToUpdate.FreebieItems.First(i => i.ItemId == itemId);
                freebieRequestToUpdate.FreebieItems.Remove(itemToRemove);
            }

            foreach (var requestFreebie in request.Freebies)
            {
                var freebieItem =
                    freebieRequestToUpdate.FreebieItems.FirstOrDefault(x => x.ItemId == requestFreebie.ItemId);

                var itemDetails = await _context.Items
                    .Include(x => x.Uom)
                    .Where(i => i.Id == freebieItem.ItemId)
                    .Select(i => new { i.ItemCode, i.ItemDescription, i.Uom.UomCode })
                    .FirstOrDefaultAsync(cancellationToken);
                if (freebieItem != null)
                {
                    // If the freebie item exists in the database, update its quantity
                    freebieItem.Quantity = requestFreebie.Quantity;
                    clientFreebies.Add(new UpdateFreebieInformationResult.Freebie
                    {
                        FreebieRequestId = freebieRequestToUpdate.Id,
                        Status = freebieRequestToUpdate.Status,
                        TransactionNumber = freebieRequestToUpdate.Id, // Change this to appropriate value
                        FreebieItems = new List<UpdateFreebieInformationResult.FreebieItem>
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
                else
                {
                    // If the freebie doesn't exist, add it.
                    freebieRequestToUpdate.FreebieItems.Add(
                        new FreebieItems
                        {
                            RequestId = request.FreebieRequestId,
                            ItemId = requestFreebie.ItemId,
                            Quantity = requestFreebie.Quantity
                        });

                    clientFreebies.Add(new UpdateFreebieInformationResult.Freebie
                    {
                        FreebieRequestId = freebieRequestToUpdate.Id,
                        Status = freebieRequestToUpdate.Status,
                        TransactionNumber = freebieRequestToUpdate.Id, // Change this to appropriate value
                        FreebieItems = new List<UpdateFreebieInformationResult.FreebieItem>
                        {
                            new()
                            {
                                Id = freebieRequestToUpdate.Id,
                                ItemId = requestFreebie.ItemId,
                                ItemCode = itemDetails.ItemCode,
                                ItemDescription = itemDetails.ItemDescription,
                                UOM = itemDetails.UomCode,
                                Quantity = requestFreebie.Quantity
                            }
                        }
                    });
                }
            }

            approvals.IsApproved = true;

            await _context.SaveChangesAsync(cancellationToken);


            var result = new UpdateFreebieInformationResult
            {
                Id = client.Id,
                OwnersName = client.Fullname,
                OwnersAddress = new UpdateFreebieInformationResult.OwnersAddressCollection
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
                AddedBy = client.AddedBy
            };

            return Result.Success(result);
        }
    }
}