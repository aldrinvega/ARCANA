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
            var listingFees = await _context.Approvals
                .Include(x => x.Client)
                .Include(x => x.ListingFee)
                .ThenInclude(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(
                    x => x.ClientId == request.ClientId
                         && x.ApprovalType == "For Listing Fee Approval"
                         && x.ListingFee.Id == request.ListingFeeId,
                    cancellationToken);

            if (listingFees == null)
            {
                throw new ListingFeeNotFound();
            }

            decimal total = 0;

            var requestItemIds = request.ListingItems.Select(f => f.ItemId).ToList();
            var existingItemIds = listingFees.ListingFee.ListingFeeItems.Select(i => i.ItemId).ToList();
            var itemsToRemove = existingItemIds.Except(requestItemIds);
            foreach (var itemId in itemsToRemove)
            {
                var itemToRemove = listingFees.ListingFee.ListingFeeItems.First(i => i.ItemId == itemId);
                listingFees.ListingFee.ListingFeeItems.Remove(itemToRemove);
            }

            foreach (var requestListingItem in request.ListingItems)
            {
                var listingFeeItem =
                    listingFees.ListingFee.ListingFeeItems.FirstOrDefault(x => x.ItemId == requestListingItem.ItemId);
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
                    listingFees.ListingFee.ListingFeeItems.Add(newItem);
                }

                listingFees.ListingFee.Total = request.Total;
            }

            listingFees.ListingFee.Status = "Requested";

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}