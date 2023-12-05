using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Price_Change;

[Route("api/PriceChange"), ApiController]

public class DeletePriceChange : ControllerBase
{
    private readonly IMediator _mediator;

    public DeletePriceChange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("DeletePriceChange/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeletePriceChangeCommand
            {
                Id = id
            };

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
    public sealed record DeletePriceChangeCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
    public class Handler : IRequestHandler<DeletePriceChangeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeletePriceChangeCommand request, CancellationToken cancellationToken)
        {
            var priceChange = await _context.ItemPriceChanges
                .FirstOrDefaultAsync(pc => pc.Id == request.Id, cancellationToken);

            if (priceChange is null)
            {
                return PriceChangeErrors.NotFound();
            }
            _context.ItemPriceChanges.Remove(priceChange);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}