namespace RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

public class UserRoleDeactivationException : Exception
{
    public UserRoleDeactivationException()
        : base("Cannot deactivate UserRole because there are permissions associated with it.")
    {
    }

    public UserRoleDeactivationException(string message)
        : base(message)
    {
    }

    public UserRoleDeactivationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}