using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]

public class DeleteVariableDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public DeleteVariableDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("DeleteVariableDiscount/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var command = new DeleteVariableDiscountCommand
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
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class DeleteVariableDiscountCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
    
    public class Handler : IRequestHandler<DeleteVariableDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteVariableDiscountCommand request, CancellationToken cancellationToken)
        {
            var existingVariableDiscount =
                await _context.VariableDiscounts.FirstOrDefaultAsync(discount => discount.Id == request.Id, cancellationToken: cancellationToken);

            if (existingVariableDiscount is null)
            {
                return DiscountErrors.NotFound();
            }
            _context.VariableDiscounts.Remove(existingVariableDiscount);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}