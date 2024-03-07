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
            CreatedAt = listingFee.CratedAt.ToString("yyyy-MM-dd"),
            ListingFeeId = listingFee.Id,
            RequestId = listingFee.RequestId,
            Status = listingFee.Status,
            Requestor = listingFee.RequestedByUser.Fullname,
            Total = listingFee.Total,
            /*CancellationReason = listingFee.Approvals.Reason,*/
            ListingItems = listingFee.ListingFeeItems.Select(li =>
                new GetAllListingFee.ClientsWithListingFee.ListingItem
                {
                    ItemId = li.ItemId,
                    ItemCode = li.Item.ItemCode,
                    ItemDescription = li.Item.ItemDescription,
                    Uom = li.Item.Uom.UomCode,
                    Sku = li.Sku,
                    UnitCost = li.UnitCost
                }).ToList(),
            ListingFeeApprovalHistories = listingFee.Request.Approvals?.OrderByDescending(a => a.CreatedAt)
                .Select( a => new GetAllListingFee.ClientsWithListingFee.ListingFeeApprovalHistory
                {
                    Module = a.Request.Module,
                    Approver = a.Approver.Fullname,
                    CreatedAt = a.CreatedAt,
                    Status = a.Status,
                })
        };
    }
}