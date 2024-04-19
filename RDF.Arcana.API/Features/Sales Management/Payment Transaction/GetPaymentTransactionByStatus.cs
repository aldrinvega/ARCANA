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
    public class GetPaymentTransactionByStatus : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetPaymentTransactionByStatus(IMediator mediator)
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
        }

        public class GetPaymentTransactionByStatusResult
        {
            public int ClientId { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string Reason { get; set; }
            public string Status { get; set; }
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
                IQueryable<Transactions> transactions = _context.Transactions;

                if(!string.IsNullOrEmpty(request.Status))
                {
                    transactions = transactions.Where(tr => tr.Status == request.Status);
                }

                var result = transactions.Select(result => new GetPaymentTransactionByStatusResult
                {
                    ClientId = result.ClientId,
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
