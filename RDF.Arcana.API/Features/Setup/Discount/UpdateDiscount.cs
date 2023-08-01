using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]

public class UpdateDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateDiscountCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDiscountCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
        {
            var existingDiscount = await _context.Discounts.FindAsync(request.Id);

            if (existingDiscount == null)
                throw new DiscountNotFoundException();

            var overlapExists = await _context.Discounts
                .Where(x => x.Id != request.Id)
                .AnyAsync(x => (x.LowerBound <= request.LowerBound && x.UpperBound >= request.LowerBound) ||
                               (x.LowerBound <= request.UpperBound && x.UpperBound >= request.UpperBound) ||
                               (x.LowerBound >= request.LowerBound && x.UpperBound <= request.UpperBound),
                    cancellationToken);

            if (overlapExists)
                throw new DiscountOverlapsToTheExistingOneException();
            
            if(
                existingDiscount.LowerBound == request.LowerBound &&
                existingDiscount.UpperBound == request.UpperBound &&
                existingDiscount.CommissionRateLower == request.CommissionRateLower &&
                existingDiscount.CommissionRateUpper == request.CommissionRateUpper &&
                existingDiscount.CommissionRateUpper == request.CommissionRateUpper)
            {
                throw new System.Exception("No changes");
            }

            existingDiscount.LowerBound = request.LowerBound;
            existingDiscount.UpperBound = request.UpperBound;
            existingDiscount.CommissionRateLower = request.CommissionRateLower;
            existingDiscount.CommissionRateUpper = request.CommissionRateUpper;
            existingDiscount.ModifiedBy = request.ModifiedBy ?? "Admin";
            existingDiscount.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateDiscount/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] UpdateDiscountCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.Id = id;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Discount has been successfully updated");
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}