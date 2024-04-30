using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction;

public static class PaymentTransactionsErrors
{
    public static Error InsufficientFunds() => new("Payment.InsufficientFunds", "You don't have enough balance to continue this transaction.");
    public static Error NotFound() => new("PaymentTransaction.NotFound", "Payment transaction not found");
    public static Error AlreadyFulfilled() => new("PaymentTransaction.AlreadyFulfilled", "Advance payment remaining balance is already fulfilled");
}
