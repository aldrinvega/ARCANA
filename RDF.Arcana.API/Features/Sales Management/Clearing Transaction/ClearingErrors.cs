using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    public class ClearingErrors
    {
        public static Error NotFound() =>
        new("Transaction.NotFound", "Client not found");
    }
}
