using RDF.Arcana.API.Features.Client.All;
using RDF.Arcana.API.Features.Client.Prospecting.All;
using RDF.Arcana.API.Features.Listing_Fee;

namespace RDF.Arcana.API.Features.Client.Regular;

public static class RegularClientsMappingProfile
{
    public static GetAllRegularClients.GetAllRegularClientResult
        ToGetAllRegularClientResult(this Domain.Clients regularClients)
    {
        return new GetAllRegularClients.GetAllRegularClientResult
        {
            OwnersName = regularClients.Fullname,
            OwnersAddress = new GetAllRegularClients.GetAllRegularClientResult.OwnersAddressCollection
            {
                HouseNumber = regularClients.OwnersAddress.HouseNumber,
                StreetName = regularClients.OwnersAddress.StreetName,
                BarangayName = regularClients.OwnersAddress.Barangay,
                City = regularClients.OwnersAddress.City,
                Province = regularClients.OwnersAddress.Province
            },
            PhoneNumber = regularClients.PhoneNumber,
            DateOfBirth = regularClients.DateOfBirthDB,
            TinNumber = regularClients.TinNumber,
            BusinessName = regularClients.BusinessName,
            BusinessAddress = new GetAllRegularClients.GetAllRegularClientResult.BusinessAddressCollection
            {
                HouseNumber = regularClients.BusinessAddress.HouseNumber,
                StreetName = regularClients.BusinessAddress.StreetName,
                BarangayName = regularClients.BusinessAddress.Barangay,
                City = regularClients.BusinessAddress.City,
                Province = regularClients.BusinessAddress.Province
            },
            StoreType = regularClients.StoreType.StoreTypeName,
            AuthorizedRepresentative = regularClients.RepresentativeName,
            AuthorizedRepresentativePosition = regularClients.RepresentativePosition,
            Cluster = regularClients.Cluster,
            Freezer = regularClients.Freezer,
            TypeOfCustomer = regularClients.CustomerType,
            DirectDelivery = regularClients.DirectDelivery,
            BookingCoverage = regularClients.BookingCoverages.BookingCoverage,
            ModeOfPayment = regularClients.ModeOfPayments.Payment,
            Terms = regularClients.Terms != null
                ? new GetAllRegularClients.GetAllRegularClientResult.ClientTerms
                {
                    TermId = regularClients.Term.TermsId,
                    Term = regularClients.Term.Terms.TermType,
                    CreditLimit = regularClients.Term.CreditLimit,
                    TermDays = regularClients.Term.TermDays.Days
                }
                : null,
            FixedDiscount = new GetAllRegularClients.GetAllRegularClientResult.FixedDiscounts
            {
                DiscountPercentage = regularClients.FixedDiscounts.DiscountPercentage
            },
            VariableDiscount = regularClients.VariableDiscount,
            Longitude = regularClients.Longitude,
            Latitude = regularClients.Latitude,
            Attachments = regularClients.ClientDocuments.Select(cd =>
                new GetAllRegularClients.GetAllRegularClientResult.Attachment
                {
                    DocumentId = cd.Id,
                    DocumentLink = cd.DocumentPath,
                    DocumentType = cd.DocumentType
                })
        };
    }

