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
            public int Id { get; set; }
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
                var paymentRecord = await _context.PaymentRecords
                    .Include(pt  => pt.PaymentTransactions)
                    .ThenInclude(t => t.Transaction)
                    .FirstOrDefaultAsync(tr =>
                        tr.Id == request.Id,
                        cancellationToken);

                //paymentRecord.PaymentTransactions.



                if (paymentRecord is null)
                {
                    return ClearingErrors.NotFound();
                }
               
                var existingPayment = await _context.PaymentRecords
                    .FirstOrDefaultAsync(p => p.Id == request.Id);


                if(existingPayment.Status == Status.Cleared)
                {
                    return ClearingErrors.Cleared();
                }

                if (existingPayment.Status == Status.Voided)
                {
                    return ClearingErrors.Voided();
                }

                existingPayment.Status = Status.Voided;
                existingPayment.IsActive = false;
                existingPayment.Reason = request.Reason;

                

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
