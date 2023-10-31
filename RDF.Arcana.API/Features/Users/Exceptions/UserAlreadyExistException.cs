namespace RDF.Arcana.API.Features.Users.Exception;

public class UserAlreadyExistException : System.Exception
{
    public UserAlreadyExistException() : base($"This user is already exist")
    {
    }
}