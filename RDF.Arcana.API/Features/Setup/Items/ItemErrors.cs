using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Items;

public class ItemErrors
{
    public static Error NotFound(int id) => new Error("Item.notFound", $"Item Id: {id} is not found.");
    public static Error AlreadyExist(string itemCode) => new Error("Item.notFound", $"Item: {itemCode} is already exist.");
}