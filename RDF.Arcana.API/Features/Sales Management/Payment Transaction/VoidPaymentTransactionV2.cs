
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction
{
    [Route("api/payment-transaction"), ApiController]
    public class VoidPaymentTransactionV2 : ControllerBase
    {
        private readonly IMediator _mediator;
        public VoidPaymentTransactionV2(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}/voidV2")]
        public async Task<IActionResult> Void([FromBody] VoidPaymentTransactionV2Command command, [FromRoute] int id)
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

        public class VoidPaymentTransactionV2Command : IRequest<Result>
        {
            public int PaymentRecordId { get; set; }
            public string Reason { get; set; }
            public string OnlinePlatform { get; set; }
            
        }

        public class Handler : IRequestHandler<VoidPaymentTransactionV2Command, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(VoidPaymentTransactionV2Command request, CancellationToken cancellationToken)
            {
                var existingPaymentRecord = await _context.PaymentRecords
                    .FirstOrDefaultAsync(pr => pr.Id == request.PaymentRecordId &&
                        pr.Status != Status.Voided, cancellationToken);

                if (existingPaymentRecord is null) 
                {
                    return PaymentTransactionsErrors.NotFound();
                }

                var paymentTransactions = await _context.PaymentTransactions
                    .Include(t => t.Transaction)
                    .Where(pt => pt.PaymentRecordId == request.PaymentRecordId)
                    .ToListAsync(cancellationToken);

                var advancePayments = await _context.AdvancePayments
                    .Where(ap => ap.ClientId == paymentTransactions.First().Transaction.ClientId)
                    .ToListAsync(cancellationToken);

                

                //var listingFees = await _context.ListingFees
                //    .Where(lf => lf.ClientId == paymentTransactions.First().Transaction.ClientId)
                //    .ToListAsync(cancellationToken);

                var transactionSales = await _context.TransactionSales
                    .Include(t => t.Transaction)
                    .Where(ts => ts.TransactionId == paymentTransactions.First().TransactionId)
                    .ToListAsync(cancellationToken);

                foreach(var payment in paymentTransactions)
                {   
                    //advancePayment
                    if(payment.PaymentMethod == PaymentMethods.AdvancePayment) 
                    {
                        decimal totalAPtoVoid = payment.PaymentAmount;
                        foreach(var advancePayment in advancePayments)
                        {
                            if(advancePayment.AdvancePaymentAmount <= totalAPtoVoid)
                            {
                                var totalAPToReturn = advancePayment.AdvancePaymentAmount - advancePayment.RemainingBalance;
                                advancePayment.RemainingBalance += totalAPToReturn;
                                totalAPtoVoid -= totalAPToReturn;

                                advancePayment.UpdatedAt = DateTime.Now;
                                advancePayment.Status = Status.Refunded;
                                advancePayment.Reason = request.Reason;

                            }
                            else
                            {
                                advancePayment.RemainingBalance += totalAPtoVoid;

                                advancePayment.UpdatedAt = DateTime.Now;
                                advancePayment.Status = Status.Refunded;
                                advancePayment.Reason = request.Reason;
                                break;
                            }
                        }
                        payment.Status = Status.Voided;
                        payment.Reason = request.Reason;
                    }

                    if(payment.PaymentMethod == PaymentMethods.Cheque)
                    {
                        payment.Status = Status.Voided;
                        payment.Reason = request.Reason;

                        var ChequeAP = await _context.AdvancePayments
                            .Where(ap => ap.PaymentTransactionId == payment.Id)
                            .ToListAsync(cancellationToken);

                        foreach (var item in ChequeAP)
                        {
                            item.Status = Status.Voided;
                            item.Reason = request.Reason;
                        }
                    }

                    //if (payment.PaymentMethod == PaymentMethods.Online)
                    //{
                    //    var onlinePayments = await _context.OnlinePayments
                    //        .Where(pr => pr.PaymentRecord.Id == request.PaymentRecordId)
                    //        .ToListAsync(cancellationToken);

                    //    payment.Status = Status.Voided;
                    //    payment.Reason = request.Reason;

                    //    foreach (var onlinePayment in onlinePayments)
                    //    {
                    //        decimal amountToReturn = 0;
                    //        if (request.OnlinePlatform == PaymentMethods.GCash)
                    //        {
                    //            onlinePayment.Status = Status.Voided;
                    //            onlinePayment.Remarks = request.Reason;
                    //            onlinePayment.UpdatedAt = DateTime.Now;

                    //            amountToReturn = onlinePayment.PaymentAmount;
                    //            payment.Transaction.TransactionSales.RemainingBalance += amountToReturn;                                

                    //        }

                    //        else if (request.OnlinePlatform == PaymentMethods.PayMaya)
                    //        {
                    //            onlinePayment.Status = Status.Voided;
                    //            onlinePayment.Remarks = request.Reason;
                    //            onlinePayment.UpdatedAt = DateTime.Now;

                    //            amountToReturn = onlinePayment.PaymentAmount;
                    //            payment.Transaction.TransactionSales.RemainingBalance += amountToReturn;
                    //        }

                    //    }

                    //}

                    //if(payment.PaymentMethod == PaymentMethods.ListingFee)
                    //{
                    //    decimal totalLFtoVoid = payment.PaymentAmount;
                    //    foreach(var listingFee in listingFees)
                    //    {
                    //        if(listingFee.Total <= totalLFtoVoid)
                    //        {

                    //        }
                    //    }
                    //}

                    //otherPayment
                    payment.Status = Status.Voided;
                    payment.Reason = request.Reason;                                        
                }

                foreach (var transactionSale in transactionSales)
                {
                    transactionSale.RemainingBalance = transactionSale.TotalAmountDue;
                }

                paymentTransactions.First().Transaction.Status = Status.Pending;
                existingPaymentRecord.Status = Status.Voided;
                existingPaymentRecord.Reason = request.Reason;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
