using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]
public class UpdateVariableDiscountStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateVariableDiscountStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateVariableDiscountStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateDiscountStatusCommand
            {
                Id = id,
                ModifiedBy = User.Identity?.Name
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

    public class UpdateDiscountStatusCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDiscountStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateDiscountStatusCommand request, CancellationToken cancellationToken)
        {
            var existingDiscount =
                await _context.VariableDiscounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingDiscount is null)
            {
                return DiscountErrors.NotFound();
            }

            existingDiscount.IsActive = !existingDiscount.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}