using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;


namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
	[Route("api/clearing-transaction")]
	public class ReturnForFilingPaymentTransaction : ControllerBase
	{
		private readonly IMediator _mediator;

		public ReturnForFilingPaymentTransaction(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPatch("filing/return")]
		public async Task<IActionResult> ReturnForFiling([FromBody] ReturnForFilingPaymentTransactionCommand command)
		{
			var result = await _mediator.Send(command);
			return result.IsFailure ? BadRequest(result) : Ok(result);
		}

		public class ReturnForFilingPaymentTransactionCommand : IRequest<Result>
		{
            public List<int> PaymentTransactionIds { get; set; }
            public string Reason { get; set; }
        }

		public class Handler : IRequestHandler<ReturnForFilingPaymentTransactionCommand, Result>
		{
			private readonly ArcanaDbContext _context;

			public Handler(ArcanaDbContext context)
			{
				_context = context;
			}

			public async Task<Result> Handle(ReturnForFilingPaymentTransactionCommand request, CancellationToken cancellationToken)
			{
				foreach (var paymentTransactionId in request.PaymentTransactionIds)
				{
					var paymentTransaction = await _context.ClearedPayments
						.Include(x => x.PaymentTransaction)
						.ThenInclude(x => x.Transaction)
						.FirstOrDefaultAsync(pt => pt.PaymentTransactionId == paymentTransactionId, cancellationToken: cancellationToken);

					if (paymentTransaction is not null)
					{
						paymentTransaction.Status = Status.ForClearing;
						paymentTransaction.Reason = request.Reason;
						paymentTransaction.PaymentTransaction.Status = Status.ForClearing;
						paymentTransaction.PaymentTransaction.Transaction.Status = Status.ForClearing;
						await _context.SaveChangesAsync(cancellationToken);
					}
				}

				// Check if any payment transactions were found
				if (request.PaymentTransactionIds.Any())
				{
					return Result.Success();
				}
				else
				{
					return ClearingErrors.NotFound();
				}
			}
		}
	}
}
