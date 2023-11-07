using RDF.Arcana.API.Domain;

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
            ListingFee = listingFee.ListingFee?.Select(lf =>
                new GetAllListingFee.GetAllListingFeeResult.ListingFeeCollections
                {
                    Id = lf.Id,
                    ApprovalId = lf.ApprovalsId,
                    Status = lf.Status,
                    RequestedBy = lf.RequestedByUser.Fullname,
                    Total = lf.Total,
                    ListingItems = lf.ListingFeeItems.Select(li =>
                        new GetAllListingFee.GetAllListingFeeResult.ListingItem
                        {
                            ItemId = li.ItemId,
                            ItemCode = li.Item.ItemCode,
                            ItemDescription = li.Item.ItemDescription,
                            Uom = li.Item.Uom.UomCode,
                            Sku = li.Sku,
                            UnitCost = li.UnitCost,
                            Quantity = li.Quantity
                        })
                })
        };
    }
}