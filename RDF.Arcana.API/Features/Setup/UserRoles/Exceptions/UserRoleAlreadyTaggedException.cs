namespace RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

public class UserRoleAlreadyTaggedException : Exception
{
    public UserRoleAlreadyTaggedException() : base("User Role is already tagged"){}
}