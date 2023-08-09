namespace RDF.Arcana.API.Features.Setup.Store_Type.Exception;

public class StoreTypeNotFoundException : System.Exception
{
    public StoreTypeNotFoundException() : base("Store type not found"){}
}