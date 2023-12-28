using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Items;

namespace RDF.Arcana.API.Features.Setup.Price_Change;
[Route("api/PriceChange"), ApiController]

public class AddPriceChange : ControllerBase
{
    private readonly IMediator _mediator;

    public AddPriceChange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewPriceChange")]
    public async Task<IActionResult> Add([FromBody]AddPriceChangeCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class AddPriceChangeCommand : IRequest<Result>
    {
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectivityDate { get; set; }
    }

    public class Handler : IRequestHandler<AddPriceChangeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddPriceChangeCommand request, CancellationToken cancellationToken)
        {
            var validateItem = await _context.Items.FirstOrDefaultAsync(item => 
                item.Id == request.ItemId, cancellationToken);

            if (validateItem is null)
            {
                return ItemErrors.NotFound(request.ItemId);
            }

            var existingPriceChange = await _context.ItemPriceChanges.FirstOrDefaultAsync(pc =>
                pc.EffectivityDate == request.EffectivityDate && pc.ItemId == request.ItemId, cancellationToken);

            if (existingPriceChange is not null)
            {
                existingPriceChange.Price = request.Price;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
                // Else add new price change
                var newPriceChange = new ItemPriceChange
                {
                    ItemId = request.ItemId,
                    Price = request.Price,
                    EffectivityDate = request.EffectivityDate
                };
                await _context.AddAsync(newPriceChange, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}