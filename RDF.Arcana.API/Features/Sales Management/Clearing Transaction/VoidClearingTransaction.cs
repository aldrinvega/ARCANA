using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.VoidPaymentTransaction;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    [Microsoft.AspNetCore.Mvc.Route("api/clearing-transaction"), ApiController]
    public class VoidClearingTransaction : ControllerBase
    {
        private readonly IMediator _mediator;
        public VoidClearingTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("void")]
        public async Task<IActionResult> Void([FromBody] VoidClearingTransactionCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return result.IsFailure ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public class VoidClearingTransactionCommand : IRequest<Result>
        {
            public List<int> PaymentTransactionIds { get; set; }
            public string Reason { get; set; }
        }

        public class Handler : IRequestHandler<VoidClearingTransactionCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
				
			}

			public async Task<Result> Handle(VoidClearingTransactionCommand request, CancellationToken cancellationToken)
			{
				var paymentTransactions = await _context.PaymentTransactions
					.Include(pr => pr.PaymentRecord)
					.Include(pt => pt.Transaction)
					.ThenInclude(tr => tr.TransactionSales)
					.Where(pt => request.PaymentTransactionIds.Contains(pt.Id))
					.ToListAsync();

				foreach (var paymentTransaction in paymentTransactions)
				{
					if (paymentTransaction != null)
					{
						paymentTransaction.Status = Status.Voided;
						paymentTransaction.PaymentRecord.Status = Status.Voided;
						foreach (var transaction in paymentTransactions)
						{
							
							transaction.Status = Status.Voided;
							transaction.Transaction.Status = Status.Voided;
							transaction.Transaction.TransactionSales.RemainingBalance += transaction.PaymentAmount;
							await _context.SaveChangesAsync(cancellationToken);
						}
						await _context.SaveChangesAsync(cancellationToken);
					}
				}

				if (paymentTransactions.Any())
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
