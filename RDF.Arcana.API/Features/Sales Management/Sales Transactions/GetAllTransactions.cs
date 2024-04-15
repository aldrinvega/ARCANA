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
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
        }

        public class GetAllTransactionQueryResult
        {
            public int TransactionNo { get; set; }
            public int ClientId { get; set; }
            public string BusinessName { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public string ChargeInvoiceNo { get; set; }
            public string AddedBy { get; set; }
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
                    .Include(ts => ts.TransactionSales);

                if (!string.IsNullOrEmpty(request.DateFrom) && !string.IsNullOrEmpty(request.DateFrom))
                {
                    var fromDate = DateTime.Parse(request.DateFrom);
                    var toDate = DateTime.Parse(request.DateTo);

                    transactions = transactions.Where(t =>
                    t.CreatedAt >= fromDate && t.CreatedAt <= toDate);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    transactions = transactions.Where(t =>
                        t.Client.Fullname.Contains(request.Search) &&
                        t.Client.BusinessName.Contains(request.Search) &&
                        t.TransactionSales.ChargeInvoiceNo.Contains(request.Search)
                    );
                }

                if (request.Status != null)
                {
                    transactions = transactions.Where(t => t.IsActive == request.Status);
                }

                if (!string.IsNullOrEmpty(request.TransactionStatus))
                {
                    transactions = transactions.Where(t => t.Status == request.TransactionStatus);
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
                        Status = result.Status,
                        CreatedAt = result.CreatedAt,
                        ChargeInvoiceNo = result.TransactionSales.ChargeInvoiceNo,
                        AddedBy = result.AddedByUser.Fullname,

                    });

                    return PagedList<GetAllTransactionQueryResult>.CreateAsync(allTransaction, request.PageNumber,
                        request.PageSize);
                }

                var result = transactions.Select(result => new GetAllTransactionQueryResult
                {
                    TransactionNo = result.Id,
                    ClientId = result.ClientId,
                    BusinessName = result.Client.BusinessName,
                    Status = result.Status,
                    CreatedAt = result.CreatedAt,
                    ChargeInvoiceNo = result.TransactionSales.ChargeInvoiceNo,
                    AddedBy = result.AddedByUser.Fullname,

                });

                return PagedList<GetAllTransactionQueryResult>.CreateAsync(result, request.PageNumber,
                    request.PageSize);
            }
        }
    }
}
