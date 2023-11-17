using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class UserRoleErrors
{
    public static Error CannotUntag(string module) => 
        new Error("UserRole.CannotUntag", $"The user with the role is currently an approver to the module {module}. " +
                  $"Prohibited to untag the module.");
}