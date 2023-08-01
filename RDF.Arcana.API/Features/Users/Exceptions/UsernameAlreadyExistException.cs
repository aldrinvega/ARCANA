namespace RDF.Arcana.API.Features.Users.Exceptions;

public class UsernameAlreadyExistException : System.Exception
{
    public UsernameAlreadyExistException() : base("Username already exist"){}
}