using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount"), ApiController]

public class UpdateDiscountStatus : ControllerBase
{

    private readonly IMediator _mediator;

    public UpdateDiscountStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateDiscountStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus(int id)
    {
        try
        {
            var command = new UpdateDiscountStatusCommand
            {
                DiscountId = id
            };
            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public class UpdateDiscountStatusCommand : IRequest<Result>
    {
        public int DiscountId { get; set; }
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
            var validateDiscount = await _context.Discounts.FirstOrDefaultAsync(dc => 
                dc.Id == request.DiscountId,
                cancellationToken);

            if (validateDiscount is null)
            {
                return DiscountErrors.NotFound();
            }

            validateDiscount.IsActive = !validateDiscount.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}