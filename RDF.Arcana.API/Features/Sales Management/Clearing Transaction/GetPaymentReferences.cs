using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction;

public class GetPaymentReferences
{
    public class GetPaymentReferencesQuery : IRequest<Result>
    {
        public string ChequeNo { get; set; }
        public string ReferenceNo { get; set; }
        public string TransactionNo { get; set; }
    }

    public class GetPaymentReferencesResult
    {
        public int PaymentTransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmountReceived { get; set; }
        public string Payee { get; set; }
        public int MyProperty { get; set; }
        public class Transaction
        {
            public string Client { get; set; }
            public string Status { get; set; }
        }

    }
}
