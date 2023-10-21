using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Listing_Fee.Exception;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class UpdateListingFeeInformation : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateListingFeeInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateListingFeeItems/{id:int}")]
    public async Task<IActionResult> UpdateListingFeeItems([FromBody] UpdateListingFeeInformationCommand command,
        [FromRoute] int id, [FromQuery] int listingFeeId)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ClientId = id;
            command.ListingFeeId = listingFeeId;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Listing Fee items updated successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }


    public class UpdateListingFeeInformationCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int ListingFeeId { get; set; }
        public decimal Total { get; set; }
        public List<ListingItem> ListingItems { get; set; }

        public class ListingItem
        {
            public int ItemId { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateListingFeeInformationCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateListingFeeInformationCommand request, CancellationToken cancellationToken)
        {
            var listingFeesInApproval = await _context.Approvals
                .Where(x => x.ClientId == request.ClientId && x.ApprovalType == "For Listing Fee Approval")
                .Select(x => x.ListingFee)
                .SingleOrDefaultAsync(cancellationToken);

            if (listingFeesInApproval == null || !listingFeesInApproval.Any())
            {
                throw new ListingFeeNotFound();
            }

            var requestItemIds = request.ListingItems.Select(f => f.ItemId).ToList();

            foreach (var listingFee in listingFeesInApproval)
            {
                if (listingFee.Id != request.ListingFeeId)
                {
                    continue;
                }

                var existingItemIds = listingFee.ListingFeeItems.Select(i => i.ItemId).ToList();
                var itemsToRemove = existingItemIds.Except(requestItemIds);
                foreach (var itemId in itemsToRemove)
                {
                    var itemToRemove = listingFee.ListingFeeItems.First(i => i.ItemId == itemId);
                    listingFee.ListingFeeItems.Remove(itemToRemove);
                }

                foreach (var requestListingItem in request.ListingItems)
                {
                    var listingFeeItem =
                        listingFee.ListingFeeItems.FirstOrDefault(x => x.ItemId == requestListingItem.ItemId);

                    if (listingFeeItem != null)
                    {
                        // If the listing fee item exists in the database, update its details
                        listingFeeItem.Sku = requestListingItem.Sku;
                        listingFeeItem.UnitCost = requestListingItem.UnitCost;
                    }
                    else
                    {
                        // If the listing fee item doesn't exist, add it.
                        var newItem = new ListingFeeItems
                        {
                            ListingFeeId = request.ListingFeeId,
                            ItemId = requestListingItem.ItemId,
                            Sku = requestListingItem.Sku,
                            UnitCost = requestListingItem.UnitCost
                        };
                        listingFee.ListingFeeItems.Add(newItem);
                    }

                    listingFee.Total = request.Total;
                }

                listingFee.Status = "Requested";
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}