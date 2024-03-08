using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Price_Mode
{
    public static class PriceModeErrors
    {
        public static Error DescriptionAlreadyExisit(string description) => new("PriceMode.AlreadyExist", $"Price mode {description} is already exist");

        public static Error CodeAlreadyExisit(string code) => new("PriceMode.CodeAlreadyExist", $"Price mode code {code} is already exist");

        public static Error NotFound() => new("PriceMode.NotFound", "Price mode not found");

        public static Error InUse() => new("PriceMode.InUse", "Price mode is in use");


    }
}
