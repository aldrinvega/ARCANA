using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

public static class FreebiesMappingExtension
{
    public static GetRequestedFreebies.GetRequestedFreebiesQueryResultCollection
        ToGetRequestedFreebiesQueryResult(this Approvals freebies)
    {
        var status = freebies.FreebieRequest == null ? "For freebie request" : "Freebie Releasing";
        return new GetRequestedFreebies.GetRequestedFreebiesQueryResultCollection
        {
            GetRequestedFreebiesQueryResults = freebies.FreebieRequest.Select(freebieRequest =>
                new GetRequestedFreebies.GetRequestedFreebiesQueryResult
                {
                    FreebieRequestId = freebieRequest.Id,
                    ClientId = freebies.ClientId,
                    OwnersName = freebies.Client.Fullname,
                    PhoneNumber = freebies.Client.PhoneNumber,
                    OwnersAddress = freebies.Client.Address,
                    TransactionNumber = freebieRequest.TransactionNumber,
                    Freebies = freebieRequest.FreebieItems.Select(x =>
                        new GetRequestedFreebies.GetRequestedFreebiesQueryResult.Freebie
                        {
                            Id = x.RequestId,
                            ItemCode = x.Items.ItemCode,
                            Quantity = x.Quantity,
                        }).ToList(),
                    // DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
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
                    OwnersAddress = freebies.Client.Address,
                    TransactionNumber = freebieRequest.TransactionNumber,
                    Freebies = freebieRequest.FreebieItems.Select(x =>
                        new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult.Freebie
                        {
                            Id = x.RequestId,
                            ItemCode = x.Items.ItemCode,
                            Quantity = x.Quantity,
                        }).ToList(),
                    // DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
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
                    OwnersAddress = freebies.Client.Address,
                    TransactionNumber = freebieRequest.TransactionNumber,
                    Freebies = freebieRequest.FreebieItems.Select(x =>
                        new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult.Freebie
                        {
                            Id = x.RequestId,
                            ItemCode = x.Items.ItemCode,
                            Quantity = x.Quantity,
                        }).ToList(),
                    // DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
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
            OwnersAddress = freebies.Address,
            TransactionNumber = freebies.FreebiesRequests?.FirstOrDefault()?.TransactionNumber,
            Status = freebies.FreebiesRequests?.FirstOrDefault()?.Status ?? "For freebie request",
            FreebieRequestId = freebies.FreebiesRequests?.FirstOrDefault()?.Id,
            Freebies = freebies.FreebiesRequests?.SelectMany(fr => fr.FreebieItems?.Select(fi =>
                new GetAllFreebies.GetAllFreebiesResult.Freebie
                {
                    Id = fi.Id,
                    ItemCode = fi.Items.ItemCode,
                    Quantity = fi.Quantity
                }) ?? new List<GetAllFreebies.GetAllFreebiesResult.Freebie>()).ToList()
        };
    }
}