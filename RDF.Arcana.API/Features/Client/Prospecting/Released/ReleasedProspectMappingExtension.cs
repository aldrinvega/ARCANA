using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Released;

public static class ReleasedProspectMappingExtension
{
    public static GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult
        GetAllReleasedProspectingRequestResult ( this Approvals approvals)
    {
        return new GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult
        {
            Id = approvals.Client.Id,
            OwnersName = approvals.Client.Fullname,
            Address = approvals.Client.Address,
            PhoneNumber = approvals.Client.PhoneNumber,
            AddedBy = approvals.Client.RequestedByUser.Fullname,
            CustomerType = approvals.Client.CustomerType,
            BusinessName = approvals.Client.BusinessName,
            CreatedAt = approvals.Client.CreatedAt,
            IsActive = approvals.Client.IsActive,
            RegistrationStatus = approvals.Client.RegistrationStatus,
            TransactionNumber = approvals.FreebieRequest.TransactionNumber,
            PhotoProofPath = approvals.FreebieRequest.PhotoProofPath,
            ESignaturePath = approvals.FreebieRequest.ESignaturePath,
            Freebies = approvals.FreebieRequest.FreebieItems.Select(x =>
                new GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult.Freebie
                {
                    Id = x.RequestId,
                    ItemCode = x.Items.ItemCode,
                    Quantity = x.Quantity,
            
                }).ToList()
        };
    }
}