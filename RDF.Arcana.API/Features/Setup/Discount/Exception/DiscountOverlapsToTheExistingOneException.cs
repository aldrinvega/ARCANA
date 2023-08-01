namespace RDF.Arcana.API.Features.Setup.Discount.Exception;

public class DiscountOverlapsToTheExistingOneException : System.Exception
{
    public DiscountOverlapsToTheExistingOneException() : base("The new discount range overlaps with an existing one."){}
}