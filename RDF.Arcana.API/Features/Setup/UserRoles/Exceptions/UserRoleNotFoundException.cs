namespace RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

public class UserRoleNotFoundException : Exception
{
    public UserRoleNotFoundException() : base("UserRole not found"){}
}