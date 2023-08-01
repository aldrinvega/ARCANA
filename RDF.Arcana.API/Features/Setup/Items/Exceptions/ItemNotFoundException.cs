namespace RDF.Arcana.API.Features.Setup.Items.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException() : base("Item not found"){}
}