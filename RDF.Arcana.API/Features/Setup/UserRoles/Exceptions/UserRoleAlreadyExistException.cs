namespace RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

public class UserRoleAlreadyExistException : Exception
{
    public UserRoleAlreadyExistException() : base("UserRole already exist"){}
}