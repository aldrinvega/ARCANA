namespace RDF.Arcana.API.Features.Client.All;

public static class AllMappingExtension
{
    public static GetAllClients.GetAllClientResult
        ToGetAllClientResult(this Domain.Clients client)
    {
        return new GetAllClients.GetAllClientResult
        {
            Id = client.Id,
                RequestId = client.RequestId,
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
                Cluster = client.Cluster,
                Freezer = client.Freezer,
                TypeOfCustomer = client.CustomerType,
                DirectDelivery = client.DirectDelivery,
                BookingCoverage = client.BookingCoverages.BookingCoverage,
                Terms = client.Term != null
                    ? new GetAllClients.GetAllClientResult.ClientTerms
                    {
                        TermId = client.Term.TermsId,
                        Term = client.Term.Terms.TermType,
                        CreditLimit = client.Term.CreditLimit,
                        TermDaysId = client.Term.TermDaysId,
                        TermDays = client.Term.TermDays?.Days
                    }
                    : null,
                FixedDiscount = client.FixedDiscounts != null
                    ? new GetAllClients.GetAllClientResult.FixedDiscounts
                    {
                        DiscountPercentage = client.FixedDiscounts.DiscountPercentage
                    }
                    : null,
                VariableDiscount = client.VariableDiscount,
                Longitude = client.Longitude,
                Latitude = client.Latitude,
                RequestedBy = client.AddedByUser.Fullname,
                Attachments = client.ClientDocuments.Select(cd =>
                    new GetAllClients.GetAllClientResult.Attachment
                    {
                        DocumentId = cd.Id,
                        DocumentLink = cd.DocumentPath,
                        DocumentType = cd.DocumentType
                    }),
                ClientApprovalHistories = client.Request.Approvals == null ? null :
                    client.Request.Approvals.OrderByDescending(a => a.CreatedAt)
                    .Select( a => new GetAllClients.GetAllClientResult.ClientApprovalHistory
                    {
                        Module = a.Request.Module,
                        Approver = a.Approver.Fullname,
                        CreatedAt = a.CreatedAt,
                        Status = a.Status,
                        Level = a.Approver.Approver.FirstOrDefault().Level,
                        Reason = a.Reason
                    })
        };
    }
}