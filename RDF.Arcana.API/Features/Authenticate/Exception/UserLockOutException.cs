namespace RDF.Arcana.API.Features.Authenticate.Exception;

public class UserLockoutException : System.Exception
{
    public UserLockoutException(int lockoutTimeRemaining)
        : base($"User account is locked. Please try again in {lockoutTimeRemaining} minutes.")
    {
        LockoutTimeRemaining = TimeSpan.FromMinutes(lockoutTimeRemaining);
    }

    public TimeSpan LockoutTimeRemaining { get; }
}