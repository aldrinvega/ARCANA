namespace RDF.Arcana.API.Features.Users.Exceptions;

public class NoUserFoundException : System.Exception
{
    public NoUserFoundException() : base("No User Found"){}
}