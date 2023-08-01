namespace RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

public class TermDaysNotFoundException : Exception
{
    public TermDaysNotFoundException() : base("Term days not found"){}
}