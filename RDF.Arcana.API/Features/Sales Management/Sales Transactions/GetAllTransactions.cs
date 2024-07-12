using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions
{
    [Route("api/sales-transaction"), ApiController]
    public class GetAllTransactions : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllTransactions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("page")]
        public async Task<IActionResult> Get([FromQuery] GetAllTransactionsQuery query)
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

        public class GetAllTransactionsQuery : UserParams, IRequest<PagedList<GetAllTransactionQueryResult>>
        {
            public string Search { get; set; }
            public bool? Status { get; set; }
            public string TransactionStatus { get; set; }
            public string InvoiceNo { get; set; }
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
            public string Terms  { get; set; }
            public int? ClientId { get; set; }
        }

        public class GetAllTransactionQueryResult
        {
            public int TransactionNo { get; set; }
            public int ClientId { get; set; }
            public string BusinessName { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public string InvoiceNo { get; set; }
            public string InvoiceType { get; set; }
            public string AddedBy { get; set; }
            public decimal RemainingBalance { get; set; }
            public decimal TotalAmountDue { get; set; }
            public string CIAttachment { get; set; }
        }

        public class Handler : IRequestHandler<GetAllTransactionsQuery, PagedList<GetAllTransactionQueryResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetAllTransactionQueryResult>> Handle(GetAllTransactionsQuery request,
                CancellationToken cancellationToken)
            {
                
                IQueryable<Transactions> transactions = _context.Transactions
                    .Include(x => x.TransactionItems)
                    .Include(ts => ts.TransactionSales)
                    .Include(cl => cl.Client)
                    .ThenInclude(to => to.Term)
                    .ThenInclude(td => td.TermDays)
                    .Include(cl => cl.Client)
                    .ThenInclude(to => to.Term)
                    .ThenInclude(t => t.Terms);

                if(request.ClientId != null)
                {
                    transactions = transactions.Where(tr => tr.ClientId == request.ClientId);
                }

                if (!string.IsNullOrEmpty(request.Terms))
                {
                    transactions = transactions.Where(tr => tr.Client.Term.Terms.TermType == request.Terms);
                }

                if (!string.IsNullOrEmpty(request.DateFrom) && !string.IsNullOrEmpty(request.DateFrom))
                {
                    var fromDate = DateTime.Parse(request.DateFrom);
                    var toDate = DateTime.Parse(request.DateTo);

                    transactions = transactions.Where(t =>
                    t.CreatedAt.Date >= fromDate.Date && t.CreatedAt.Date <= toDate.Date)
                                .OrderByDescending(d => d.CreatedAt);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    transactions = transactions.Where(t =>
                        t.Client.Fullname.Contains(request.Search) ||
                        t.Client.BusinessName.Contains(request.Search) ||
                        t.TransactionSales.ChargeInvoiceNo.Contains(request.Search));
                }

                if (request.Status != null)
                {
                    transactions = transactions.Where(t => t.IsActive == request.Status);
                }

                if (!string.IsNullOrEmpty(request.TransactionStatus))
                {
                    if(request.TransactionStatus == Status.Overdue && transactions.Any(cl => cl.Client.Term.Terms.TermType == Common.Terms.OneUpOneDown))
                    {
                        transactions = transactions.Where(result => result.CreatedAt.AddDays(result.Client.Term.TermDays.Days) < DateTime.Now);
                    }
                    else
                    {
                        transactions = transactions.Where(t => t.Status == request.TransactionStatus);
                    }
                }

                if (!string.IsNullOrEmpty(request.InvoiceNo))
                {
                    transactions = transactions.Where(inv => inv.InvoiceNo.ToLower().Contains(request.InvoiceNo.ToLower()));
                }

                // if (request.TransactionStatus == Status.Voided)
                // {
                //     var voidedTransactions = transactions.Where(vt => vt.Status == Status.Voided);
                //
                //     var voidedResult = voidedTransactions.Select(result => new GetAllTransactionQueryResult
                //     {
                //         TransactionNo = result.Id,
                //         ClientId = result.ClientId,
                //         BusinessName = result.Client.BusinessName,
                //         Status = result.Status,
                //         CreatedAt = result.CreatedAt,
                //         ChargeInvoiceNo = result.TransactionSales.ChargeInvoiceNo,
                //         AddedBy = result.AddedByUser.Fullname
                //     });
                //
                //     return PagedList<GetAllTransactionQueryResult>.CreateAsync(voidedResult, request.PageNumber,
                //         request.PageSize);
                // }

                if (request.TransactionStatus is null)
                {
                    transactions = transactions.Where(x => x.Status != Status.Voided);
                    var allTransaction = transactions.Select(result => new GetAllTransactionQueryResult
                    {
                        TransactionNo = result.Id,
                        ClientId = result.ClientId,
                        BusinessName = result.Client.BusinessName,
                        Status = (result.Client.Term.Terms.TermType == Common.Terms.OneUpOneDown && result.CreatedAt.AddDays(result.Client.Term.TermDays.Days) < DateTime.Now) ? Status.Overdue : result.Status,
                        CreatedAt = result.CreatedAt,
                        InvoiceNo = result.InvoiceNo,
                        InvoiceType = result.InvoiceType,
                        AddedBy = result.AddedByUser.Fullname,
                        RemainingBalance = result.TransactionSales.RemainingBalance,
                        TotalAmountDue = result.TransactionSales.TotalAmountDue,
                        CIAttachment = result.InvoiceAttach

                    });

                    return PagedList<GetAllTransactionQueryResult>.CreateAsync(allTransaction, request.PageNumber,
                        request.PageSize);
                }

                var result = transactions.Select(result => new GetAllTransactionQueryResult
                {
                    TransactionNo = result.Id,
                    ClientId = result.ClientId,
                    BusinessName = result.Client.BusinessName,
                    Status = (result.Client.Term.Terms.TermType == Common.Terms.OneUpOneDown && result.CreatedAt.AddDays(result.Client.Term.TermDays.Days) < DateTime.Now) ? Status.Overdue : result.Status,
                    CreatedAt = result.CreatedAt,
                    InvoiceNo = result.InvoiceNo,
                    InvoiceType = result.InvoiceType,
                    AddedBy = result.AddedByUser.Fullname,
                    RemainingBalance = result.TransactionSales.RemainingBalance,
                    TotalAmountDue = result.TransactionSales.TotalAmountDue,
                    CIAttachment = result.InvoiceAttach
                });

                return PagedList<GetAllTransactionQueryResult>.CreateAsync(result, request.PageNumber,
                    request.PageSize);
            }
        }
    }
}
