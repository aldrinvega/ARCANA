using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Discount;

public class DiscountErrors
{
    public static Error AlreadyExist(string discount) => new("Discount.AlreadyExist", $"{discount} is already exist");
    public static Error NotFound() => new("Discount.NotFound", "Discount not found");
}