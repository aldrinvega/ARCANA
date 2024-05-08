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
                command.PaymentRecordId = id;
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
            public int PaymentRecordId { get; set; }
            public string Reason { get; set; }
        }
        public class TotalAmountDues
        {
            public int TransactionId { get; set; }
            public decimal Value { get; set; }
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
                var existingPaymentTransaction = await _context.PaymentRecords
                    .FirstOrDefaultAsync(t => t.Id == request.PaymentRecordId, cancellationToken);

                if (existingPaymentTransaction is null)
                {
                    return PaymentTransactionsErrors.NotFound();
                }

                var paymentTransactions = await _context.PaymentTransactions
                    .Include(tr => tr.Transaction)
                    .ThenInclude(ts => ts.TransactionSales)
                    .Where(pt => pt.PaymentRecordId == request.PaymentRecordId && pt.Status != Status.Voided)
                    .ToListAsync(cancellationToken);

                if (!paymentTransactions.Any())
                {
                    return PaymentTransactionsErrors.NotFound();
                }

                var advancePayments = await _context.AdvancePayments
                    .Where(ap => ap.ClientId == paymentTransactions.First().Transaction.ClientId && ap.AdvancePaymentAmount != ap.RemainingBalance)
                    .ToListAsync(cancellationToken);

                var totalAdvancePayment = paymentTransactions
                        .Where(x => x.PaymentMethod == PaymentMethods.AdvancePayment)
                        .Sum(x => x.TotalAmountReceived);

                var totalPayments = paymentTransactions
                        .Where(x => x.PaymentMethod != PaymentMethods.AdvancePayment)
                        .Sum(x => x.TotalAmountReceived);

                var totalAmountDues = existingPaymentTransaction.PaymentTransactions
                       .GroupBy(t => t.TransactionId)
                       .Select(g => g.First())
                       .Sum(x => x.Transaction.TransactionSales.TotalAmountDue);

                foreach(var paymentTransaction in paymentTransactions)
                {

                    //Sakin to
                    var transactions = await _context.Transactions
                        .Include(ts => ts.TransactionSales)
                        .FirstOrDefaultAsync(ts => ts.Id == paymentTransaction.TransactionId);

                    if(paymentTransaction.PaymentMethod == PaymentMethods.AdvancePayment)
                    {
                        foreach(var advancePayment in advancePayments)
                        {
                            totalAmountDues -= paymentTransaction.TotalAmountReceived;
                            var toAdd = transactions.TransactionSales.RemainingBalance - paymentTransaction.TotalAmountReceived;
                            transactions.TransactionSales.RemainingBalance += toAdd;
                            paymentTransaction.Status = Status.Voided;

                            var toDeduct = advancePayment.AdvancePaymentAmount - advancePayment.RemainingBalance;

                            if(toDeduct > 0)
                            {
                                if (totalAdvancePayment > 0)
                                {
                                    totalAdvancePayment -= toDeduct;
                                    advancePayment.RemainingBalance += toDeduct;

                                    await _context.SaveChangesAsync(cancellationToken);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    
                }

                //foreach (Domain.AdvancePayment advancePayment in advancePayments)
                //{
                //    if (advancePayment.AdvancePaymentAmount == advancePayment.RemainingBalance && totalAdvancePayment > 0)
                //    {
                //        return PaymentTransactionsErrors.AlreadyFulfilled();
                //    }

                //    foreach(var paymentTransaction in paymentTransactions.Where(pt => pt.PaymentMethod == PaymentMethods.AdvancePayment))
                //    {
                //       if(totalAmountDues > 0)
                //        {
                //          totalAmountDues -= paymentTransaction.TotalAmountReceived;
                //            var toAdd = paymentTransaction.Transaction.TransactionSales.RemainingBalance - paymentTransaction.TotalAmountReceived;
                //          paymentTransaction.Transaction.TransactionSales.RemainingBalance += toAdd;
                //            paymentTransaction.Status = Status.Voided;
                //        }
                //    }

                //    var toDeduct = advancePayment.AdvancePaymentAmount - advancePayment.RemainingBalance;

                //    if(toDeduct > 0)
                //    {  
                //        if(totalAdvancePayment > 0)
                //        {
                //            totalAdvancePayment -= toDeduct;
                //            advancePayment.RemainingBalance += toDeduct;

                //            await _context.SaveChangesAsync(cancellationToken);
                //        }
                //        else
                //        {
                //            continue;
                //        }
                //    }
                //}

                foreach (var paymentTransaction in paymentTransactions.Where(pt => pt.PaymentMethod != PaymentMethods.AdvancePayment))
                {

                    var transactions = await _context.Transactions
                        .Include(ts => ts.TransactionSales)
                        .FirstOrDefaultAsync(ts => ts.Id == paymentTransaction.TransactionId);


                    paymentTransaction.Status = Status.Voided;
                    if (totalAmountDues > 0)
                    {
                            totalAmountDues -= paymentTransaction.TotalAmountReceived;
                            transactions.TransactionSales.RemainingBalance += paymentTransaction.TotalAmountReceived;
                     }
                }

                existingPaymentTransaction.Status = Status.Voided;
                existingPaymentTransaction.Reason = request.Reason;

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }           
        }
    }
}
