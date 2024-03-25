using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/price-mode-items"), ApiController]
    public class GetAllItemsForPriceModeByPriceModeCode : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllItemsForPriceModeByPriceModeCode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllItemsForPriceModeQuery query)
        {
            try
            {

                var priceModeItems = await _mediator.Send(query);

                Response.AddPaginationHeader(
                priceModeItems.CurrentPage,
                priceModeItems.PageSize,
                priceModeItems.TotalCount,
                priceModeItems.TotalPages,
                priceModeItems.HasPreviousPage,
                priceModeItems.HasNextPage
            );
                var results = new
                {

                    priceModeItems,
                    priceModeItems.CurrentPage,
                    priceModeItems.PageSize,
                    priceModeItems.TotalCount,
                    priceModeItems.TotalPages,
                    priceModeItems.HasPreviousPage,
                    priceModeItems.HasNextPage
                };

                var successResult = Result.Success(results);
                return Ok(successResult);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetAllItemsForPriceModeQuery : UserParams, IRequest<PagedList<GetAllItemsForPriceModeResult>>
        {
            public int? PriceModeId { get; set; }
            public string Search { get; set; }
            public bool? Status { get; set; }
        }

        public class GetAllItemsForPriceModeResult
        {
            public int PriceModeItemId { get; set; }
            public int PriceModeId { get; set; }
            public string PriceModeCode { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string ItemImageLink { get; set; }
            public string Uom { get; set; }
            public string ProductSubCategoryName { get; set; }
            public string MeatType { get; set; }
            public bool IsActive { get; set; }
            public string ModifiedBy { get; set; }
            public decimal? CurrentPrice { get; set; }
            
        }

        public class Handler : IRequestHandler<GetAllItemsForPriceModeQuery, PagedList<GetAllItemsForPriceModeResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetAllItemsForPriceModeResult>> Handle(GetAllItemsForPriceModeQuery request, CancellationToken cancellationToken)
            {
                var now = DateTime.Now;
                IQueryable<Domain.PriceModeItems> priceModeItems = _context.PriceModeItems
                    .Include(x => x.PriceMode)
                    .Include(i => i.Item)
                    .ThenInclude(x => x.Uom)
                    .Include(i => i.Item)
                    .ThenInclude(x => x.MeatType)
                    .Include(i => i.Item)
                    .ThenInclude(x => x.ProductSubCategory)
                    .Where(x => x.PriceMode.IsActive);

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    priceModeItems = priceModeItems.Where(pmi => 
                    pmi.Item.ItemCode.Contains(request.Search) ||   
                    pmi.Item.ItemDescription.Contains(request.Search) ||
                    pmi.Item.Uom.UomDescription.Contains(request.Search) ||
                    pmi.Item.Uom.UomCode.Contains(request.Search) ||
                    pmi.PriceMode.PriceModeCode.Contains(request.Search) ||
                    pmi.PriceMode.PriceModeDescription.Contains(request.Search));
                }

                if(request.Status is not null)
                {
                    priceModeItems = priceModeItems.Where(pmi => pmi.IsActive == request.Status);
                }

                if (request.PriceModeId is not null)
                {
                    priceModeItems = priceModeItems.Where(pmi => pmi.PriceModeId == request.PriceModeId);
                }

                var result = priceModeItems.Select(pm => new GetAllItemsForPriceModeResult
                {
                    PriceModeItemId = pm.Id,
                    PriceModeId = pm.PriceModeId,
                    PriceModeCode = pm.PriceMode.PriceModeCode,
                    ItemId = pm.ItemId,
                    ItemCode = pm.Item.ItemCode,
                    ItemDescription = pm.Item.ItemDescription,
                    ItemImageLink = pm.Item.ItemImageLink,
                    Uom = pm.Item.Uom.UomCode,
                    MeatType = pm.Item.MeatType.MeatTypeName,
                    ProductSubCategoryName = pm.Item.ProductSubCategory.ProductSubCategoryName,
                    IsActive = pm.IsActive,
                    CurrentPrice = pm.ItemPriceChanges
                            .OrderByDescending(p => p.EffectivityDate)
                            .First(pc => pc.EffectivityDate <= now).Price
                }).OrderBy(x => x.ItemCode);

                return await PagedList<GetAllItemsForPriceModeResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
