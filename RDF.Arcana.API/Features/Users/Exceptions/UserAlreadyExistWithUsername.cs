namespace RDF.Arcana.API.Features.Users.Exceptions;

public class UserAlreadyExistWithUsername : System.Exception
{
    public UserAlreadyExistWithUsername(string username) : base($"{username} is already exist")
    {
    }
}