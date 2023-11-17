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
        public List<ListingItem> ListingFeeItems { get; set; }

        public class ListingItem
        {
            public int ItemId { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateListingFeeInformationCommand, Unit>
    {
        private const string UNDER_REVIEW = "Under review";
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateListingFeeInformationCommand command, CancellationToken cancellationToken)
        {
            var listingFee = await _context.ListingFees
                .Include(x => x.ListingFeeItems)
                .Where(lf => lf.ClientId == command.ClientId && lf.Id == command.ListingFeeId)
                .Include(x => x.Approvals)
                .FirstOrDefaultAsync(cancellationToken);

            if (listingFee == null)
                throw new ListingFeeNotFound();
            if (command.ListingFeeItems == null)
                throw new ArgumentException("ListingFeeItems cannot be null");

            var listingFeeItems = command.ListingFeeItems.Select(x => x.ItemId).ToList();
            var existingItemList = listingFee.ListingFeeItems.Select(x => x.ItemId).ToList();
            var toRemove = existingItemList.Except(listingFeeItems);

            foreach (var itemId in toRemove)
            {
                var forRemove = listingFee.ListingFeeItems.First(i => i.ItemId == itemId);
                listingFee.ListingFeeItems.Remove(forRemove);
            }

            foreach (var item in command.ListingFeeItems)
            {
                var listingFeeItemToAdd = listingFee.ListingFeeItems.FirstOrDefault(x => x.ItemId == item.ItemId);

                if (listingFeeItemToAdd != null)
                {
                    listingFeeItemToAdd.Sku = item.Sku;
                    listingFeeItemToAdd.UnitCost = item.UnitCost;
                    listingFeeItemToAdd.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    listingFee.ListingFeeItems.Add(new ListingFeeItems
                    {
                        ListingFeeId = command.ListingFeeId,
                        ItemId = item.ItemId,
                        UnitCost = item.UnitCost,
                        Sku = item.Sku
                    });
                }
            }

            listingFee.Approvals.ApprovalType = Status.UnderReview;
            listingFee.Approvals.IsApproved = false;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}