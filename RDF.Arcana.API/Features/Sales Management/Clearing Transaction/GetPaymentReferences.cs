
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction;

[Route("api/Clearing"), ApiController]
public class GetPaymentReferences : ControllerBase
{
    private readonly IMediator _mediator;
    public GetPaymentReferences(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("test")]
    public async Task<IActionResult> Get([FromQuery] GetPaymentReferencesQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    public class GetPaymentReferencesQuery : IRequest<Result>
    {
        public string Search { get; set; }
    }

    public class GetPaymentReferencesResult
    {
        public int PaymentTransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmountReceived { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal RemainingBalance { get; set; }
        public string Payee { get; set; }
        public DateTime DateReceived { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

        //cheque
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string ChequeNo { get; set; }
        public DateTime ChequeDate { get; set; }
        public string BankName { get; set; }
        public decimal ChequeAmount { get; set; }

        //online
        public string OnlinePlatform { get; set; }
        public string ReferenceNo { get; set; }

        //advancePayment
        public decimal? ExcessAdvancePayment { get; set; }

        public IEnumerable<TransactionResult> Transactions { get; set; }
        public class TransactionResult
        {
            public int Id { get; set; }
            public string Client { get; set; }
            public string Status { get; set; }
            public string InvoiceType { get; set; }
            public string InvoiceNo { get; set; }
            public string AddedBy { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetPaymentReferencesQuery, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetPaymentReferencesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<PaymentTransaction> paymentTransactions = _context.PaymentTransactions
                .Include(t => t.Transaction)
                .ThenInclude(ts => ts.TransactionSales)
                .Include(t => t.Transaction)
                .ThenInclude(c => c.Client)
                .Include(t => t.Transaction)
                .ThenInclude(c => c.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                IQueryable<PaymentTransaction> tempQuery = paymentTransactions;

                if (int.TryParse(request.Search, out int transactionNo))
                {
                    tempQuery = paymentTransactions.Where(pt => pt.TransactionId == transactionNo
                                            && (pt.PaymentMethod == PaymentMethods.AdvancePayment ||
                                            pt.PaymentMethod == PaymentMethods.Withholding ||
                                            pt.PaymentMethod == PaymentMethods.ListingFee ||
                                            pt.PaymentMethod == PaymentMethods.Cash));
                }

                bool hasAnyMatches = await tempQuery.AnyAsync(cancellationToken);

                if (hasAnyMatches)
                {
                    paymentTransactions = tempQuery;
                }
                else
                {
                    paymentTransactions = paymentTransactions.Where(pt => (pt.ChequeNo.Contains(request.Search) && pt.PaymentMethod == PaymentMethods.Cheque) ||
                                                                          (pt.ReferenceNo.Contains(request.Search) && pt.PaymentMethod == PaymentMethods.Online));
                }
            }

            bool finalMatch = await paymentTransactions.AnyAsync(cancellationToken);

            if (!finalMatch)
            {
                return ClearingErrors.NotFoundReferece();
            }

            var result = paymentTransactions.Select(pt => new GetPaymentReferencesResult
            {
                PaymentTransactionId = pt.TransactionId,
                PaymentMethod = pt.PaymentMethod,
                TotalAmountReceived = pt.TotalAmountReceived,
                TotalAmountDue = pt.Transaction.TransactionSales.TotalAmountDue,
                RemainingBalance = pt.Transaction.TransactionSales.RemainingBalance,
                Payee = pt.Payee,
                DateReceived = pt.DateReceived,
                Status = pt.Status,
                Reason = pt.Reason,
                AccountName = pt.AccountName,
                AccountNo = pt.AccountNo,
                ChequeNo = pt.ChequeNo,
                ChequeDate = pt.ChequeDate,
                BankName = pt.BankName,
                ChequeAmount = pt.ChequeAmount,
                OnlinePlatform = null, 
                ReferenceNo = pt.ReferenceNo,
                ExcessAdvancePayment = pt.PaymentMethod == PaymentMethods.Cheque
                                    ? _context.AdvancePayments
                                                .Where(ap => ap.PaymentTransactionId == pt.Id)
                                                .Select(ap => (decimal?)ap.AdvancePaymentAmount)
                                                .FirstOrDefault() ?? 0
                                    : (decimal?)null,
                Transactions = new List<GetPaymentReferencesResult.TransactionResult> 
                {
                    new GetPaymentReferencesResult.TransactionResult
                    {
                        Id = pt.Transaction.Id,
                        Client = pt.Transaction.Client.BusinessName,
                        Status = pt.Transaction.Status,
                        InvoiceType = pt.Transaction.InvoiceType,
                        InvoiceNo = pt.Transaction.InvoiceNo,
                        AddedBy = pt.Transaction.AddedByUser.Fullname
                    }
                }
            }).ToListAsync(cancellationToken);

            return Result.Success(result);
        }
    }
}
