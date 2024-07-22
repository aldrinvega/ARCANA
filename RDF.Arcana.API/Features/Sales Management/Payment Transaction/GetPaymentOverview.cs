using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;


namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction
{
	[Route("api/payment"), ApiController]
	public class GetPaymentOverview : ControllerBase
	{
		private readonly IMediator _mediator;

		public GetPaymentOverview(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("overview")]
		public async Task<IActionResult> GetPaymentOverviewAsync([FromQuery] GetPaymentOverviewRequest query)
		{

			var result = await _mediator.Send(query);

			return result.IsSuccess ? Ok(result) : BadRequest(result);
		}

		public class GetPaymentOverviewRequest : IRequest<Result>
		{
			public string ReferenceNo { get; set; }
            public string PaymentMethod { get; set; }
        }

		public class GetPaymentOverviewResponse
		{
            public string PaymentMethod { get; set; }
            public string PaymentChannel { get; set; }
            public string CreatedAt { get; set; }
            public string AddedBy { get; set; }
            public decimal TotalAmount { get; set; }
			public string ModeOfPayment { get; set; }
			public ICollection<Transaction> Transactions { get; set; }

            public class Transaction
			{
                public int PaymentTransactionId { get; set; }
                public string InvoiceNo { get; set; }
                public decimal PaymentAmount { get; set; }
                public ICollection<TransactionItem> TransactionItems { get; set; }

            }

			public class TransactionItem
			{
                public string ItemCode { get; set; }
                public string ItemDescription { get; set; }
                public int Quantity { get; set; }
                public decimal Amount { get; set; }
            }
        }

		public class Handler : IRequestHandler<GetPaymentOverviewRequest, Result>
		{
			private readonly ArcanaDbContext _context;

			public Handler(ArcanaDbContext context)
			{
				_context = context;
			}

			public async Task<Result> Handle(GetPaymentOverviewRequest request, CancellationToken cancellationToken)
			{
				// Fetch PaymentRecords along with the necessary navigation properties eagerly loaded
				var paymentTransactions = await _context.PaymentRecords
					.Where(pr => pr.PaymentTransactions.Any(pt => pt.ReferenceNo == request.ReferenceNo))
					.Include(pr => pr.PaymentTransactions)
						.ThenInclude(pt => pt.Transaction)
							.ThenInclude(t => t.TransactionItems)
								.ThenInclude(ti => ti.Item)
					.Include(pr => pr.PaymentTransactions)
						.ThenInclude(pt => pt.AddedByUser)
					.SelectMany(pr => pr.PaymentTransactions)
					.Where(pt => (pt.ReferenceNo == request.ReferenceNo && pt.PaymentMethod == request.PaymentMethod))
					.ToListAsync(cancellationToken);

				// Perform the grouping and projection in memory
				var paymentOverview = paymentTransactions
					.GroupBy(pt => new { pt.PaymentMethod, pt.ReferenceNo, pt.BankName, pt.ChequeDate, pt.AddedByUser })
					.Select(g => new GetPaymentOverviewResponse
					{
						PaymentMethod = g.Key.PaymentMethod,
						PaymentChannel = g.Key.BankName,
						CreatedAt = g.Key.ChequeDate.ToString("MM-dd-yyyy"),
						AddedBy = g.Key.AddedByUser.Fullname,
						TotalAmount = g.Sum(pt => pt.PaymentAmount),
						ModeOfPayment = g.Key.PaymentMethod,
						Transactions = g.Select(pt => new GetPaymentOverviewResponse.Transaction
						{
							PaymentTransactionId = pt.Id,
							InvoiceNo = pt.Transaction.InvoiceNo,
							PaymentAmount = pt.TotalAmountReceived,
							TransactionItems = pt.Transaction.TransactionItems.Select(ti => new GetPaymentOverviewResponse.TransactionItem
							{
								ItemCode = ti.Item.ItemCode,
								ItemDescription = ti.Item.ItemDescription,
								Quantity = ti.Quantity,
								Amount = ti.Amount
							}).ToList()
						}).ToList()
					}).ToList();

				return Result.Success(paymentOverview);
			}
		}
	}
}
