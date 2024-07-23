using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction;
[Route("api/clearing-transaction"), ApiController]
public class FileClearingTransaction : ControllerBase
{
	private readonly IMediator _mediator;

	public FileClearingTransaction(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPatch("filing")]
	public async Task<IActionResult> File([FromBody] FiledClearingTransctionCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsFailure ? BadRequest(result) : Ok(result);

	}

	public class FiledClearingTransctionCommand : IRequest<Result>
	{
            public List<int> PaymentTransactionIds { get; set; }
        }

	public class Handler : IRequestHandler<FiledClearingTransctionCommand, Result>
	{
		private readonly ArcanaDbContext _context;

		public Handler(ArcanaDbContext context)
		{
			_context = context;
		}

		public async Task<Result> Handle(FiledClearingTransctionCommand request, CancellationToken cancellationToken)
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
                        paymentTransaction.Status = Status.Cleared;
                        paymentTransaction.PaymentTransaction.Status = Status.Cleared;
                        paymentTransaction.PaymentTransaction.Transaction.Status = Status.Cleared;
                        paymentTransaction.PaymentTransaction.PaymentRecord.Status = Status.Cleared;
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
