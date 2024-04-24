using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using static RDF.Arcana.API.Features.Sales_Management.Sales_Transactions.GetAllTransactions;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    [Microsoft.AspNetCore.Mvc.Route("api/clearing-transaction"), ApiController]
    public class GetAllForClearingTransaction : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllForClearingTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("pages")]
        public async Task<IActionResult> Get([FromQuery] GetAllForClearingTransactionQuery query)
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

        public class GetAllForClearingTransactionQuery : UserParams, IRequest<PagedList<GetAllForClearingTransactionResult>>
        {
            public string Search { get; set; }
        }

        public class GetAllForClearingTransactionResult
        {
            public int TxNumber { get; set; }
            public string BusinessName { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public string PaymentType { get; set; }
        }

        public class Handler : IRequestHandler<GetAllForClearingTransactionQuery, PagedList<GetAllForClearingTransactionResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public Task<PagedList<GetAllForClearingTransactionResult>> Handle(GetAllForClearingTransactionQuery request,
                CancellationToken cancellationToken)
            {
                IQueryable<Transactions> transactions = _context.Transactions
                    .Include(x => x.TransactionSales)
                    .Include(c => c.Client)
                    .Where(p => p.Status == Status.Paid);             

                if (!string.IsNullOrEmpty(request.Search))
                {
                    transactions = transactions.Where(tr => tr.Client.BusinessName.Contains(request.Search));
                }

                var result = transactions.Select(result => new GetAllForClearingTransactionResult
                {
                    TxNumber = result.TransactionSales.TransactionId,
                    BusinessName = result.Client.BusinessName,
                    Date = result.CreatedAt,
                    Amount = result.TransactionSales.TotalAmountDue,
                    PaymentType = result.Status
                });

                return PagedList<GetAllForClearingTransactionResult>.CreateAsync(result, request.PageNumber,
                    request.PageSize);


            }
        }
    }
}
