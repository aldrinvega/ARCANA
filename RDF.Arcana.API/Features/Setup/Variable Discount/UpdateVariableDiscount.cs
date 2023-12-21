using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Variable_Discount;

[Route("api/VariableDiscount")]
[ApiController]
public class UpdateVariableDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateVariableDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateVariableDiscount/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] UpdateDiscountCommand command)
    {
        try
        {
            command.Id = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class UpdateDiscountCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var existingDiscount = await _context.VariableDiscounts.FindAsync(request.Id);

            if (existingDiscount == null)
                throw new DiscountNotFoundException();

            var overlapExists = await _context.VariableDiscounts
                .AnyAsync(x =>
                        ((request.LowerBound >= x.MinimumAmount && request.LowerBound <= x.MaximumAmount) ||
                         (request.UpperBound >= x.MinimumAmount && request.UpperBound <= x.MaximumAmount) ||
                         (request.LowerBound <= x.MinimumAmount && request.UpperBound >= x.MaximumAmount) ||
                         (request.LowerBound >= x.MinimumAmount && request.UpperBound <= x.MaximumAmount)) ||
                        ((request.CommissionRateLower >= x.MinimumPercentage &&
                          request.CommissionRateLower <= x.MaximumPercentage) ||
                         (request.CommissionRateUpper >= x.MinimumPercentage &&
                          request.CommissionRateUpper <= x.MaximumPercentage) ||
                         (request.CommissionRateLower <= x.MinimumPercentage &&
                          request.CommissionRateUpper >= x.MaximumPercentage) ||
                         (request.CommissionRateLower >= x.MinimumPercentage &&
                          request.CommissionRateUpper <= x.MaximumPercentage)),
                    cancellationToken);


            if (overlapExists)
                return VariableDiscountErrors.Overlap();

            existingDiscount.MinimumAmount = request.LowerBound;
            existingDiscount.MaximumAmount = request.UpperBound;
            existingDiscount.MinimumPercentage = request.CommissionRateLower;
            existingDiscount.MaximumPercentage = request.CommissionRateUpper;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}