using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Listing_Fee.Errors;

public class ListingFeeErrors
{
    public static Error NotFound() =>
        new Error("ListingFee.NotFound", "Listing fee request not found.");
}