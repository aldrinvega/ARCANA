namespace RDF.Arcana.API.Features.Users.Exception;

public class UserAlreadyExistException : System.Exception
{
    public UserAlreadyExistException(string username) : base($"{username} is already exist")
    {
    }
}