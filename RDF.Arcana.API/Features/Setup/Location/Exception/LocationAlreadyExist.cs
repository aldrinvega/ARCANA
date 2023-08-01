namespace RDF.Arcana.API.Features.Setup.Location.Exception;

public class LocationAlreadyExist : System.Exception
{
    public LocationAlreadyExist() : base("Location already exist"){}
}   