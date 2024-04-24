using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

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
            public string Status { get; set; }
            public string Search { get; set; }
            public string PaymentStatus { get; set; }
        }

        public class GetPaymentTransactionByStatusResult
        {
            public int TransactionId { get; set; }
            public int ClientId { get; set; }
            public string BusinessName { get; set; }
            public string FullName { get; set; }
            public string ChargeInvoiceNo { get; set; }
            public decimal TotalAmountDue { get; set; }
            public decimal RemainingBalance { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string Reason { get; set; }
            public string Status { get; set; }
            public ICollection<PaymentTransaction> PaymentTransactions { get; set; }


            public class PaymentTransaction
            {

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

                IQueryable<Transactions> transactions = _context.Transactions
                    .Include(c => c.Client)
                    .Include(ts => ts.TransactionSales);


                if (int.TryParse(request.Search, out int transactionId) && !string.IsNullOrEmpty(request.Search))
                {
                    transactions = transactions.Where(tr => tr.Id == transactionId);
                }
                else
                {
                    transactions = transactions.Where(tx => 
                                   tx.Client.BusinessName.Contains(request.Search) ||
                                   tx.Client.Fullname.Contains(request.Search) ||
                                   tx.TransactionSales.ChargeInvoiceNo.Contains(request.Search));
                }

                var result = transactions.Select(result => new GetPaymentTransactionByStatusResult
                {
                    TransactionId = result.Id,
                    ClientId = result.ClientId,
                    BusinessName = result.Client.BusinessName,
                    FullName = result.Client.Fullname,
                    ChargeInvoiceNo = result.TransactionSales.ChargeInvoiceNo,
                    TotalAmountDue = result.TransactionSales.TotalAmountDue,
                    RemainingBalance = result.TransactionSales.RemainingBalance,
                    CreatedAt = result.CreatedAt,   
                    UpdatedAt = result.UpdatedAt,
                    Reason = result.Reason,
                    Status = result.Status
                });

                return PagedList<GetPaymentTransactionByStatusResult>.CreateAsync(result, request.PageNumber,
                    request.PageSize);
            }
        }
    }
}
