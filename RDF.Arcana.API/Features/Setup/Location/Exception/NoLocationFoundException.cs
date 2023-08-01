namespace RDF.Arcana.API.Features.Setup.Location.Exception;

public class NoLocationFoundException : System.Exception
{
    public NoLocationFoundException() : base("No location found"){}
}