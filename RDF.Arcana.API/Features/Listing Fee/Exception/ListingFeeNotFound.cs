namespace RDF.Arcana.API.Features.Listing_Fee.Exception;

public class ListingFeeNotFound : System.Exception
{
    public ListingFeeNotFound() : base("Listing not found")
    {
    }
}