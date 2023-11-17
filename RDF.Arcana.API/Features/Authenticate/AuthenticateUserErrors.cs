using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Authenticate;

public class AuthenticateUserErrors
{
    public static Error UsernamePasswordIncorrect() =>
        new Error("Authenticate.UsernamePasswordIncorrect", "Username or password is incorrect!");

    public static Error UnauthorizedAccess() =>
        new Error("Authenticate.UnauthorizedAccess", "You are not authorized to log in.");
    public static Error NoRole() =>
        new Error("Authenticate.NoRole", "There is no role assigned to this user. Contact admin");
}