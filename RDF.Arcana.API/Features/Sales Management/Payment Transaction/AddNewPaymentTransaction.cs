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
        public int ClientId { get; set; }
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

            foreach (var transactions in request.TransactionId)
            {
                
                var transaction = await _context.Transactions
                    .Include(transactions => transactions.TransactionSales)
                    .FirstOrDefaultAsync(t =>
                        t.Id == transactions,
                    cancellationToken);

                if (transaction is null)
                {
                    return TransactionErrors.NotFound();
                }

                var amountToPay = transaction.TransactionSales.RemainingBalance;

                //For advance payment transactions
                if (request.PaymentMethod == PaymentMethods.AdvancePayment)
                {
                    var advancePayments = await _context.AdvancePayments
                        .Where(x =>
                            x.ClientId == transaction.ClientId &&
                            x.IsActive &&
                            x.Status != Status.Voided)
                        .ToListAsync(cancellationToken);

                    var remainingBalance = advancePayments.Sum(ap => ap.RemainingBalance);

                    foreach (var advancePayment in advancePayments)
                    {
                        decimal remainingToPay;
                        if (advancePayment.RemainingBalance <= amountToPay)
                        {
                            remainingToPay = amountToPay - advancePayment.RemainingBalance;
                            advancePayment.RemainingBalance = 0;
                            transaction.TransactionSales.RemainingBalance = remainingToPay < 0 ? 0 : remainingToPay;
                            transaction.Status = Status.Paid;
                            request.PaymentAmount = 0;

                        }
                        else
                        {
                            advancePayment.RemainingBalance -= amountToPay;
                            remainingToPay = 0;
                            // Update the remaining payment amount
                            request.PaymentAmount -= amountToPay;
                        }

                        var paymentTransaction = new PaymentTransaction
                        {
                            TransactionId = transaction.Id,
                            PaymentRecordId = paymentRecord.Id,
                            PaymentMethod = request.PaymentMethod,
                            PaymentAmount = request.PaymentAmount,
                            TotalAmountReceived = request.TotalAmountReceived,
                            Payee = request.Payee,
                            ChequeDate = request.ChequeDate,
                            BankName = request.BankName,
                            ChequeNo = request.ChequeNo,
                            DateReceived = DateTime.Now,
                            ChequeAmount = request.ChequeAmount,
                            AccountName = request.AccountName,
                            AccountNo = request.AccountNo,
                            AddedBy = request.AddedBy,
                            Status = Status.Received,
                        };

                        await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        // Break the loop if the payment amount can cover the total amount due
                        if (remainingToPay == 0)
                        {
                            continue;
                        }
                    }
                }

                //For Cash, Cheque, and Online payments
                if( request.PaymentMethod == PaymentMethods.Cheque ||
                    request.PaymentMethod == PaymentMethods.ListingFee ||
                    request.PaymentMethod == PaymentMethods.Online ||
                    request.PaymentMethod == PaymentMethods.Cash)
                {
                    // Calculate the remaining amount to pay for this transaction
                    var remainingToPay = amountToPay - request.PaymentAmount;

                    // Update the remaining balance of the transaction
                    transaction.TransactionSales.RemainingBalance = remainingToPay < 0 ? 0 : remainingToPay;

                    var paymentTransaction = new PaymentTransaction
                    {
                        TransactionId = transaction.Id,
                        PaymentRecordId = paymentRecord.Id,
                        PaymentMethod = request.PaymentMethod,
                        PaymentAmount = request.PaymentAmount,
                        TotalAmountReceived = request.TotalAmountReceived,
                        Payee = request.Payee,
                        ChequeDate = request.ChequeDate,
                        BankName = request.BankName,
                        ChequeNo = request.ChequeNo,
                        DateReceived = DateTime.Now,
                        ChequeAmount = request.ChequeAmount,
                        AccountName = request.AccountName,
                        AccountNo = request.AccountNo,
                        AddedBy = request.AddedBy,
                        Status = Status.Received,

                    };

                    await _context.PaymentTransactions.AddAsync(paymentTransaction, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    // Update the remaining payment amount
                    request.PaymentAmount -= amountToPay;

                    if (remainingToPay <= 0)
                    {
                        transaction.Status = Status.Paid;
                        await _context.SaveChangesAsync(cancellationToken);
                        continue; 
                    }
                    else
                    {
                        transaction.TransactionSales.RemainingBalance = remainingToPay < 0 ? 0 : remainingToPay;
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                    if (request.PaymentAmount > 0)
                    {
                        var advancePayment = new AdvancePayment
                        {
                            ClientId = request.ClientId,
                            PaymentMethod = request.PaymentMethod,
                            AdvancePaymentAmount = request.PaymentAmount,
                            RemainingBalance = request.PaymentAmount,
                            Payee = request.Payee,
                            ChequeDate = request.ChequeDate,
                            BankName = request.BankName,
                            ChequeNo = request.ChequeNo,
                            DateReceived = request.DateReceived,
                            ChequeAmount = request.ChequeAmount,
                            AccountName = request.AccountName,
                            AccountNo = request.AccountNo,
                            AddedBy = request.AddedBy,
                            Origin = Origin.Excess
                        };

                        await _context.AdvancePayments.AddAsync(advancePayment, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }

            

            return Result.Success();
        }
    }
}