using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/price-mode"), ApiController]
    public class GetAllPriceMode : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllPriceMode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("page")]
        public async Task<IActionResult> Get([FromQuery]GetAllPriceModeQuery query)
        {
            try
            {
                var priceMode = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    priceMode.CurrentPage,
                    priceMode.PageSize,
                    priceMode.TotalCount,
                    priceMode.TotalPages,
                    priceMode.HasPreviousPage,
                    priceMode.HasNextPage
                    );
                var results = new
                {

                    priceMode,
                    priceMode.CurrentPage,
                    priceMode.PageSize,
                    priceMode.TotalCount,
                    priceMode.TotalPages,
                    priceMode.HasPreviousPage,
                    priceMode.HasNextPage
                };

                var successResult = Result.Success(results);
                return Ok(successResult);
            }
            catch (Exception e)
            {
                return Conflict(e.Message);
            }
        }
        public class GetAllPriceModeQuery : UserParams, IRequest<PagedList<GetAllPriceModeResult>>
        {
            public string Search { get; set; }
            public bool? Status { get; set; }
        }

        public class GetAllPriceModeResult
        {
            public int Id { get; set; }
            public string PriceModeCode { get; set; }
            public string PriceModeDescription { get; set; }
            public string CreatedAt { get; set; }
            public string UpdatedAt { get; set; }
            public bool IsActive { get; set; }
        }

        public class Handler : IRequestHandler<GetAllPriceModeQuery, PagedList<GetAllPriceModeResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<GetAllPriceModeResult>> Handle(GetAllPriceModeQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PriceMode> priceMode = _context.PriceMode;

                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    priceMode = priceMode.Where(pm => pm.PriceModeCode.Contains(request.Search) || pm.PriceModeDescription.Contains(request.Search));
                }

                if(request.Status is not null)
                {
                    priceMode = priceMode.Where(x => x.IsActive == request.Status);
                }

                var result = priceMode.Select(pm => new GetAllPriceModeResult
                {
                    Id = pm.Id,
                    PriceModeCode = pm.PriceModeCode,
                    PriceModeDescription = pm.PriceModeDescription,
                    CreatedAt = pm.CreatedAt.ToString(),
                    UpdatedAt = pm.UpdatedAt.ToString(),
                    IsActive = pm.IsActive
                });

                return await PagedList<GetAllPriceModeResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            }
        }
    }
}
