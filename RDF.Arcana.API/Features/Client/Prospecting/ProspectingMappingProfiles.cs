using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Prospecting.Approved;
using RDF.Arcana.API.Features.Client.Prospecting.Rejected;
using RDF.Arcana.API.Features.Client.Prospecting.Request;

namespace RDF.Arcana.API.Features.Client.Prospecting;

public static class ProspectingMappingProfiles
{
    public static GetAllRequestedProspectAsync.GetAllRequestedProspectResult
        ToGetAllRequestedProspectResult(this Approvals requestedClient)
    {
        return new GetAllRequestedProspectAsync.GetAllRequestedProspectResult
        {
            Id = requestedClient.ClientId,
            OwnersName = requestedClient.Client.Fullname,
            // CreatedAt = requestedClient.,
            BusinessName = requestedClient.Client.BusinessName,
            PhoneNumber = requestedClient.Client.PhoneNumber,
            CustomerType = requestedClient.Client.CustomerType,
            AddedBy = requestedClient.Client.AddedBy,
            OwnersAddress = new GetAllRequestedProspectAsync.GetAllRequestedProspectResult.OwnersAddressCollection
            {
                HouseNumber = requestedClient.Client.OwnersAddress.HouseNumber,
                StreetName = requestedClient.Client.OwnersAddress.StreetName,
                BarangayName = requestedClient.Client.OwnersAddress.Barangay,
                City = requestedClient.Client.OwnersAddress.City,
                Province = requestedClient.Client.OwnersAddress.Province
            },
            StoreType = requestedClient.Client.StoreType.StoreTypeName,
            IsActive = requestedClient.IsActive
        };
    }

    public static GetAllApprovedProspectAsync.GetAllApprovedProspectResult
        ToGetGetAllApprovedProspectResult(this Domain.Clients approvedClient)
    {
        var freebies = approvedClient.Approvals.SelectMany(a => a.FreebieRequest).ToList();

        return new GetAllApprovedProspectAsync.GetAllApprovedProspectResult
        {
            Id = approvedClient.Id,
            OwnersName = approvedClient.Fullname,
            BusinessName = approvedClient.BusinessName,
            PhoneNumber = approvedClient.PhoneNumber,
            EmailAddress = approvedClient.EmailAddress,
            Origin = approvedClient.Origin,
            AddedBy = approvedClient.AddedByUser.Fullname,
            OwnersAddress = new GetAllApprovedProspectAsync.GetAllApprovedProspectResult.OwnersAddressCollection
            {
                HouseNumber = approvedClient.OwnersAddress.HouseNumber,
                StreetName = approvedClient.OwnersAddress.StreetName,
                BarangayName = approvedClient.OwnersAddress.Barangay,
                City = approvedClient.OwnersAddress.City,
                Province = approvedClient.OwnersAddress.Province
            },
            StoreType = approvedClient.StoreType.StoreTypeName,
            IsActive = approvedClient.IsActive,
            RegistrationStatus = approvedClient.RegistrationStatus,
            Freebies = freebies.Any()
                ? freebies.Select(fr => new GetAllApprovedProspectAsync.GetAllApprovedProspectResult.Freebie
                {
                    FreebieRequestId = fr.Id,
                    Status = fr.Status,
                    TransactionNumber = fr.Id,
                    FreebieItems = fr.FreebieItems.Select(i =>
                        new GetAllApprovedProspectAsync.GetAllApprovedProspectResult.FreebieItem
                        {
                            Id = i.Id,
                            ItemId = i.ItemId,
                            ItemCode = i.Items.ItemCode,
                            ItemDescription = i.Items.ItemDescription,
                            UOM = i.Items.Uom.UomCode,
                            Quantity = i.Quantity
                        }).ToList()
                }).ToList()
                : null
        };
    }

    public static GetAllRejectProspectAsync.GetAllRejectProspectResult
        ToGetGetAllRejectProspectResult(this Approvals rejectClient)
    {
        return new GetAllRejectProspectAsync.GetAllRejectProspectResult
        {
            Id = rejectClient.ClientId,
            OwnersName = rejectClient.Client.Fullname,
            // CreatedAt = rejectClient.DateRejected,
            BusinessName = rejectClient.Client.BusinessName,
            PhoneNumber = rejectClient.Client.PhoneNumber,
            CustomerType = rejectClient.Client.CustomerType,
            AddedBy = rejectClient.Client.AddedBy,
            OwnersAddress = new GetAllRejectProspectAsync.GetAllRejectProspectResult.OwnersAddressCollection
            {
                HouseNumber = rejectClient.Client.OwnersAddress.HouseNumber,
                StreetName = rejectClient.Client.OwnersAddress.StreetName,
                BarangayName = rejectClient.Client.OwnersAddress.Barangay,
                City = rejectClient.Client.OwnersAddress.City,
                Province = rejectClient.Client.OwnersAddress.Province
            },
            IsActive = rejectClient.IsActive,
            StoreType = rejectClient.Client.StoreType.StoreTypeName,
            Reason = rejectClient.Reason
        };
    }
}