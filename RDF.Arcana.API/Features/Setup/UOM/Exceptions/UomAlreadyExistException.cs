namespace RDF.Arcana.API.Features.Setup.UOM.Exceptions;

public class UomAlreadyExistException : Exception
{
    public UomAlreadyExistException() : base("UOM already exist, try something else"){}
}