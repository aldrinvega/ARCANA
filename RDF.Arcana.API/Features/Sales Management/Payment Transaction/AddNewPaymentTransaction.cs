using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction;

[Route("api/payment"), ApiController]
public class AddNewPaymentTransaction : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> AddPayment([FromBody] AddNewPaymentTransactionCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            var result = await Mediator.Send(command);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    public class AddNewPaymentTransactionCommand : IRequest<Result>
    {
        public List<int> TransactionId { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public int AddedBy { get; set; }

        public class Payment
        {
            public string PaymentMethod { get; set; }
            public decimal PaymentAmount { get; set; }
            public decimal TotalAmountReceived { get; set; }
            public string Payee { get; set; }
            public DateTime ChequeDate { get; set; }
            public string BankName { get; set; }
            public string ChequeNo { get; set; }
            public DateTime DateReceived { get; set; }
            public decimal ChequeAmount { get; set; }
            public string AccountName { get; set; }
            public string AccountNo { get; set; }
            public int AddedBy { get; set; }
            public string OnlinePlatform { get; set; }
            public string ReferenceNo { get; set; }
        }
    
    }
    
    public class Handler : IRequestHandler<AddNewPaymentTransactionCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            decimal totalAmount = 0;

            //Get the sum of the total amount due of the transactions selected
            foreach (var transactionId in request.TransactionId)
            {
                var transactions = await _context.Transactions
                    .Include(sales => sales.TransactionSales)
                    .FirstOrDefaultAsync(tr => tr.Id == transactionId);

                if(transactions is not null)
                {
                    totalAmount += transactions.TransactionSales.TotalAmountDue;
                }
                else if (transactions.Status == Status.Paid)
                {
                    return TransactionErrors.AlreadyPaid();
                }
                else
                {
                    return TransactionErrors.NotFound();
                }
            }

            //Create New Payment Records

            var paymentRecord = new PaymentRecords
            {
                AddedBy = request.AddedBy,
                ModifiedBy = request.AddedBy
            };

            await _context.PaymentRecords.AddAsync(paymentRecord, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Order transactions by total amount due (descending)
            var orderedTransactions = request.TransactionId
                .OrderByDescending(tid => _context.Transactions
                    .FirstOrDefault(t => t.Id == tid)?.TransactionSales.RemainingBalance ?? 0)
                .ToList();

            foreach (int transactionId in orderedTransactions)
            {
                var transaction = await _context.Transactions
                    .Include(t => t.TransactionSales)
                    .FirstOrDefaultAsync(t => t.Id == transactionId, cancellationToken);

                if (transaction is null)
                {
                    return TransactionErrors.NotFound();
                }

                decimal amountToPay = transaction.TransactionSales.RemainingBalance;

                // Order payments by payment amount (descending)
                var orderedPayments = request.Payments
                    .OrderByDescending(p => p.PaymentAmount)
                    .ToList();

                foreach (var payment in orderedPayments) 
                {
                    // If nothing more to pay for this transaction, move to the next
                    if (amountToPay <= 0 || payment.PaymentAmount <= 0)  
                    {
                        break;
                    }

                    decimal excessAmount = payment.PaymentAmount - amountToPay;
                    decimal paymentAmount = payment.PaymentAmount;

                    if (excessAmount > 0)
                    {
                        // Use excess amount to pay the next transaction
                        //payment.PaymentAmount -= excessAmount;
                        //amountToPay = 0;
                    }
                    //else
                    //{
                    //    // Not enough to cover the full amount, use entire payment
                    //    amountToPay -= payment.PaymentAmount;
                    //}

                    //For advance payment transactions
                    if (payment.PaymentMethod == PaymentMethods.AdvancePayment)
                    {
                        var advancePayments = await _context.AdvancePayments
                            .Where(x =>
                                x.ClientId == transaction.ClientId &&
                                x.IsActive &&
                                x.Status != Status.Voided)
                            .ToListAsync(cancellationToken);

                        var amountToPayAdvancePayment = request.Payments.Where(pm => pm.PaymentMethod == PaymentMethods.AdvancePayment)
                            .Sum(pa => pa.PaymentAmount);

                        

                        var remainingBalance = advancePayments.Sum(ap => ap.RemainingBalance);

                        var paymentTransaction = new PaymentTransaction
                        {
                            TransactionId = transaction.Id,
                            PaymentRecordId = paymentRecord.Id,
                            PaymentMethod = payment.PaymentMethod,
                            PaymentAmount = payment.PaymentAmount,
                            TotalAmountReceived = payment.TotalAmountReceived,
                            Payee = payment.Payee,
                            ChequeDate = payment.ChequeDate,
                            BankName = payment.BankName,
                            ChequeNo = payment.ChequeNo,
                            DateReceived = DateTime.Now,
                            ChequeAmount = payment.ChequeAmount,
                            AccountName = payment.AccountName,
                            AccountNo = payment.AccountNo,
                            AddedBy = request.AddedBy,
                            Status = Status.Received,
                            OnlinePlatform = payment.OnlinePlatform,
                            Reason = payment.ReferenceNo
                        };

                        await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        foreach (var advancePayment in advancePayments)
                        {

                            

                            decimal remainingToPay;
                            if (advancePayment.RemainingBalance <= amountToPayAdvancePayment)
                            {
                                var balance = amountToPay - advancePayment.RemainingBalance;
                                remainingToPay = amountToPayAdvancePayment - advancePayment.RemainingBalance;
                                transaction.TransactionSales.RemainingBalance = balance < 0 ? 0 : balance;
                                amountToPay -= advancePayment.RemainingBalance;
                                amountToPayAdvancePayment = remainingToPay;
                                payment.PaymentAmount = amountToPayAdvancePayment;
                                
                                advancePayment.RemainingBalance = 0;
                                await _context.SaveChangesAsync(cancellationToken);
                            }
                            else
                            {
                                remainingToPay = amountToPayAdvancePayment;
                                advancePayment.RemainingBalance -= amountToPayAdvancePayment;
                                transaction.TransactionSales.RemainingBalance -= payment.PaymentAmount < 0 ? 0 : payment.PaymentAmount;
                                amountToPay -= remainingToPay;
                                transaction.Status = Status.Paid;
                                payment.PaymentAmount = excessAmount;
                                remainingToPay = 0;
                                await _context.SaveChangesAsync(cancellationToken);
                                break;
                            }

                            // Break the loop if the payment amount can cover the total amount due
                            if (remainingToPay == 0)
                            {
                                continue;
                            }
                        }

                        

                    }


                    if (payment.PaymentMethod == PaymentMethods.Cheque)
                    {
                        var transactionSales = await _context.TransactionSales
                            .FirstOrDefaultAsync(ts => ts.TransactionId == transaction.Id);

                        decimal totalAmountDue = transactionSales.TotalAmountDue;

                        if(payment.PaymentAmount >  totalAmountDue) 
                        {
                            decimal excessChequeAmount = payment.PaymentAmount - totalAmountDue;

                            var paymentTransaction = new PaymentTransaction
                            {
                                TransactionId = transaction.Id,
                                PaymentRecordId = paymentRecord.Id,
                                PaymentMethod = payment.PaymentMethod,
                                PaymentAmount = totalAmountDue,
                                TotalAmountReceived = payment.PaymentAmount,
                                Payee = payment.Payee,
                                ChequeDate = payment.ChequeDate,
                                BankName = payment.BankName,
                                ChequeNo = payment.ChequeNo,
                                DateReceived = DateTime.Now,
                                ChequeAmount = payment.PaymentAmount,
                                AccountName = payment.AccountName,
                                AccountNo = payment.AccountNo,
                                AddedBy = request.AddedBy,
                                Status = Status.Received,
                                OnlinePlatform = payment.OnlinePlatform,
                                Reason = payment.ReferenceNo
                            };

                            await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);

                            var advancePayment = new AdvancePayment
                            {
                                ClientId = transactionSales.Transaction.ClientId,
                                PaymentMethod = payment.PaymentMethod,
                                AdvancePaymentAmount = excessChequeAmount,
                                RemainingBalance = excessChequeAmount,
                                Payee = payment.Payee,
                                ChequeDate = payment.ChequeDate,
                                BankName = payment.BankName,
                                ChequeNo = payment.ChequeNo,
                                DateReceived = payment.DateReceived,
                                ChequeAmount = excessChequeAmount,
                                AccountName = payment.AccountName,
                                AccountNo = payment.AccountNo,
                                AddedBy = payment.AddedBy,
                                Origin = Origin.Excess,
                                PaymentTransactionId = paymentTransaction.Id

                            };
                            transactionSales.Transaction.Status = Status.Paid;
                            transactionSales.RemainingBalance = 0;
                            await _context.AdvancePayments.AddAsync(advancePayment, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                        else
                        {
                            var remainingBal = totalAmountDue - payment.PaymentAmount;
                            transactionSales.RemainingBalance = remainingBal;

                            var paymentTransaction = new PaymentTransaction
                            {
                                TransactionId = transaction.Id,
                                PaymentRecordId = paymentRecord.Id,
                                PaymentMethod = payment.PaymentMethod,
                                PaymentAmount = payment.PaymentAmount,
                                TotalAmountReceived = payment.TotalAmountReceived,
                                Payee = payment.Payee,
                                ChequeDate = payment.ChequeDate,
                                BankName = payment.BankName,
                                ChequeNo = payment.ChequeNo,
                                DateReceived = DateTime.Now,
                                ChequeAmount = payment.ChequeAmount,
                                AccountName = payment.AccountName,
                                AccountNo = payment.AccountNo,
                                AddedBy = request.AddedBy,
                                Status = Status.Received,
                                OnlinePlatform = payment.OnlinePlatform,
                                Reason = payment.ReferenceNo

                            };

                            await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                        
                    }
                    
                    if (payment.PaymentMethod == PaymentMethods.Online)
                    {
                        var amountToPayOnline = request.Payments.Sum(pa => pa.PaymentAmount);

                        var remainingToPay = transaction.TransactionSales.RemainingBalance - payment.PaymentAmount;

                        transaction.TransactionSales.RemainingBalance = remainingToPay <= 0 ? 0 : remainingToPay;
                        var paymentTransaction = new PaymentTransaction
                        {
                            TransactionId = transaction.Id,
                            PaymentRecordId = paymentRecord.Id,
                            PaymentMethod = payment.PaymentMethod,
                            PaymentAmount = payment.PaymentAmount,
                            TotalAmountReceived = payment.TotalAmountReceived,
                            Payee = payment.Payee,
                            DateReceived = DateTime.Now,
                            AccountName = payment.AccountName,
                            AccountNo = payment.AccountNo,
                            OnlinePlatform = payment.OnlinePlatform,
                            ReferenceNo = payment.ReferenceNo,
                            AddedBy = request.AddedBy,
                            Status = Status.Received
                        };

                        var onlinePayment = new OnlinePayment
                        {
                            ClientId = transaction.ClientId,
                            PaymentRecord = paymentRecord,
                            OnlinePaymentName = payment.OnlinePlatform,
                            AccountName = payment.AccountName,
                            AccountNo = payment.AccountNo,
                            PaymentAmount = payment.PaymentAmount,
                            ReferenceNumber = payment.ReferenceNo,
                            CreatedAt = DateTime.Now,
                            AddedBy = request.AddedBy,
                            Status = Status.Received

                        };

                        await _context.OnlinePayments.AddAsync(onlinePayment, cancellationToken);
                        await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        transaction.Status = remainingToPay <= 0 ? Status.Paid : Status.Pending;
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    //For Cash, Cheque,
                    if (payment.PaymentMethod == PaymentMethods.ListingFee ||
                        payment.PaymentMethod == PaymentMethods.Cash)
                    {
                        var amountToPayOthers = request.Payments.Where(pm => pm.PaymentMethod == PaymentMethods.Cash ||
                             pm.PaymentMethod == PaymentMethods.ListingFee ||
                             pm.PaymentMethod == PaymentMethods.OffSet)
                            .Sum(pa => pa.PaymentAmount);

                        // Calculate the remaining amount to pay for this transaction
                        var remainingToPay = amountToPay - amountToPayOthers;


                        // Update the remaining balance of the transaction
                        transaction.TransactionSales.RemainingBalance = remainingToPay < 0 ? 0 : remainingToPay;

                        var paymentTransaction = new PaymentTransaction
                        {
                            TransactionId = transaction.Id,
                            PaymentRecordId = paymentRecord.Id,
                            PaymentMethod = payment.PaymentMethod,
                            PaymentAmount = payment.PaymentAmount,
                            TotalAmountReceived = payment.TotalAmountReceived,
                            Payee = payment.Payee,
                            ChequeDate = payment.ChequeDate,
                            BankName = payment.BankName,
                            ChequeNo = payment.ChequeNo,
                            DateReceived = DateTime.Now,
                            ChequeAmount = payment.ChequeAmount,
                            AccountName = payment.AccountName,
                            AccountNo = payment.AccountNo,
                            AddedBy = request.AddedBy,
                            Status = Status.Received,
                            OnlinePlatform = payment.OnlinePlatform,
                            Reason = payment.ReferenceNo

                        };

                        await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        if (remainingToPay <= 0)
                        {
                            transaction.Status = Status.Paid;
                            payment.PaymentAmount = excessAmount;
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                        else
                        {
                            transaction.TransactionSales.RemainingBalance = remainingToPay < 0 ? 0 : remainingToPay;
                            payment.PaymentAmount = 0;
                            transaction.Status = Status.Pending;
                            await _context.SaveChangesAsync(cancellationToken);
                        }

                        if (payment.PaymentMethod == PaymentMethods.Cheque && excessAmount > 0 && amountToPayOthers <= 0)
                        {
                            var advancePayment = new AdvancePayment
                            {
                                ClientId = transaction.ClientId,
                                PaymentMethod = payment.PaymentMethod,
                                AdvancePaymentAmount = payment.PaymentAmount,
                                RemainingBalance = payment.PaymentAmount,
                                Payee = payment.Payee,
                                ChequeDate = payment.ChequeDate,
                                BankName = payment.BankName,
                                ChequeNo = payment.ChequeNo,
                                DateReceived = payment.DateReceived,
                                ChequeAmount = payment.ChequeAmount,
                                AccountName = payment.AccountName,
                                AccountNo = payment.AccountNo,
                                AddedBy = request.AddedBy,
                                Origin = Origin.Excess
                            };

                            await _context.AdvancePayments.AddAsync(advancePayment, cancellationToken);
                            await _context.SaveChangesAsync(cancellationToken);
                        }
                    }
                } 
            }

            return Result.Success();
        }
    }
}