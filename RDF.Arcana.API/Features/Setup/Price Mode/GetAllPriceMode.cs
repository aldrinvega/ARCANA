using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Drawing.Text;

namespace RDF.Arcana.API.Features.Setup.Price_Mode
{
    [Route("api/price-mode"), ApiController]
    public class GetAllPriceMode :ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllPriceMode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllStoreTypes([FromQuery] GetAllpriceModeQuery query)
        {
            try
            {
                var pricemodes = await _mediator.Send(query);

                Response.AddPaginationHeader(
                    pricemodes.CurrentPage,
                    pricemodes.PageSize,
                    pricemodes.TotalCount,
                    pricemodes.TotalPages,
                    pricemodes.HasPreviousPage,
                    pricemodes.HasNextPage
                    );

                var result = new
                {
                    pricemodes,
                    pricemodes.CurrentPage,
                    pricemodes.PageSize,
                    pricemodes.TotalCount,
                    pricemodes.TotalPages,
                    pricemodes.HasPreviousPage,
                    pricemodes.HasNextPage
                };

                var successResult = Result.Success(result);
                return Ok(successResult);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetAllpriceModeQuery : UserParams, IRequest<PagedList<PriceModeResult>>
        {
            public string Search { get; set; }
            public bool? Status { get; set; }
        }

        public class PriceModeResult
        {
            public int Id { get; set; }
            public string PriceModeCode { get; set; }
            public string PriceModeDescription { get; set; }
            public bool IsActive { get; set; }
            public string AddedBy { get; set; }
            public string CreatedAt { get; set; }
            public string UpdatedAt { get; set; }
        }

        public class Handler : IRequestHandler<GetAllpriceModeQuery, PagedList<PriceModeResult>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<PagedList<PriceModeResult>> Handle(GetAllpriceModeQuery request, CancellationToken cancellationToken)
            {
                IQueryable<PriceMode> priceMode = _context.PriceMode
                    .Include(ab => ab.AddedByUser);

                if(!string.IsNullOrEmpty(request.Search))
                {
                    priceMode = priceMode.Where(p => p.PriceModeCode.Contains(request.Search) || p.PriceModeDescription.Contains(request.Search));
                }

                if(request.Status != null)
                {
                    priceMode = priceMode.Where(pm => pm.IsActive == request.Status);
                }

                var priceModeResult = priceMode.Select(pm => new PriceModeResult
                {
                    Id = pm.Id,

                    PriceModeCode = pm.PriceModeCode,
                    IsActive = pm.IsActive,
                    PriceModeDescription = pm.PriceModeDescription,
                    AddedBy = pm.AddedByUser.Fullname,
                    CreatedAt = pm.CreatedAt.ToString("MM-dd-yyyy"),
                    UpdatedAt = pm.UpdatedAt.ToString("MM-dd-yyyy")
                });


                return await PagedList<PriceModeResult>.CreateAsync(priceModeResult, request.PageNumber, request.PageSize);
            }
        }
    }
}
