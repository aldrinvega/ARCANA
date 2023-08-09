namespace RDF.Arcana.API.Features.Setup.Store_Type.Exception;

public class StoreTypeAlreadyExistException : System.Exception
{
    public StoreTypeAlreadyExistException(string name) : base($"{name} is already exist"){}
}