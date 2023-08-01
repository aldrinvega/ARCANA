namespace RDF.Arcana.API.Features.Setup.Company.Exceptions;

public class NoCompanyFoundException : Exception
{
    public NoCompanyFoundException() : base("No company fround"){ }
}