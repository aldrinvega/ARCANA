namespace RDF.Arcana.API.Features.Setup.Term_Days.Exceptions;

public class TermDaysAlreadyExist : Exception
{
    public TermDaysAlreadyExist() : base("Term days already exist"){}
}