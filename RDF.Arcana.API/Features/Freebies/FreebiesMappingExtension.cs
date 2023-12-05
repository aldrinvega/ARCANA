using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

public static class FreebiesMappingExtension
{
    public static GetRequestedFreebies.GetRequestedFreebiesQueryResultCollection
        ToGetRequestedFreebiesQueryResult(this Approvals freebies)
    {
        return new GetRequestedFreebies.GetRequestedFreebiesQueryResultCollection
        {
            GetRequestedFreebiesQueryResults = freebies.FreebieRequest?.Select(freebieRequest =>
                new GetRequestedFreebies.GetRequestedFreebiesQueryResult
                {
                    FreebieRequestId = freebieRequest.Id,
                    ClientId = freebies.ClientId,
                    OwnersName = freebies.Client.Fullname,
                    PhoneNumber = freebies.Client.PhoneNumber,
                    OwnersAddress = new GetRequestedFreebies.GetRequestedFreebiesQueryResult.OwnersAddressCollection
                    {
                        HouseNumber = freebies.Client.OwnersAddress.HouseNumber,
                        StreetName = freebies.Client.OwnersAddress.StreetName,
                        BarangayName = freebies.Client.OwnersAddress.Barangay,
                        City = freebies.Client.OwnersAddress.City,
                        Province = freebies.Client.OwnersAddress.Province
                    },
                    TransactionNumber = freebieRequest.Id,
                    Freebies = freebieRequest.FreebieItems.Select(x =>
                        new GetRequestedFreebies.GetRequestedFreebiesQueryResult.Freebie
                        {
                            Id = x.RequestId,
                            ItemCode = x.Items.ItemCode,
                            Quantity = x.Quantity,
                        }).ToList(),
                }).ToList()
        };
    }

    public static GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResultCollection
        ToGetApprovedFreebiesQueryResult(this Approvals freebies)
    {
        return new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResultCollection
        {
            GetAllApprovedFreebiesQueryResults = freebies.FreebieRequest.Select(freebieRequest =>
                new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult
                {
                    FreebieRequestId = freebieRequest.Id,
                    ClientId = freebies.ClientId,
                    OwnersName = freebies.Client.Fullname,
                    PhoneNumber = freebies.Client.PhoneNumber,
                    OwnersAddress = new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult.OwnersAddressCollection
                    {
                        HouseNumber = freebies.Client.OwnersAddress.HouseNumber,
                        StreetName = freebies.Client.OwnersAddress.StreetName,
                        BarangayName = freebies.Client.OwnersAddress.Barangay,
                        City = freebies.Client.OwnersAddress.City,
                        Province = freebies.Client.OwnersAddress.Province
                    },
                    TransactionNumber = freebieRequest.Id,
                    Freebies = freebieRequest.FreebieItems.Select(x =>
                        new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult.Freebie
                        {
                            Id = x.RequestId,
                            ItemCode = x.Items.ItemCode,
                            Quantity = x.Quantity,
                        }).ToList(),
                }).ToList()
        };
    }

    public static GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResultCollection
        ToGetAllRejectedFreebiesQueryResultCollection(this Approvals freebies)
    {
        return new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResultCollection
        {
            GetAllRejectedFreebiesQueryResults = freebies.FreebieRequest.Select(freebieRequest =>
                new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult
                {
                    FreebieRequestId = freebieRequest.Id,
                    ClientId = freebies.ClientId,
                    OwnersName = freebies.Client.Fullname,
                    PhoneNumber = freebies.Client.PhoneNumber,
                    OwnersAddress = new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult.OwnersAddressCollection
                    {
                        HouseNumber = freebies.Client.OwnersAddress.HouseNumber,
                        StreetName = freebies.Client.OwnersAddress.StreetName,
                        BarangayName = freebies.Client.OwnersAddress.Barangay,
                        City = freebies.Client.OwnersAddress.City,
                        Province = freebies.Client.OwnersAddress.Province
                    },
                    TransactionNumber = freebieRequest.Id,
                    Freebies = freebieRequest.FreebieItems.Select(x =>
                        new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult.Freebie
                        {
                            Id = x.RequestId,
                            ItemCode = x.Items.ItemCode,
                            Quantity = x.Quantity,
                        }).ToList(),
                }).ToList()
        };
    }

    public static GetAllFreebies.GetAllFreebiesResult
        ToGetAllFreebiesResult(this Domain.Clients freebies)
    {
        return new GetAllFreebies.GetAllFreebiesResult
        {
            ClientId = freebies.Id,
            OwnersName = freebies.Fullname,
            PhoneNumber = freebies.PhoneNumber,
            OwnersAddress = new GetAllFreebies.GetAllFreebiesResult.OwnersAddressCollection
            {
                HouseNumber = freebies.OwnersAddress.HouseNumber,
                StreetName = freebies.OwnersAddress.StreetName,
                BarangayName = freebies.OwnersAddress.Barangay,
                City = freebies.OwnersAddress.City,
                Province = freebies.OwnersAddress.Province
            },
            TransactionNumber = freebies.FreebiesRequests?.FirstOrDefault()?.Id,
            Status = freebies.FreebiesRequests?.FirstOrDefault()?.Status ?? "For freebie request",
            FreebieRequestId = freebies.FreebiesRequests?.FirstOrDefault()?.Id,
            Freebies = freebies.FreebiesRequests?.SelectMany(fr => fr.FreebieItems?.Select(fi =>
                new GetAllFreebies.GetAllFreebiesResult.Freebie
                {
                    Id = fi.Id,
                    ItemCode = fi.Items.ItemCode,
                    Quantity = fi.Quantity
                }) ?? new List<GetAllFreebies.GetAllFreebiesResult.Freebie>()).ToList(),
            FreebieApprovalHistories = freebies.Request.Approvals
                ?.OrderByDescending(a => a.CreatedAt)
                .Select( a => new GetAllFreebies.GetAllFreebiesResult.FreebieApprovalHistory
                {
                    Module = a.Request.Module,
                    Approver = a.Approver.Fullname,
                    CreatedAt = a.CreatedAt,
                    Status = a.Status,
                })
        };
    }
}