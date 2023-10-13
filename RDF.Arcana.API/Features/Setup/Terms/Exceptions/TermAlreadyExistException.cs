namespace RDF.Arcana.API.Features.Setup.Terms.Exceptions;

public class TermAlreadyExistException : Exception
{
    public TermAlreadyExistException(string term) : base($"{term} is already exist.")
    {
    }
}