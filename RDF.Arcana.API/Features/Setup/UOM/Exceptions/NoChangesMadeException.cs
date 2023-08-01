namespace RDF.Arcana.API.Features.Setup.UOM.Exceptions;

public class NoChangesMadeException : Exception
{
    public NoChangesMadeException() : base("No Changes Made"){}
}