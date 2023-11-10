using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Items;

public class ItemErrors
{
    public static Error NotFound(int id) => new Error("Item.notFound", $"Item ID {id} is not found.");
}