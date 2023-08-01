namespace RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

public class MeatTypeNotFoundException : Exception
{
    public MeatTypeNotFoundException() : base("Meat Type not found"){}
}