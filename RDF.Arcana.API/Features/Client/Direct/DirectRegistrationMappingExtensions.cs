using RDF.Arcana.API.Domain;
namespace RDF.Arcana.API.Features.Client.Direct;

public static class DirectRegistrationMappingExtensions
{
    public static GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult
        ToGetAllDirectRegistrationClientsResult(this Approvals directRegistrationClients)
    {
        return new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult
        {
            ClientId = directRegistrationClients.ClientId,
            Fullname = directRegistrationClients.Client.Fullname,
            BusinessName = directRegistrationClients.Client.BusinessName,
            BusinessAddress = new GetAllDirectRegistrationClients.
                GetAllDirectRegistrationClientsResult.
                BusinessAddressCollection
                {
                    HouseNumber = directRegistrationClients.Client.BusinessAddress.HouseNumber,
                    StreetName = directRegistrationClients.Client.BusinessAddress.StreetName,
                    BarangayName = directRegistrationClients.Client.BusinessAddress.Barangay,
                    City = directRegistrationClients.Client.BusinessAddress.City,
                    Province = directRegistrationClients.Client.BusinessAddress.Province
                },
            PhoneNumber = directRegistrationClients.Client.PhoneNumber,
            OwnersAddress = new GetAllDirectRegistrationClients.
                    GetAllDirectRegistrationClientsResult.
                    OwnersAddressCollection
                {
                    HouseNumber = directRegistrationClients.Client.OwnersAddress.HouseNumber,
                    StreetName = directRegistrationClients.Client.OwnersAddress.StreetName,
                    BarangayName = directRegistrationClients.Client.OwnersAddress.Barangay,
                    City = directRegistrationClients.Client.OwnersAddress.City,
                    Province = directRegistrationClients.Client.OwnersAddress.Province
                },
            RepresentativeName = directRegistrationClients.Client.RepresentativeName,
            RepresentativePosition = directRegistrationClients.Client.RepresentativePosition,
            Cluster = directRegistrationClients.Client.ClusterId,
            StoreType = directRegistrationClients.Client.StoreType.StoreTypeName,
            Freezer = directRegistrationClients.Client.Freezer,
            CustomerType = directRegistrationClients.Client.CustomerType,
            DirectDelivery = directRegistrationClients.Client.DirectDelivery,
            BookingCoverage = directRegistrationClients.Client.BookingCoverages.BookingCoverage,
            RegistrationStatus = directRegistrationClients.Client.RegistrationStatus,
            IsActive = directRegistrationClients.Client.IsActive,
            AddedBy = directRegistrationClients.RequestedByUser.Fullname,
            Longitude = directRegistrationClients.Client.Longitude,
            Latitude = directRegistrationClients.Client.Latitude,
            VariableDiscount = directRegistrationClients.Client.VariableDiscount,
            DiscountPercentage = directRegistrationClients.Client.FixedDiscounts.DiscountPercentage,
            Terms = directRegistrationClients.Client.Term != null
                ? new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult.ClientTerms
                {
                    TermId = directRegistrationClients.Client.Term.TermsId,
                    Term = directRegistrationClients.Client.Term.Terms.TermType,
                    CreditLimit = directRegistrationClients.Client.Term.CreditLimit,
                    TermDays = directRegistrationClients.Client.Term.TermDays.Days
                }
                : null,
            Attachments = directRegistrationClients.Client.ClientDocuments.Select(c =>
                new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult.ClientAttachments
                {
                    DocumentLink = c.DocumentPath
                }).ToList()
        };
    }
}