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
            BusinessAddress =
                new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult.BusinessAddressCollection
                {
                    HouseNumber = directRegistrationClients.Client.BusinessAddress.HouseNumber,
                    StreetName = directRegistrationClients.Client.BusinessAddress.StreetName,
                    BarangayName = directRegistrationClients.Client.BusinessAddress.Barangay,
                    City = directRegistrationClients.Client.BusinessAddress.City,
                    Province = directRegistrationClients.Client.BusinessAddress.Province
                },
            PhoneNumber = directRegistrationClients.Client.PhoneNumber,
            OwnersAddress =
                new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult.OwnersAddressCollection
                {
                    HouseNumber = directRegistrationClients.Client.OwnersAddress.HouseNumber,
                    StreetName = directRegistrationClients.Client.OwnersAddress.StreetName,
                    BarangayName = directRegistrationClients.Client.OwnersAddress.Barangay,
                    City = directRegistrationClients.Client.OwnersAddress.City,
                    Province = directRegistrationClients.Client.OwnersAddress.Province
                },
            RepresentativeName = directRegistrationClients.Client.RepresentativeName,
            RepresentativePosition = directRegistrationClients.Client.RepresentativePosition,
            Cluster = directRegistrationClients.Client.Cluster,
            StoreType = directRegistrationClients.Client.StoreType.StoreTypeName,
            Freezer = directRegistrationClients.Client.Freezer,
            ClientType = directRegistrationClients.Client.ClientType,
            CustomerType = directRegistrationClients.Client.CustomerType,
            DirectDelivery = directRegistrationClients.Client.DirectDelivery,
            BookingCoverage = directRegistrationClients.Client.BookingCoverages.BookingCoverage,
            ModeOfPayment = directRegistrationClients.Client.ModeOfPayments.Payment,
            RegistrationStatus = directRegistrationClients.Client.RegistrationStatus,
            IsActive = directRegistrationClients.Client.IsActive,
            AddedBy = directRegistrationClients.RequestedByUser.Fullname,
            Longitude = directRegistrationClients.Client.Longitude,
            Latitude = directRegistrationClients.Client.Latitude,
            VariableDiscount = directRegistrationClients.Client.VariableDiscount,
            DiscountPercentage = directRegistrationClients.Client.FixedDiscounts.DiscountPercentage,
            Terms = directRegistrationClients.Client.Term.TermOptions.Select(x =>
                new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult.ClientTerms
                {
                    TermId = x.TermId,
                    Term = directRegistrationClients.Client.Term.TermType,
                    CreditLimit = x.CreditLimit,
                    TermDays = x.TermDays.Days
                }).ToList(),
            Attachments = directRegistrationClients.Client.ClientDocuments.Select(c =>
                new GetAllDirectRegistrationClients.GetAllDirectRegistrationClientsResult.ClientAttachments
                {
                    Attachment = c.DocumentPath
                }).ToList()
        };
    }
}