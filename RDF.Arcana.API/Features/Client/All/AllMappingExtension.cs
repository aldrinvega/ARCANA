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
                ModeOfPayments = client.ClientModeOfPayment.Select(mop => new GetAllClients.GetAllClientResult.ModeOfPayment
                {
                    Id = mop.ModeOfPaymentId
                }),
                StoreType = client.StoreType.StoreTypeName,
                AuthorizedRepresentative = client.RepresentativeName,
                AuthorizedRepresentativePosition = client.RepresentativePosition,
                ClusterId = client.ClusterId,
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
                        TermDays = client.Term.TermDays.Days
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
                        
                    }),
                UpdateHistories = client.Request.UpdateRequestTrails == null ? null :
                    client.Request.UpdateRequestTrails.Select(uh => new GetAllClients.GetAllClientResult.UpdateHistory
                    {
                        Module = uh.ModuleName,
                        UpdatedAt = uh.UpdatedAt
                    }),
                Freebies = client.FreebiesRequests
                    .Where(fr => fr.Status == Status.Approved || fr.Status == Status.Released)
                    .Select(x => new GetAllClients.GetAllClientResult.FreebiesCollection
                {
                    TransactionNumber = x.Id,
                    FreebieRequestId = x.Id,
                    Status = x.Status,
                    ESignature = x.ESignaturePath,
                    Freebies = x.FreebieItems.Select(x => new GetAllClients.GetAllClientResult.Items
                    {
                        Id = x.Id,
                        ItemCode = x.Items.ItemCode,
                        ItemDescription = x.Items.ItemDescription,
                            Uom = x.Items.Uom.UomCode,
                        Quantity = x.Quantity
                    })
                }),
                ListingFees = client.ListingFees.Select( lf => new GetAllClients.GetAllClientResult.ListingFeeCollection
                {
                    Id = lf.Id,
                    RequestId = lf.RequestId,
                    Total = lf.Total,
                    Status = lf.Status,
                    ApprovalDate = lf.ApprovalDate.ToString("MM/dd/yyyy HH:mm:ss"),
                    ListingItems = lf.ListingFeeItems.Select(lfi => new GetAllClients.GetAllClientResult.ListingItems
                    {
                        Id = lfi.Id,
                        ItemCode = lfi.Item.ItemCode,
                        ItemDescription = lfi.Item.ItemDescription,
                        Sku = lfi.Sku,
                        UnitCost = lfi.UnitCost,
                        Uom = lfi.Item.Uom.UomCode
                    })
                }),
                Approvers = client.Request.RequestApprovers.Select(x => new GetAllClients.GetAllClientResult.RequestApproversForClients
                {
                    Name = x.Approver.Fullname,
                    Level = x.Level
                })
        };
    }
}