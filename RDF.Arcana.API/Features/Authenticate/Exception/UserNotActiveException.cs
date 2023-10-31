namespace RDF.Arcana.API.Features.Authenticate.Exception;

public class UserNotActiveException : System.Exception
{
    public UserNotActiveException() : base("You are not authorized to log in.")
    {
    }
}