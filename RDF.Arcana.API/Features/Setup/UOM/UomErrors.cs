using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.UOM;

public class UomErrors
{
    public static Error AlreadyExist(string uom) => new Error("UOM.AlreadyExist", $"{uom} is already exist");
    public static Error NotFound() => new Error("UOM.NotFound", "No UOM found");
}