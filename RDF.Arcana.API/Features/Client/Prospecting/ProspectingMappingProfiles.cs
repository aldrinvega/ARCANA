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
            Address = requestedClient.Client.Address,
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
            Origin = approvedClient.CustomerType,
            AddedBy = approvedClient.Fullname,
            Address = approvedClient.Address,
            StoreType = approvedClient.StoreType.StoreTypeName,
            IsActive = approvedClient.IsActive,
            RegistrationStatus = approvedClient.RegistrationStatus,
            Freebies = freebies.Any()
                ? freebies.Select(fr => new GetAllApprovedProspectAsync.GetAllApprovedProspectResult.Freebie
                {
                    Status = fr.Status,
                    FreebieItems = fr.FreebieItems.Select(i =>
                        new GetAllApprovedProspectAsync.GetAllApprovedProspectResult.FreebieItem
                        {
                            Id = i.Id,
                            ItemCode = i.Items.ItemCode,
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
            Address = rejectClient.Client.Address,
            IsActive = rejectClient.IsActive,
            StoreType = rejectClient.Client.StoreType.StoreTypeName,
            Reason = rejectClient.Reason
        };
    }
}