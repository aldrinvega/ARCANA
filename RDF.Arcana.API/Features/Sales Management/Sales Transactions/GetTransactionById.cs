using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;

[Route("api/sales-transaction"), ApiController]

public class GetTransactionById : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTransactionById(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var query = new GetTransactionByIdQuery
            {
                TranasctionId = id
            };
            
            var result = await _mediator.Send(query);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetTransactionByIdQuery : IRequest<Result>
    {
        public int TranasctionId { get; set; }
    }

    public class GetTransactionByIdResult
    {
        public int TransactionNo { get; set; }
        public int ClientId { get; set; }
        public string BusinessName { get; set; }
        public string InvoiceNo { get; set; }
        public string InvoiceType { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public BusinessAddressResult BusinessAddress { get; set; }

        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalAmountDue { get; set; }
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
            public decimal UnitPrice { get; set; }
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

        public decimal DiscountPercentage { get; set; }
        public string Remarks { get; set; }
    }
    
    public class Handler : IRequestHandler<GetTransactionByIdQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            var existingTransaction = await _context.Transactions
                .Include(ti => ti.TransactionItems)
                .ThenInclude(transactionItems => transactionItems.Item)
                .ThenInclude(items => items.Uom)
                .Include(ts => ts.TransactionSales)
                .Include(transactions => transactions.Client)
                .ThenInclude(clients => clients.BusinessAddress)
                .FirstOrDefaultAsync(t => t.Id == request.TranasctionId, cancellationToken);

            if (existingTransaction is null)
            {
                return TransactionErrors.NotFound();
            }

            var result = new GetTransactionByIdResult
            {
                TransactionNo = existingTransaction.Id,
                ClientId = existingTransaction.ClientId,
                BusinessName = existingTransaction.Client.BusinessName,
                CreatedAt = existingTransaction.CreatedAt,
                InvoiceNo = existingTransaction.InvoiceNo,
                InvoiceType = existingTransaction.InvoiceType,
                BusinessAddress = new GetTransactionByIdResult.BusinessAddressResult
                {
                    HouseNumber = existingTransaction.Client.BusinessAddress.HouseNumber,
                    StreetName = existingTransaction.Client.BusinessAddress.StreetName,
                    BarangayName = existingTransaction.Client.BusinessAddress.Barangay,
                    City = existingTransaction.Client.BusinessAddress.City,
                    Province = existingTransaction.Client.BusinessAddress.Province
                },
                Items = existingTransaction.TransactionItems.Select(ti => new GetTransactionByIdResult.Item
                {
                    ItemId = ti.ItemId,
                    ItemCode = ti.Item.ItemCode,
                    ItemDescription = ti.Item.ItemDescription,
                    Uom = ti.Item.Uom.UomCode,
                    Quantity = ti.Quantity,
                    UnitPrice = ti.UnitPrice,
                    Amount = ti.Amount
                }),
                Subtotal = existingTransaction.TransactionSales.SubTotal,
                DiscountAmount = existingTransaction.TransactionSales.DiscountAmount +
                                 existingTransaction.TransactionSales.SpecialDiscountAmount,
                TotalSales = existingTransaction.TransactionSales.TotalSales,
                TotalAmountDue = existingTransaction.TransactionSales.TotalAmountDue,
                AmountDue = existingTransaction.TransactionSales.AmountDue,
                AddVat = existingTransaction.TransactionSales.AddVat,
                VatableSales = existingTransaction.TransactionSales.VatableSales,
                VatExemptSales = existingTransaction.TransactionSales.VatExemptSales,
                ZeroRatedSales = existingTransaction.TransactionSales.ZeroRatedSales,
                VatAmount = existingTransaction.TransactionSales.VatAmount,
                DiscountPercentage = existingTransaction.TransactionSales.Discount + 
                                     existingTransaction.TransactionSales.SpecialDiscount,

                Remarks = existingTransaction.TransactionSales.Remarks
            };
            
            return Result.Success(result);
        }
    }
}