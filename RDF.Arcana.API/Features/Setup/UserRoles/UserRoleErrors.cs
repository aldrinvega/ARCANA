using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

public class UserRoleErrors
{
    public static Error CannotUntag(string module) => 
        new("UserRole.CannotUntag", $"The user with the role is currently an approver to the module {module}. " +
                  $"Prohibited to untag the module.");
    public static Error AlreadyExist(string userRole) => 
        new("UserRole.AlreadyExist", $"{userRole} is already exist");

    public static Error CannotArchive(string name) => new("UserRole.CannotArchive",
        $"User Role cannot be archived because it is currently associated with the user '{name}'");

    public static Error NotFound() => new("UserRole.NotFound", "No user role found");

    public static Error WithPermission() => new("userRole.WithPermission"
        , "Cannot deactivate UserRole because there are permissions associated with it.");

    public static Error Exception(string message) => new("UserRole.Exception", $"Error adding user role: {message}");
}