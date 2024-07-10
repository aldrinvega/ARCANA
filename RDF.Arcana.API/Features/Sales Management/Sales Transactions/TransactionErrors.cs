using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;

public static class TransactionErrors
{
    public static Error NotFound() => new("Transaction.NotFound", "No transaction found");
    public static Error AlreadyPaid() => new("Transaction.AlreadyPaid", "This transaction is already paid");
    public static Error CreditLimitExceeded() => new("Transaction.CreditLimitExceeded", "Not enough credit limit");
    public static Error AlreadyHasPayment() => new("Transaction.AlreadyHasPayment", "This has payment transaction");
    public static Error InvoiceAlreadyExist(string Invoice) => new("Transaction.InvoiceAlreadyExist", $"invoice {Invoice} is already exist");
    public static Error Voided() => new("Transaction.Voided", "This transaction is already voided");
    public static Error SICI() => new("SI/CI", "Invalid Invoice Type [Sales/Charge]");
    public static Error InvalidInvoiceNumber() => new("SI/CI", "Invoice Number must not be value string or null");
}