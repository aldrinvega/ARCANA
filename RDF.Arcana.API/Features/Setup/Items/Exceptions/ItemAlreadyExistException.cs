namespace RDF.Arcana.API.Features.Setup.Items.Exceptions;

public class ItemAlreadyExistException : Exception
{
    public ItemAlreadyExistException() : base("Item is already exist"){}
}