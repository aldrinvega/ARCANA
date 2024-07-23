using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Sales_Management.Payment_Transaction.VoidPaymentTransaction;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    [Microsoft.AspNetCore.Mvc.Route("api/void-payment-transaction"), ApiController]
    public class VoidClearingTransaction : ControllerBase
    {
        private readonly IMediator _mediator;
        public VoidClearingTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}/void")]
        public async Task<IActionResult> Void([FromBody] VoidClearingTransactionCommand command, [FromRoute] int id)
        {
            try
            {
                command.Id = id;
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
				foreach (var paymentTransactionId in request.PaymentTransactionIds)
				{
					var paymentTransaction = await _context.ClearedPayments
						.Include(pt => pt.PaymentTransaction)
						.ThenInclude(x => x.PaymentRecord)
						.Include(x => x.PaymentTransaction)
						.ThenInclude(x => x.Transaction)
						.FirstOrDefaultAsync(pt => pt.PaymentTransactionId == paymentTransactionId, cancellationToken: cancellationToken);

					if (paymentTransaction is not null)
					{
						paymentTransaction.Status = Status.ForClearing;
						paymentTransaction.PaymentTransaction.Status = Status.ForClearing;
						paymentTransaction.PaymentTransaction.Transaction.Status = Status.ForClearing;
						paymentTransaction.PaymentTransaction.PaymentRecord.Status = Status.ForClearing;
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
