﻿using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Users;

public static class MapperProfile
{
    public static GetUsersAsync.GetUserAsyncQueryResult
        ToGetUserAsyncQueryResult(this User user)
    {
        return new GetUsersAsync.GetUserAsyncQueryResult
        {
            Id = user.Id,
            FullIdNo = user.FullIdNo,
            Fullname = user.Fullname,
            Username = user.Username,
            Password = user.Password,
            AddedBy = user.AddedByUser?.Fullname,
            MobileNumber = user.MobileNumber,
            CreatedAt = user.CreatedAt,
            IsActive = user.IsActive,
            CompanyName = user.Company?.CompanyName,
            DepartmentName = user.Department?.DepartmentName,
            LocationName = user.Location?.LocationName,
            RoleName = user.UserRoles?.UserRoleName,
            Permission = user.UserRoles?.Permissions,
            ClusterId = user.CdoCluster?.ClusterId,
            Cluster = user.CdoCluster?.Cluster?.ClusterType
        };
    }
}