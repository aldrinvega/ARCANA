using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction;

public class AddNewPaymentTransaction
{
    public class AddNewPaymentTransactionCommand : IRequest<Result>
    {
        public int TransactionId { get; set; }
        public int Payee { get; set; }

    }
}