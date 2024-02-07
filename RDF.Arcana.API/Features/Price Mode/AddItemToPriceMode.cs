using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Price_Mode
{
    [Route("api/price-mode-items"), ApiController]
    public class AddItemToPriceMode : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddItemToPriceMode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddItemToPriceModeCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AddedBy = userId;
                }

                var result = await _mediator.Send(command);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        public class AddItemToPriceModeCommand : IRequest<Result>
        {
            public int PriceModeId { get; set; }
            public int ItemId { get; set; }
            public decimal Price { get; set; }
            public int AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<AddItemToPriceModeCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddItemToPriceModeCommand request, CancellationToken cancellationToken)
            {
                var currentDateTime = DateTime.Now;
                var roundedDateTime = new DateTime(
                    currentDateTime.Year,
                    currentDateTime.Month,
                    currentDateTime.Day,
                    currentDateTime.Hour,
                    currentDateTime.Minute,
                    0 // Set seconds to zero
                );

                var exisitingItem = await _context.PriceModeItems
                .Include(i => i.Item)
                    .Where(pmi =>
                    pmi.PriceModeId == request.PriceModeId &&
                    pmi.IsActive
                    ).ToListAsync(cancellationToken);


                    var priceModeItem = exisitingItem.FirstOrDefault(x => x.ItemId == request.ItemId);


                    if (priceModeItem != null)
                    {
                        return PriceModeItemsErrors.AlreadyExist(priceModeItem.Item.ItemCode);
                    }
                    else
                    {
                        var newPriceModeItem = new PriceModeItems
                        {
                            PriceModeId = request.PriceModeId,
                            AddedBy = request.AddedBy,
                            ItemId = request.ItemId,
                        };

                        await _context.PriceModeItems.AddAsync(newPriceModeItem, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        var priceChange = new ItemPriceChange
                        {
                            PriceModeItemId = newPriceModeItem.Id,
                            Price = request.Price,
                            AddedBy = request.AddedBy,
                            EffectivityDate = roundedDateTime
                        };

                        await _context.AddAsync(priceChange, cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);
                    }

                return Result.Success();
            }
        }
    }
}
