using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;

public class AdvancePaymentErrors
{
    public static Error NotFound() => new("AdvancePayment.NotFound", "No advance payment found");
}