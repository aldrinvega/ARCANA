using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Items;

[Route("api/Items")]
[ApiController]
public class GetItemsAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetItemsAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllItems")]
    public async Task<IActionResult> GetAllItems([FromQuery] GetItemsAsyncQuery request)
    {
        try
        {
            var items = await _mediator.Send(request);
            Response.AddPaginationHeader(
                items.CurrentPage,
                items.PageSize,
                items.TotalCount,
                items.TotalPages,
                items.HasPreviousPage,
                items.HasNextPage
            );
            var results = new
            {

                items,
                items.CurrentPage,
                items.PageSize,
                items.TotalCount,
                items.TotalPages,
                items.HasPreviousPage,
                items.HasNextPage
            };

            var successResult = Result.Success(results);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class GetItemsAsyncQuery : UserParams, IRequest<PagedList<GetItemsAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetItemsAsyncResult
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string Uom { get; set; }
        public string ProductCategory { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string MeatType { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public IEnumerable<PriceChangeHistory> PriceChangeHistories { get; set; }
        public IEnumerable<FuturePriceChange> FuturePriceChanges { get; set; }

        public class PriceChangeHistory
        {
            public int Id { get; set; }
            public decimal Price { get; set; }
            public string EffectivityDate { get; set; }
        }

        public class FuturePriceChange
        {
            public int Id { get; set; }
            public decimal Price { get; set; }
            public string EffectivityDate { get; set; }
        }
        
        
    }

    public class Handler : IRequestHandler<GetItemsAsyncQuery, PagedList<GetItemsAsyncResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetItemsAsyncResult>> Handle(GetItemsAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Items> items = _context.Items
                .Include(x => x.AddedByUser)
                .Include(x => x.Uom)
                .Include(x => x.MeatType)
                .Include(x => x.ItemPriceChange)
                .Include(x => x.ProductSubCategory)
                .ThenInclude(x => x.ProductCategory);

            if (!string.IsNullOrEmpty(request.Search))
            {
                items = items
                    .Where(i => i.ItemCode.Contains(request.Search) || i.ItemDescription.Contains(request.Search));
            }

            if (request.Status != null)
            {
                items = items
                    .Where(i => i.IsActive == request.Status);
            }

            var result = items.Select(i => i.ToGetItemsAsyncResult());

            return await PagedList<GetItemsAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}