using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Authenticate;

public static class AuthenticateMappingExtension
{
    public static AuthenticateUser.AuthenticateUserResult
        ToGetAuthenticatedUserResult(this User user, string token)
    {
        return new AuthenticateUser.AuthenticateUserResult(user, token);
    }
}