using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Location;

public class LocationErrors
{
    public static Error NotFound() => new Error("Location.NotFound", "No location found");

    public static Error AlreadyExist(string location) => new Error("Location.AlreadyExist", $"Location {location} already exist");
    
}