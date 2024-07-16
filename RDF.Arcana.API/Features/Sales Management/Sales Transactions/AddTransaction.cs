using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Esf;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Setup.Items;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;
[Route("api/sales-transaction"), ApiController]

public class AddTransaction : ControllerBase
{
    private readonly IMediator _mediator;

    public AddTransaction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddtransactionCommand command)
    {
        try
        {

            if (User.Identity is ClaimsIdentity identity
               && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    public class AddtransactionCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public int AddedBy { get; set; }
        public ICollection<Item> Items { get; set; }
        public decimal SpecialDiscount { get; set; }
        public decimal Discount { get; set; }
        public string ChargeInvoiceNo { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }
        public class Item
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
            public int UnitPrice { get; set; }
        }
    }

    public class AddNewTransactionResult
    {
        public int TransactionNo { get; set; }
        public int ClientId { get; set; }
        public string BusinessName { get; set; }
        public BusinessAddressResult BusinessAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ChargeInvoiceNo { get; set; }
        public int AddedBy { get; set; }
        public ICollection<Item> Items { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }

        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalAmountDue { get; set; }
        public decimal RemainingBalance { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AddVat { get; set; }
        public decimal VatableSales { get; set; }
        public decimal VatExemptSales { get; set; }
        public decimal ZeroRatedSales { get; set; }
        public decimal VatAmount { get; set; }

        public class Item
        {
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public int Quantity { get; set; }
            public int UnitPrice { get; set; }
            public decimal Amount { get; set; }
        }

        public class BusinessAddressResult
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddtransactionCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddtransactionCommand request, CancellationToken cancellationToken)
        {
            List<AddNewTransactionResult.Item> itemsCollection = new();

            var existingClient = await _context
                .Clients
                .Include(x => x.BusinessAddress)
                .Include(cl => cl.Term)
                .ThenInclude(cl => cl.Terms)
                .FirstOrDefaultAsync(cl => 
                cl.Id == request.ClientId,
                cancellationToken);

            

            if (existingClient == null)
            {
                return ClientErrors.NotFound();
            }

            if ((request.InvoiceType == Status.Charge || request.InvoiceType == Status.Sales) &&
                (string.IsNullOrEmpty(request.InvoiceNo) || request.InvoiceNo == "string") ||
                (request.InvoiceType != Status.Charge && request.InvoiceType != Status.Sales))
            {
                if (request.InvoiceType != Status.Charge && request.InvoiceType != Status.Sales)
                {
                    return TransactionErrors.SICI();
                }
                else
                {
                    return TransactionErrors.InvalidInvoiceNumber();
                }
            }
            var existingInvoice = await _context.Transactions.AnyAsync(ts => ts.InvoiceNo == request.InvoiceNo);

            if (existingInvoice)
            {
                return TransactionErrors.InvoiceAlreadyExist(request.InvoiceNo);
            }


            var items = request.Items.Select(items => new
            {
                items.ItemId,
                items.Quantity,
                items.UnitPrice,
                Amount = items.UnitPrice * items.Quantity,
            });

            // Calculate the subtotal for the items purchased
            var subTotal = items.Sum(x => x.Amount);

            if (existingClient.Term.Terms.TermType == Common.Terms.CreditLimit)
            {
                
                var creditLimit = existingClient.Term.CreditLimit;
                if (creditLimit < subTotal)
                {
                    return TransactionErrors.CreditLimitExceeded();

                }

            }

            //Add new transaction
            var transaction = new Transactions
            {
                ClientId = request.ClientId,
                Status = Status.Pending,
                AddedBy = request.AddedBy,
                InvoiceNo = request.InvoiceNo,
                InvoiceType = request.InvoiceType
            };

            //Add and save to database
            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var item in request.Items)
            {
                var exisitngitems = await _context.Items.FirstOrDefaultAsync(i => i.Id == item.ItemId, cancellationToken);

                if (exisitngitems == null)
                {
                    return ItemErrors.NotFound(item.ItemId);
                }

                //Add items
                var transactionItems = new TransactionItems
                {
                    TransactionId = transaction.Id,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Amount = item.UnitPrice * item.Quantity,
                    AddedBy = request.AddedBy
                };

                //Get the item details inserted by Item Id
                var itemDetails = await _context.Items
                    .Include(x => x.Uom)
                    .Where(i => i.Id == item.ItemId)
                    .Select(i => new { i.ItemCode, i.ItemDescription, i.Uom.UomCode })
                    .FirstOrDefaultAsync(cancellationToken);

                itemsCollection.Add(new AddNewTransactionResult.Item
                {
                    ItemId = item.ItemId,
                    ItemCode = itemDetails.ItemCode,
                    ItemDescription = itemDetails.ItemDescription,
                    Uom = itemDetails.UomCode,
                    UnitPrice = item.UnitPrice,
                    Quantity = item.Quantity,
                    Amount = transactionItems.Amount
                });

                //Add and save to database
                await _context.TransactionItems.AddAsync(transactionItems, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);


            }

            //Calculate sales
            

            // Get the total discount percentage
            var totalDiscount = request.Discount + request.SpecialDiscount;

            // Calculate the discount amount based on the total discount percentage
            var discountAmount = subTotal * (totalDiscount / 100);

            var specialDiscountAmount = subTotal * (request.SpecialDiscount / 100);

            var userDiscount = subTotal * (request.Discount / 100);

            var totalSales = subTotal - discountAmount;

            // Calculate the vatable sales (total sales before VAT)
            var vatableSales = (subTotal - discountAmount) / VATCalculations.VAT;
            vatableSales = Math.Round(vatableSales, 2);

            // Calculate the VAT amount based on the vatable sales and the VAT rate
            var vatAmount = totalSales - vatableSales;
            vatAmount = Math.Round(vatAmount, 2);

            var amountDue = totalSales - vatAmount;

            // Calculate the total amount due (total sales including VAT)
            var totalAmountDue = vatableSales + vatAmount;
            totalAmountDue = Math.Round(totalAmountDue, 2);

            // AddVat is the same as the VAT amount
            var addVat = vatAmount;


            // Create and save the transaction sales record
            var newTransactionSales = new TransactionSales
            {
                TransactionId = transaction.Id,
                TotalSales = totalSales,
                VatableSales = vatableSales,
                SubTotal = subTotal,
                AmountDue = amountDue, 
                TotalAmountDue = totalAmountDue,
                Discount = request.Discount / 100, 
                DiscountAmount = userDiscount,
                SpecialDiscount = request.SpecialDiscount / 100, 
                SpecialDiscountAmount = specialDiscountAmount, 
                VatAmount = vatAmount,
                ChargeInvoiceNo = request.ChargeInvoiceNo,
                AddVat = addVat,
                RemainingBalance = totalAmountDue,
                AddedBy = request.AddedBy
            };

            await _context.TransactionSales.AddAsync(newTransactionSales, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            var result = new AddNewTransactionResult
            {
                TransactionNo = newTransactionSales.TransactionId,
                ClientId = transaction.ClientId,
                BusinessName = transaction.Client.BusinessName,
                BusinessAddress = new AddNewTransactionResult.BusinessAddressResult
                {
                    Province = transaction.Client.BusinessAddress.Province,
                    City = transaction.Client.BusinessAddress.City,
                    BarangayName = transaction.Client.BusinessAddress.Barangay,
                    StreetName = transaction.Client.BusinessAddress.StreetName,
                    HouseNumber = transaction.Client.BusinessAddress.HouseNumber,
                },
                CreatedAt = transaction.CreatedAt,
                ChargeInvoiceNo = newTransactionSales.ChargeInvoiceNo,
                Items = itemsCollection,
                Subtotal = newTransactionSales.SubTotal,
                VatableSales = newTransactionSales.VatableSales,
                TotalSales = newTransactionSales.TotalSales,
                AmountDue = newTransactionSales.AmountDue,
                AddVat = newTransactionSales.AddVat,
                TotalAmountDue = newTransactionSales.TotalAmountDue,
                RemainingBalance = newTransactionSales.RemainingBalance,
                DiscountAmount = discountAmount,
                VatAmount = newTransactionSales.VatAmount,
                VatExemptSales = newTransactionSales.VatExemptSales,
                ZeroRatedSales = newTransactionSales.ZeroRatedSales
            };

            return Result.Success(result);

        }
    }
}

