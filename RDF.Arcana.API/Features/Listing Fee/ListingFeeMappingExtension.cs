using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Location;

namespace RDF.Arcana.API.Features.Listing_Fee;

public static class ListingFeeMappingExtension
{
    public static GetAllListingFee.GetAllListingFeeResult
        ToGetAllListingFeeResult(this Approvals listingFee)
    {
        return new GetAllListingFee.GetAllListingFeeResult
        {
            ClientId = listingFee.ClientId,
            ClientName = listingFee.Client.Fullname,
            BusinessName = listingFee.Client.BusinessName,
            RequestedBy = listingFee.ListingFee.RequestedBy,
            Total = listingFee.ListingFee.Total,
            ListingItems = listingFee.ListingFee.ListingFeeItems.Select(x =>
                new GetAllListingFee.GetAllListingFeeResult.ListingItem
                {
                    ItemId = x.ItemId,
                    ItemCode = x.Item.ItemCode,
                    ItemDescription = x.Item.ItemDescription,
                    Uom = x.Item.Uom.UomCode,
                    Sku = x.Sku,
                    UnitCost = x.UnitCost,
                    Quantity = x.Quantity
                })
        };
    }
}