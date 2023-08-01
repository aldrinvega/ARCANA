using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Clients.Prospecting;

public static class ProspectingMappingProfiles
{
    public static GetAllRequestedProspectAsync.GetAllRequestedProspectResult
        ToGetGetAllProspectResult(this RequestedClient requestedClient)
    {
        return new GetAllRequestedProspectAsync.GetAllRequestedProspectResult
        {
            Id = requestedClient.ClientId,
            OwnersName = requestedClient.Client.OwnersName,
            CreatedAt = requestedClient.DateRequest,
            BusinessName = requestedClient.Client.BusinessName,
            PhoneNumber = requestedClient.Client.PhoneNumber,
            CustomerType = requestedClient.Client.CustomerType,
            AddedBy = requestedClient.Client.AddedBy,
            Address = requestedClient.Client.OwnersAddress
        };
    }
    
    public static GetAllApprovedProspectAsync.GetAllApprovedProspectResult
        ToGetGetAllApprovedProspectResult(this ApprovedClient approvedClient)
    {
        return new GetAllApprovedProspectAsync.GetAllApprovedProspectResult
        {
            Id = approvedClient.ClientId,
            OwnersName = approvedClient.Client.OwnersName,
            CreatedAt = approvedClient.DateApproved,
            BusinessName = approvedClient.Client.BusinessName,
            PhoneNumber = approvedClient.Client.PhoneNumber,
            CustomerType = approvedClient.Client.CustomerType,
            AddedBy = approvedClient.Client.AddedBy,
            Address = approvedClient.Client.OwnersAddress
        };
    }
    
    public static GetAllRejectProspectAsync.GetAllRejectProspectResult
        ToGetGetAllRejectProspectResult(this RejectedClients rejectClient)
    {
        return new GetAllRejectProspectAsync.GetAllRejectProspectResult
        {
            Id = rejectClient.ClientId,
            OwnersName = rejectClient.Client.OwnersName,
            CreatedAt = rejectClient.DateRejected,
            BusinessName = rejectClient.Client.BusinessName,
            PhoneNumber = rejectClient.Client.PhoneNumber,
            CustomerType = rejectClient.Client.CustomerType,
            AddedBy = rejectClient.Client.AddedBy,
            Address = rejectClient.Client.OwnersAddress
        };
    }
}