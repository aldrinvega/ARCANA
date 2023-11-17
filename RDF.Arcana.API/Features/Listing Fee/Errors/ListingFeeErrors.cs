using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Listing_Fee.Errors;

public class ListingFeeErrors
{
    public static Error NotFound() =>
        new Error("ListingFee.NotFound", "Listing fee request not found.");
    public static Error AlreadyArchived() =>
        new Error("ListingFee.AlreadyArchived", "Listing fee is already archived");
    public static Error AlreadyVoided() =>
        new Error("ListingFee.AlreadyVoided", "Listing fee is already voided");
    public static Error Unauthorized() =>
        new Error("ListingFee.Unauthorized", "You are not authorized to void this listing.");
    public static Error AlreadyRequested(string itemDescription) =>
        new Error("ListingFee.AlreadyRequested", $"{itemDescription} has already been requested.");
}