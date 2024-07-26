using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.GetAllPaymentTransactions.GetPaymentTransactionByStatusResult;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction
{
    [Microsoft.AspNetCore.Mvc.Route("api/payment-transaction"), ApiController]
    public class GetAllPaymentTransactions : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllPaymentTransactions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("page")]
        public async Task<IActionResult> Get([FromQuery] GetPaymentTransactionByStatusQuery query)
        {
            try
            {
                var transactions = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage);
                var result = new
                {
                    transactions,
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetPaymentTransactionByStatusQuery : UserParams, IRequest<PagedList<GetPaymentTransactionByStatusResult>>
        {
            public bool? Status { get; set; }
            public string Search { get; set; }
            public string PaymentStatus { get; set; }
            public string PaymentMethods { get; set; }
            public int? TransactionId { get; set; }
            public int? ClientId { get; set; }

        }

        public class GetPaymentTransactionByStatusResult
        {
            public int PaymentRecordId { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string Status { get; set; }
            public IEnumerable<PaymentTransaction> PaymentTransactions { get; set; }
            public string BusinessName { get; set; }
            public decimal TotalPayment { get; set; }
            public class PaymentTransaction
            {
                public int Id { get; set; }
                public int TransactionId { get; set; }
                public string Remarks { get; set; }
                public decimal TotalAmountDue { get; set; }
                public decimal RemainingBalance { get; set; }
                public int ClientId { get; set; }
                public string BusinessName { get; set; }
                public string FullName { get; set; }                                                   
                public string PaymentMethod { get; set; }
                public decimal PaymentAmount { get; set; }
                public decimal TotalAmountReceived { get; set; }
                public string Payee { get; set; }
                public DateTime ChequeDate { get; set; }
                public string BankName { get; set; }
                public string ChequeNo { get; set; }
                public DateTime DateReceived { get; set; }
                public decimal ChequeAmount { get; set; }
                public string AccountName { get; set; }
                public string AccountNo { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetPaymentTransactionByStatusQuery, PagedList<GetPaymentTransactionByStatusResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetPaymentTransactionByStatusResult>> Handle(GetPaymentTransactionByStatusQuery request,
                CancellationToken cancellationToken)
            {

                IQueryable<PaymentRecords> paymentTransactions = _context.PaymentRecords
                    .Include(c => c.PaymentTransactions)
                    .ThenInclude(pt => pt.Transaction)
                    .ThenInclude(ts => ts.TransactionSales)

                    .Include(c => c.PaymentTransactions)
                    .ThenInclude(pt => pt.Transaction)
                    .ThenInclude(ts => ts.Client);

                bool searchHandled = false;

                if (int.TryParse(request.Search, out int transactionId) && !string.IsNullOrEmpty(request.Search))
                {
                    paymentTransactions = paymentTransactions.Where(tr => tr.PaymentTransactions.Any(tr => tr.TransactionId == transactionId));
                    searchHandled = true;
                }

                //I cant get the decimal here
                const decimal tolerance = 0.01m; 

                if (!searchHandled && decimal.TryParse(request.Search, out decimal totalAmount) && !string.IsNullOrEmpty(request.Search))
                {
                    paymentTransactions = paymentTransactions.Where(tr => tr.PaymentTransactions.Any(tr => Math.Abs(tr.TotalAmountReceived - totalAmount) <= tolerance));
                    searchHandled = true;
                }

                if (!searchHandled && !string.IsNullOrEmpty(request.Search))
                {
                    paymentTransactions = paymentTransactions.Where(tx =>
                                   tx.PaymentTransactions.Any(tr => tr.Transaction.Client.BusinessName.Contains(request.Search)) ||
                                   tx.PaymentTransactions.Any(tr => tr.Transaction.Client.Fullname.Contains(request.Search)) ||
                                   tx.PaymentTransactions.Any(tr => tr.Transaction.TransactionSales.Remarks.Contains(request.Search)));
                }

                if (request.TransactionId.HasValue)
                {
                    paymentTransactions = paymentTransactions.Where(pt => pt.PaymentTransactions.Any(tr => tr.TransactionId == request.TransactionId.Value));
                }

                if (request.ClientId.HasValue)
                {
                    paymentTransactions = paymentTransactions.Where(pt => pt.PaymentTransactions.Any(tr => tr.Transaction.ClientId == request.ClientId.Value));
                }

                if (request.Status.HasValue)
                {
                    paymentTransactions = paymentTransactions.Where(t => t.IsActive == request.Status);
                }

                if (!string.IsNullOrEmpty(request.PaymentStatus))
                {
                    paymentTransactions = paymentTransactions.Where(tr => tr.PaymentTransactions.Any(tr => tr.Status == request.PaymentStatus));
                }

                if (!string.IsNullOrEmpty(request.PaymentMethods))
                {
                    paymentTransactions = paymentTransactions
                        .Where(pt => pt.PaymentTransactions.Any(pm => pm.PaymentMethod == request.PaymentMethods));
                }

                var result = paymentTransactions.Select(result => new GetPaymentTransactionByStatusResult
                {
                    PaymentRecordId = result.Id,
                    CreatedAt = result.CreatedAt,   
                    UpdatedAt = result.UpdatedAt,
                    Status = result.Status,
                    BusinessName = result.PaymentTransactions.Select(pt => pt.Transaction.Client.BusinessName).FirstOrDefault(),
                    TotalPayment = result.PaymentTransactions.Sum(pt => pt.PaymentAmount),
                    PaymentTransactions = result.PaymentTransactions.Select(pt => new GetPaymentTransactionByStatusResult.PaymentTransaction
                    {
                        Id = pt.Id,
                        TransactionId = pt.TransactionId,
                        Remarks = pt.Transaction.TransactionSales.Remarks,
                        TotalAmountDue = pt.Transaction.TransactionSales.TotalAmountDue,
                        RemainingBalance = pt.Transaction.TransactionSales.RemainingBalance,
                        ClientId = pt.Transaction.ClientId,
                        BusinessName = pt.Transaction.Client.BusinessName,
                        FullName = pt.Transaction.Client.Fullname,
                        PaymentMethod = pt.PaymentMethod,
                        PaymentAmount = pt.PaymentAmount,
                        TotalAmountReceived = pt.TotalAmountReceived,
                        Payee = pt.Payee,
                        ChequeDate = pt.ChequeDate,
                        BankName = pt.BankName,
                        ChequeNo = pt.ChequeNo,
                        DateReceived = pt.DateReceived,
                        ChequeAmount = pt.ChequeAmount,
                        AccountName = pt.AccountName,
                        AccountNo = pt.AccountNo
                    }).ToList()
                }).OrderByDescending(r => r.BusinessName);

                return PagedList<GetPaymentTransactionByStatusResult>.CreateAsync(result, request.PageNumber,
                    request.PageSize);
            }
        }
    }
}
