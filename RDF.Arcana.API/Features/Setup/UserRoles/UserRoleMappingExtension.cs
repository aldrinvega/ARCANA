namespace RDF.Arcana.API.Features.Setup.UserRoles;

public static class UserRoleMappingExtension
{
    public static GetUserRolesAsync.GetUserRoleAsyncResult
        ToGetUserRoleAsyncQueryResult(this Domain.UserRoles userRoles)
    {
        return new GetUserRolesAsync.GetUserRoleAsyncResult
        {
            Id = userRoles.Id,
            RoleName = userRoles.UserRoleName,
            CreatedAt = userRoles.CreatedAt,
            IsActive = userRoles.IsActive,
            UpdatedAt = userRoles.UpdatedAt,
            AddedBy = userRoles.AddedByUser.Fullname,
            Permissions = userRoles.Modules.Select(x => new GetUserRolesAsync.GetUserRoleAsyncResult.Module
            {
                ModuleId = x.Id,
                ModuleName = x.ModuleName
            }),
            IsTagged = userRoles.User != null,
            User = userRoles.User?.Fullname
        };
    }
}