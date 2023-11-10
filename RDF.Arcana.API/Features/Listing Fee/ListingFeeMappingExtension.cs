using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Listing_Fee;

public static class ListingFeeMappingExtension
{
    public static GetAllListingFee.ClientsWithListingFee ToGetAllListingFeeResult(this ListingFee listingFee)
    {
        return new GetAllListingFee.ClientsWithListingFee
        {
            ClientId = listingFee.ClientId,
            ClientName = listingFee.Client.Fullname,
            BusinessName = listingFee.Client.BusinessName,
            CreateAt = listingFee.CratedAt.ToString("yyyy-MM-dd hh:mm:ss"),
            ListingFeeId = listingFee.Id,
            ApprovalId = listingFee.ApprovalsId,
            Status = listingFee.Status,
            RequestedBy = listingFee.RequestedByUser.Fullname,
            Total = listingFee.Total,
            CancellationReason = listingFee.Approvals.Reason,
            ListingItems = listingFee.ListingFeeItems.Select(li =>
                new GetAllListingFee.ClientsWithListingFee.ListingItem
                {
                    ItemId = li.ItemId,
                    ItemCode = li.Item.ItemCode,
                    ItemDescription = li.Item.ItemDescription,
                    Uom = li.Item.Uom.UomCode,
                    Sku = li.Sku,
                    UnitCost = li.UnitCost
                }).ToList()
        };
    }
}