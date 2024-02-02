using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Price_Mode
{
    public static class PriceModeErrors
    {
        public static Error AlreadyExisit(string Mode) => new("PriceMode.AlreadyExist", $"Price mode {Mode} is already exist");

        public static Error NotFound() => new("PriceMode.NotFound", "Price mode not found");


    }
}
