using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_transactions;

public static class SalesTransactionErrors
{
    public static Error NotFound() => new("SalesTransaction.NotFound", "Sales transaction not found.");
}