using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions
{
    [Route("api/sales-transaction"), ApiController]
    public class GetAllTransactions : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllTransactions(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("page")]
        public async Task<IActionResult> Get([FromQuery] GetAllTransactionsQuery query)
        {
            try
            {
                var transactions = await _mediator.Send(query);
                
                Response.AddPaginationHeader(
                    transactions.CurrentPage, 
                    transactions.PageSize, 
                    transactions.TotalCount, 
                    transactions.TotalPages, 
                    transactions.HasNextPage,
                    transactions.HasPreviousPage);
                var result = new
                {
                    transactions,
                    transactions.CurrentPage,
                    transactions.PageSize,
                    transactions.TotalCount,
                    transactions.TotalPages,
                    transactions.HasNextPage,
                    transactions.HasPreviousPage
                };

                var successResult = Result.Success(result);

                return Ok(successResult);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetAllTransactionsQuery : UserParams, IRequest<PagedList<GetAllTransactionQueryResult>>
        {
            public string Search { get; set; }
            public bool? Status { get; set; }
            public string TransactionStatus { get; set; }
        }

        public class GetAllTransactionQueryResult
        {
            public int TransactionNo { get; set; }
            public int ClientId { get; set; }
            public string BusinessName { get; set; }
            public BusinessAddressResult BusinessAddress { get; set; }
            public DateTime CreatedAt { get; set; }
            public string ChargeInvoiceNo { get; set; }
            public string AddedBy { get; set; }
            public IEnumerable<Item> Items { get; set; }

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

            public class Handler : IRequestHandler<GetAllTransactionsQuery, PagedList<GetAllTransactionQueryResult>>
            {
                private readonly ArcanaDbContext _context;

                public Handler(ArcanaDbContext context)
                {
                    _context = context;
                }

                public Task<PagedList<GetAllTransactionQueryResult>> Handle(GetAllTransactionsQuery request,
                    CancellationToken cancellationToken)
                {
                    IQueryable<Transactions> transactions = _context.Transactions
                        .Include(x => x.TransactionItems);


                    if (!string.IsNullOrEmpty(request.Search))
                    {
                        transactions = transactions.Where(t =>
                            t.Client.Fullname.Contains(request.Search) &&
                            t.Client.BusinessName.Contains(request.Search) &&
                            t.TransactionSales.ChargeInvoiceNo.Contains(request.Search)
                        );
                    }

                    if (request.Status != null)
                    {
                        transactions = transactions.Where(t => t.IsActive == request.Status);
                    }

                    if (request.TransactionStatus == Status.Voided)
                    {
                        var voidedTransactions = transactions.Where(vt => vt.Status == Status.Voided);
                        
                        var voidedResult = voidedTransactions.Select(result => new GetAllTransactionQueryResult
                        {
                            TransactionNo = result.Id,
                            ClientId = result.ClientId,
                            BusinessName = result.Client.BusinessName,
                            BusinessAddress = new BusinessAddressResult
                            {
                                HouseNumber = result.Client.BusinessAddress.HouseNumber,
                                StreetName = result.Client.BusinessAddress.StreetName,
                                BarangayName = result.Client.BusinessAddress.Barangay,
                                City = result.Client.BusinessAddress.City,
                                Province = result.Client.BusinessAddress.Province,
                            },
                            CreatedAt = result.CreatedAt,
                            ChargeInvoiceNo = result.TransactionSales.ChargeInvoiceNo,
                            AddedBy = result.AddedByUser.Fullname,
                            Items = result.TransactionItems.Select(ti => new Item
                            {
                                ItemId = ti.ItemId,
                                ItemCode = ti.Item.ItemCode,
                                ItemDescription = ti.Item.ItemDescription,
                                Uom = ti.Item.Uom.UomCode,
                                Quantity = ti.Quantity,
                                UnitPrice = ti.UnitPrice,
                                Amount = ti.Amount
                            }),
                            Subtotal = result.TransactionSales.SubTotal,
                            DiscountAmount = result.TransactionSales.DiscountAmount +
                                             result.TransactionSales.SpecialDiscountAmount,
                            TotalSales = result.TransactionSales.TotalSales,
                            TotalAmountDue = result.TransactionSales.TotalAmountDue,
                            AmountDue = result.TransactionSales.AmountDue,
                            AddVat = result.TransactionSales.AddVat,
                            VatableSales = result.TransactionSales.VatableSales,
                            VatExemptSales = result.TransactionSales.VatExemptSales,
                            ZeroRatedSales = result.TransactionSales.ZeroRatedSales,
                            VatAmount = result.TransactionSales.VatAmount
                        });

                        return PagedList<GetAllTransactionQueryResult>.CreateAsync(voidedResult, request.PageNumber,
                            request.PageSize);
                    }

                    var result = transactions.Select(result => new GetAllTransactionQueryResult
                    {
                        TransactionNo = result.Id,
                        ClientId = result.ClientId,
                        BusinessName = result.Client.BusinessName,
                        BusinessAddress = new BusinessAddressResult
                        {
                            HouseNumber = result.Client.BusinessAddress.HouseNumber,
                            StreetName = result.Client.BusinessAddress.StreetName,
                            BarangayName = result.Client.BusinessAddress.Barangay,
                            City = result.Client.BusinessAddress.City,
                            Province = result.Client.BusinessAddress.Province,
                        },
                        CreatedAt = result.CreatedAt,
                        ChargeInvoiceNo = result.TransactionSales.ChargeInvoiceNo,
                        AddedBy = result.AddedByUser.Fullname,
                        Items = result.TransactionItems.Select(ti => new Item
                        {
                            ItemId = ti.ItemId,
                            ItemCode = ti.Item.ItemCode,
                            ItemDescription = ti.Item.ItemDescription,
                            Uom = ti.Item.Uom.UomCode,
                            Quantity = ti.Quantity,
                            UnitPrice = ti.UnitPrice,
                            Amount = ti.Amount
                        }),
                        Subtotal = result.TransactionSales.SubTotal,
                        DiscountAmount = result.TransactionSales.DiscountAmount +
                                         result.TransactionSales.SpecialDiscountAmount,
                        TotalSales = result.TransactionSales.TotalSales,
                        TotalAmountDue = result.TransactionSales.TotalAmountDue,
                        AmountDue = result.TransactionSales.AmountDue,
                        AddVat = result.TransactionSales.AddVat,
                        VatableSales = result.TransactionSales.VatableSales,
                        VatExemptSales = result.TransactionSales.VatExemptSales,
                        ZeroRatedSales = result.TransactionSales.ZeroRatedSales,
                        VatAmount = result.TransactionSales.VatAmount
                    });

                    return PagedList<GetAllTransactionQueryResult>.CreateAsync(result, request.PageNumber,
                        request.PageSize);
                }

            }
        }
    }
}