using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class UserRoleErrors
{
    public static Error CannotUntag(string module) => 
        new Error("UserRole.CannotUntag", $"The user with the role is currently an approver to the module {module}. " +
                  $"Prohibited to untag the module.");
    public static Error AlreadyExist(string userRole) => 
        new Error("UserRole.AlreadyExist", $"{userRole} is already exist");

    public static Error CannotArchive(string name) => new Error("UserRole.CannotArchive",
        $"User Role cannot be archived because it is currently associated with the user '{name}'");

    public static Error NotFound() => new Error("UserRole.NotFound", "No user role found");
}