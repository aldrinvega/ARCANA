namespace RDF.Arcana.API.Features.Users.Exceptions;

public class UsernamePasswordIncorrectException : System.Exception
{
    public UsernamePasswordIncorrectException() : base("Username or password is incorrect!")
    {
    }
}