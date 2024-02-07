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
    public class GetAllPriceModeForClients :ControllerBase
    {
        private readonly IMediator _mediator;

        public GetAllPriceModeForClients(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllStoreTypes()
        {
            try
            {
                var query = new GetAllPriceModeQuery();
                var pricemodes = await _mediator.Send(query);

                return Ok(pricemodes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class GetAllPriceModeQuery : IRequest<Result> {}

        public class PriceModeResult
        {
            public int Id { get; set; }
            public string PriceModeCode { get; set; }
            public string PriceModeDescription { get; set; }
            public bool IsActive { get; set; }
            public string CreatedAt { get; set; }
            public string UpdatedAt { get; set; }
        }

        public class Handler : IRequestHandler<GetAllPriceModeQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllPriceModeQuery request, CancellationToken cancellationToken)
            {
                var priceMode = await _context.PriceMode.ToListAsync(cancellationToken);

                var priceModeResult = priceMode.Select(pm => new PriceModeResult
                {
                    Id = pm.Id,

                    PriceModeCode = pm.PriceModeCode,
                    IsActive = pm.IsActive,
                    PriceModeDescription = pm.PriceModeDescription,
                    CreatedAt = pm.CreatedAt.ToString("MM-dd-yyyy"),
                    UpdatedAt = pm.UpdatedAt.ToString("MM-dd-yyyy")
                });

                return Result.Success(priceModeResult);
            }
        }
    }
}
