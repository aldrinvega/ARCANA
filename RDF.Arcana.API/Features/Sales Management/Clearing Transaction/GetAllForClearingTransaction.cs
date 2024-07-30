using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

[Route("api/clearing-transaction")]
[ApiController]
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

            Response.AddPaginationHeader(transactions.CurrentPage, transactions.PageSize, transactions.TotalCount,
                transactions.TotalPages, transactions.HasNextPage, transactions.HasPreviousPage);

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
        public string Status { get; set; }
    }

    public class GetAllForClearingTransactionResult
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentChannel { get; set; }
        public string ReferenceNo { get; set; }
        public decimal TotalPaymentAmount { get; set; }
        public int ClearingId { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public string ATag { get; set; }
        public class Invoice
		{
			public string InvoiceNo { get; set; }
		}

	}


	public class Handler : IRequestHandler<GetAllForClearingTransactionQuery, PagedList<GetAllForClearingTransactionResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }
		public async Task<PagedList<GetAllForClearingTransactionResult>> Handle(GetAllForClearingTransactionQuery request,
			CancellationToken cancellationToken)
		{
			var paymentTransactions = _context.PaymentTransactions
				.Include(pt => pt.Transaction)
				.Include(pt => pt.ClearedPayment)
				.Where(pt => pt.Status == request.Status);

			if (!string.IsNullOrEmpty(request.Search))
			{
				paymentTransactions = paymentTransactions
					.Where(pt => pt.ReferenceNo.Contains(request.Search));
			}

			var groupedResults = paymentTransactions
				.GroupBy(pt => new { 
                    pt.PaymentMethod, 
                    pt.ReferenceNo, 
                    pt.BankName, 
                    pt.ClearedPayment.ATag, 
                    pt.Reason })
				.Select(g => new GetAllForClearingTransactionResult
				{
					PaymentMethod = g.Key.PaymentMethod,
					PaymentChannel = g.Key.BankName,
					ReferenceNo = g.Key.ReferenceNo,
					TotalPaymentAmount = g.Sum(pt => pt.PaymentAmount),
					Date = g.Max(pt => pt.ChequeDate),
					ATag = g.Key.ATag,
					Reason = g.Key.Reason,
					Invoices = g.Select(x => new GetAllForClearingTransactionResult.Invoice
					{
						InvoiceNo = x.Transaction.InvoiceNo
					}).Distinct().ToList()
				});

			return await PagedList<GetAllForClearingTransactionResult>.CreateAsync(groupedResults, request.PageNumber,
				request.PageSize);
		}

	}
}