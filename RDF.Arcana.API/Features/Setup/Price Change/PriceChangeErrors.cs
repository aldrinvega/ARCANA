using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Price_Change;

public class PriceChangeErrors
{
    public static Error AlreadyAdded(DateTime effectivityDate) => new Error("PriceChange.AlreadyAdded",
        $"Price change for {effectivityDate:MM/dd/yyyy} is already added");

    public static Error NotFound() => new Error("PriceChange.NotFound", "Price change not found");
}