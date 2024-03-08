using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Price_Mode
{
    public static class PriceModeItemsErrors
    {
        public static Error AlreadyExist(string itemCode) => new("PriceModeItems.AlreadyExist", $"Item \"{itemCode}\" is already exist.");
        public static Error NotFound() => new("PriceModeItems.NotFound", "Item not found");    
    }
}
