using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

public static class FreebiesMappingExtension
{
    public static GetRequestedFreebies.GetRequestedFreebiesQueryResult
        ToGetRequestedFreebiesQueryResult(this FreebieRequest freebies)
    {
        return new GetRequestedFreebies.GetRequestedFreebiesQueryResult
        {
            Id = freebies.Id,
            OwnersName = freebies.ApprovedClient.Client.OwnersName,
            PhoneNumber = freebies.ApprovedClient.Client.PhoneNumber,
            OwnersAddress = freebies.ApprovedClient.Client.OwnersAddress,
            TransactionNumber = freebies.TransactionNumber,
            Freebies = freebies.Freebies.Select(x => new GetRequestedFreebies.GetRequestedFreebiesQueryResult.Freebie
            {
                Id = x.Id,
                ItemCode = x.Item.ItemCode,
                Quantity = x.Quantity,

            }).ToList(),
            DateCreated = freebies.CreatedAt.ToString("yyyy-MM-dd")
        };
    }
}