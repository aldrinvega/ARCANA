namespace RDF.Arcana.API.Features.Setup.Location;

public static class LocationMappingExtension
{
    public static GetAllLocationAsync.GetAllLocationAsyncResult
        ToGetAllLocationResult(this Domain.Location location)
    {
        return new GetAllLocationAsync.GetAllLocationAsyncResult
        {
            Id = location.Id,
            LocationName = location.LocationName,
            Users = location.Users.Select(x => x.Fullname),
            AddedBy = location.AddedByUser.Fullname,
            CreatedAt = location.CreatedAt.ToString("MM/dd/yyyy"),
            UpdatedAt = location.UpdatedAt.ToString(),
            IsActive = location.IsActive
        };
    }
}