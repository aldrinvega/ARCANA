using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Client.All;

public static class AllMappingExtension
{
    public static GetAllClients.GetAllClientResult
        ToGetAllClientResult(this Domain.Clients client)
    {
        return new GetAllClients.GetAllClientResult
        {
            Id = client.Id,
            CreatedAt = client.CreatedAt,
            RequestId = client.RequestId,
            Origin = client.Origin,
            OwnersName = client.Fullname,
            OwnersAddress = client.OwnersAddress != null
                    ? new GetAllClients.GetAllClientResult.OwnersAddressCollection
                    {
                        HouseNumber = client.OwnersAddress.HouseNumber,
                        StreetName = client.OwnersAddress.StreetName,
                        BarangayName = client.OwnersAddress.Barangay,
                        City = client.OwnersAddress.City,
                        Province = client.OwnersAddress.Province
                    }
                    : null,
                PhoneNumber = client.PhoneNumber,
                EmailAddress = client.EmailAddress,
                DateOfBirth = client.DateOfBirthDB,
                TinNumber = client.TinNumber,
                BusinessName = client.BusinessName,
                BusinessAddress = client.BusinessAddress != null
                    ? new GetAllClients.GetAllClientResult.BusinessAddressCollection
                    {
                        HouseNumber = client.BusinessAddress.HouseNumber,
                        StreetName = client.BusinessAddress.StreetName,
                        BarangayName = client.BusinessAddress.Barangay,
                        City = client.BusinessAddress.City,
                        Province = client.BusinessAddress.Province
                    }
                    : null,
                StoreType = client.StoreType.StoreTypeName,
                AuthorizedRepresentative = client.RepresentativeName,
                AuthorizedRepresentativePosition = client.RepresentativePosition,
                ClusterId = client.ClusterId,
                Longitude = client.Longitude,
                Latitude = client.Latitude,
                Requestor = client.AddedByUser.Fullname
        };
    }
}