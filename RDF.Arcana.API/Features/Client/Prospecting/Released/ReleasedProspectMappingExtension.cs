using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Client.Prospecting.Released;

public static class ReleasedProspectMappingExtension
{
    public static GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult
        GetAllReleasedProspectingRequestResult(this Approvals approvals)
    {
        return new GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult
        {
            Id = approvals.Client.Id,
            OwnersName = approvals.Client.Fullname,
            OwnersAddress =
                new GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult.OwnersAddressCollection
                {
                    HouseNumber = approvals.Client.OwnersAddress.HouseNumber,
                    StreetName = approvals.Client.OwnersAddress.StreetName,
                    BarangayName = approvals.Client.OwnersAddress.Barangay,
                    City = approvals.Client.OwnersAddress.City,
                    Province = approvals.Client.OwnersAddress.Province
                },
            PhoneNumber = approvals.Client.PhoneNumber,
            AddedBy = approvals.Client.RequestedByUser.Fullname,
            CustomerType = approvals.Client.CustomerType,
            BusinessName = approvals.Client.BusinessName,
            CreatedAt = approvals.Client.CreatedAt,
            IsActive = approvals.Client.IsActive,
            RegistrationStatus = approvals.Client.RegistrationStatus,

            // Assuming TransactionNumber, PhotoProofPath, and ESignaturePath are similar for each FreebieRequest in the collection
            TransactionNumber = approvals.FreebieRequest.FirstOrDefault()?.Id,
            PhotoProofPath = approvals.FreebieRequest.FirstOrDefault()?.PhotoProofPath,
            ESignaturePath = approvals.FreebieRequest.FirstOrDefault()?.ESignaturePath,

            Freebies = approvals.FreebieRequest
                .SelectMany(x => x.FreebieItems)
                .Select(fi => new GetAllReleasedProspectingRequest.GetAllReleasedProspectingRequestResult.Freebie
                {
                    Id = fi.RequestId,
                    ItemCode = fi.Items.ItemCode,
                    Quantity = fi.Quantity,
                }).ToList()
        };
    }
}