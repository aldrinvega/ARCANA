namespace RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

public class UserRoleUntaggingException : Exception
{
    public UserRoleUntaggingException() : base("Cannot untag UserRole because there are User(s) associated with it."){}
}