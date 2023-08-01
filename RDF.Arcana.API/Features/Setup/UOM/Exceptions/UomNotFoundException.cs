namespace RDF.Arcana.API.Features.Setup.UOM.Exceptions;

public class UomNotFoundException : Exception
{
    public UomNotFoundException() : base("UOM not found"){}
}