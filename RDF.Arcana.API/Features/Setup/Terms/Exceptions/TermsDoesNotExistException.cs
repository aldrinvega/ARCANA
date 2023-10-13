namespace RDF.Arcana.API.Features.Setup.Terms.Exceptions;

public class TermsDoesNotExistException : Exception
{
    public TermsDoesNotExistException(int terms) : base($"Terms with id {terms} does not exist")
    {
    }
}