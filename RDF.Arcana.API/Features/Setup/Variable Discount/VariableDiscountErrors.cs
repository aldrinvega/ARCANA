using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Variable_Discount;

public class VariableDiscountErrors
{
    public static Error NotFound() => new Error("Discount.NotFound", "Discount not found");

    public static Error Overlap() => new Error("Discount.Overlap", "The new discount range overlaps with an existing one.");
}