    public static GetAllProspects.GetAllProspectQueryResult
        ToGetAllProspectResult(this Domain.Clients client)
    {
        return new GetAllProspects.GetAllProspectQueryResult
        {
            OwnersName = client.Fullname,
            OwnersAddress = client.OwnersAddress != null
                ? new GetAllProspects.GetAllProspectQueryResult.OwnersAddressCollection
                {
                    HouseNumber = client.OwnersAddress?.HouseNumber,
                    StreetName = client.OwnersAddress?.StreetName,
                    BarangayName = client.OwnersAddress?.Barangay,
                    City = client.OwnersAddress?.City,
                    Province = client.OwnersAddress?.Province
                }
                : null,
            PhoneNumber = client.PhoneNumber,
            DateOfBirth = client.DateOfBirthDB,
            TinNumber = client.TinNumber,
            BusinessName = client.BusinessName,
            BusinessAddress = client.BusinessAddress != null
                ? new GetAllProspects.GetAllProspectQueryResult.BusinessAddressCollection
                {
                    HouseNumber = client.BusinessAddress?.HouseNumber,
                    StreetName = client.BusinessAddress?.StreetName,
                    BarangayName = client.BusinessAddress?.Barangay,
                    City = client.BusinessAddress?.City,
                    Province = client.BusinessAddress?.Province
                }
                : null,
            StoreType = client.StoreType?.StoreTypeName,
            AuthorizedRepresentative = client.RepresentativeName,
            AuthorizedRepresentativePosition = client.RepresentativePosition,
            Cluster = client.Cluster, // Ensure a default value if Cluster can be null
            Freezer = client.Freezer, // Ensure a default value if Freezer can be null
            TypeOfCustomer = client.CustomerType,
            DirectDelivery = client.DirectDelivery, // Ensure a default value if DirectDelivery can be null
            BookingCoverage = client.BookingCoverages?.BookingCoverage,
            ModeOfPayment = client.ModeOfPayments?.Payment,
            Terms = client.Term != null
                ? new GetAllProspects.GetAllProspectQueryResult.ClientTerms
                {
                    TermId = client.Term.TermsId,
                    Term = client.Term.Terms.TermType,
                    CreditLimit = client.Term.CreditLimit,
                    TermDays = client.Term.TermDays.Days
                }
                : null,
            FixedDiscount = client.FixedDiscounts != null
                ? new GetAllProspects.GetAllProspectQueryResult.FixedDiscounts
                {
                    DiscountPercentage = client.FixedDiscounts.DiscountPercentage
                }
                : null,
            VariableDiscount = client.VariableDiscount, // Ensure a default value if VariableDiscount can be null
            Longitude = client.Longitude,
            Latitude = client.Latitude,
            Attachments = client.ClientDocuments?.Select(cd =>
                new GetAllProspects.GetAllProspectQueryResult.Attachment
                {
                    DocumentId = cd.Id,
                    DocumentLink = cd.DocumentPath,
                    DocumentType = cd.DocumentType
                })
        };
    }

    public static GetAllClients.GetAllClientResult
        ToGetAllClientResult(this Domain.Clients client)
    {
        return new GetAllClients.GetAllClientResult
        {
            Id = client.Id,
            OwnersName = client.Fullname,
            OwnersAddress = client.OwnersAddress != null
                ? new GetAllClients.GetAllClientResult.OwnersAddressCollection
                {
                    HouseNumber = client.OwnersAddress?.HouseNumber,
                    StreetName = client.OwnersAddress?.StreetName,
                    BarangayName = client.OwnersAddress?.Barangay,
                    City = client.OwnersAddress?.City,
                    Province = client.OwnersAddress?.Province
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
                    HouseNumber = client.BusinessAddress?.HouseNumber,
                    StreetName = client.BusinessAddress?.StreetName,
                    BarangayName = client.BusinessAddress?.Barangay,
                    City = client.BusinessAddress?.City,
                    Province = client.BusinessAddress?.Province
                }
                : null,
            StoreType = client.StoreType?.StoreTypeName,
            AuthorizedRepresentative = client.RepresentativeName,
            AuthorizedRepresentativePosition = client.RepresentativePosition,
            Cluster = client.Cluster, // Ensure a default value if Cluster can be null
            Freezer = client.Freezer, // Ensure a default value if Freezer can be null
            TypeOfCustomer = client.CustomerType,
            DirectDelivery = client.DirectDelivery, // Ensure a default value if DirectDelivery can be null
            BookingCoverage = client.BookingCoverages?.BookingCoverage,
            ModeOfPayment = client.ModeOfPayments?.Payment,
            Terms = client.Term != null
                ? new GetAllClients.GetAllClientResult.ClientTerms
                {
                    TermId = client.Term.TermsId,
                    Term = client.Term.Terms.TermType,
                    CreditLimit = client.Term.CreditLimit,
                    TermDays = client.Term.TermDays?.Days
                }
                : null,
            FixedDiscount = client.FixedDiscounts != null
                ? new GetAllClients.GetAllClientResult.FixedDiscounts
                {
                    DiscountPercentage = client.FixedDiscounts.DiscountPercentage
                }
                : null,
            VariableDiscount = client.VariableDiscount, // Ensure a default value if VariableDiscount can be null
            Longitude = client.Longitude,
            Latitude = client.Latitude,
            RequestedBy = client.AddedByUser.Fullname,
            Attachments = client.ClientDocuments?.Select(cd =>
                new GetAllClients.GetAllClientResult.Attachment
                {
                    DocumentId = cd.Id,
                    DocumentLink = cd.DocumentPath,
                    DocumentType = cd.DocumentType
                })
        };
    }

    public static GetAllClientsInListingFee.GetAllClientsInListingFeeResult
        ToGetAllClientsInListingFeeResult(this Domain.Clients client)
    {
        return new GetAllClientsInListingFee.GetAllClientsInListingFeeResult
        {
            Id = client.Id,
            OwnersName = client.Fullname,
            BusinessName = client.BusinessName,
        };
    }
}