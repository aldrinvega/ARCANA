using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;

public static class TransactionErrors
{
    public static Error NotFound() => new("Transaction.NotFound", "No transaction found");
    public static Error AlreadyPaid() => new("Transaction.AlreadyPaid", "This transaction is already paid");
}