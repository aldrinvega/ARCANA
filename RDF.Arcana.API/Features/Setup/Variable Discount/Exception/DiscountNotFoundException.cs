namespace RDF.Arcana.API.Features.Setup.Discount.Exception;

public class DiscountNotFoundException : System.Exception
{
    public DiscountNotFoundException() : base("Discount not found"){}
}