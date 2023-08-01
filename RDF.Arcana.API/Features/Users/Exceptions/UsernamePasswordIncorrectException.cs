namespace RDF.Arcana.API.Features.Users.Exception;

public class UsernamePasswordIncorrectException : System.Exception
{
    public UsernamePasswordIncorrectException() : base("Username or password is incorrect!"){}
}