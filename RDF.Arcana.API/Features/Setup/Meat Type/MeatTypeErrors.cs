using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Meat_Type;

public class MeatTypeErrors
{
    public static Error NotFound() => new Error("MeatType.NotFound", "No meat type found");

    public static Error AlreadyExist(string meatType) => new Error("MeatType.AlreadyExist", $"{meatType} is already exist");
    
    
}