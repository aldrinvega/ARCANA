using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction
{
    [Route("api/payment-transaction"), ApiController]
    public class VoidPaymentTransaction : ControllerBase
    {
        private readonly IMediator _mediator;

        public VoidPaymentTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}/void")]
        public async Task<IActionResult> Void([FromBody] VoidPaymentTransactionCommand command, [FromRoute] int id)
        {
            try
            {
                command.ClientId = id;
                var result = await _mediator.Send(command);

                return result.IsFailure ? BadRequest(result) : Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public class VoidPaymentTransactionCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public string Reason { get; set; }
        }

        public class Handler : IRequestHandler<VoidPaymentTransactionCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(VoidPaymentTransactionCommand request, CancellationToken cancellationToken)
            {
                var existingTransaction = await _context.Transactions
                    .FirstOrDefaultAsync(t => t.ClientId == request.ClientId);

                if (existingTransaction is null)
                {
                    return PaymentTransactionsErrors.NotFound();
                }

                existingTransaction.Status = Status.Voided;
                existingTransaction.Reason = request.Reason;

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }           
        }
    }
}
