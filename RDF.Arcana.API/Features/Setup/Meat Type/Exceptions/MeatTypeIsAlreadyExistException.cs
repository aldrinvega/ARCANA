namespace RDF.Arcana.API.Features.Setup.Meat_Type.Exceptions;

public class MeatTypeIsAlreadyExistException : Exception
{
    public MeatTypeIsAlreadyExistException(string meatType) : base($"Meat Type {meatType} is already exist"){}
}