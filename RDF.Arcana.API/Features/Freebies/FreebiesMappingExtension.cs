using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

public static class FreebiesMappingExtension
{
    public static GetRequestedFreebies.GetRequestedFreebiesQueryResult
        ToGetRequestedFreebiesQueryResult(this Approvals freebies)
    {
        return new GetRequestedFreebies.GetRequestedFreebiesQueryResult
        {
            FreebieRequestId = freebies.FreebieRequest.Id,
            ClientId = freebies.ClientId,
            OwnersName = freebies.Client.Fullname,
            PhoneNumber = freebies.Client.PhoneNumber,
            OwnersAddress = freebies.Client.Address,
            TransactionNumber = freebies.FreebieRequest.TransactionNumber,
            Freebies = freebies.FreebieRequest.FreebieItems.
                Select(x => new GetRequestedFreebies.GetRequestedFreebiesQueryResult.Freebie
            {
                Id = x.Id,
                RequestId = x.RequestId,
                ItemCode = x.Items.ItemCode,
                Quantity = x.Quantity,

            }).ToList(),
            // DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
        };
    }
    
    public static GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult
        ToGetApprovedFreebiesQueryResult(this Approvals freebies)
    {
        return new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult
        {
            FreebieRequestId = freebies.FreebieRequest.Id,
            ClientId = freebies.ClientId,
            OwnersName = freebies.Client.Fullname,
            PhoneNumber = freebies.Client.PhoneNumber,
            OwnersAddress = freebies.Client.Address,
            TransactionNumber = freebies.FreebieRequest.TransactionNumber,
            Freebies = freebies.FreebieRequest.FreebieItems.
                Select(x => new GetAllApprovedFreebies.GetAllApprovedFreebiesQueryResult.Freebie
                {
                    Id = x.RequestId,
                    ItemCode = x.Items.ItemCode,
                    Quantity = x.Quantity,

                }).ToList(),    
            // DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
        };
    }
    
    public static GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult
        ToGetAllRejectedFreebiesQueryResult(this Approvals freebies)
    {
        return new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult
        {
            FreebieRequestId = freebies.FreebieRequest.Id,
            ClientId = freebies.ClientId,
            OwnersName = freebies.Client.Fullname,
            PhoneNumber = freebies.Client.PhoneNumber,
            OwnersAddress = freebies.Client.Address,
            TransactionNumber = freebies.FreebieRequest.TransactionNumber,
            Freebies = freebies.FreebieRequest.FreebieItems.
                Select(x => new GetAllRejectedFreebies.GetAllRejectedFreebiesQueryResult.Freebie
                {
                    Id = x.RequestId,
                    ItemCode = x.Items.ItemCode,
                    Quantity = x.Quantity,

                }).ToList(),
            // DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
        };
    }
}