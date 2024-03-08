using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Special_Discount;

[Route("api/special-discount"), ApiController]

public class VoidSpecialDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public VoidSpecialDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("void/{id}")]
    public async Task<IActionResult> Void([FromRoute]int id)
    {
        try
        {
            var command = new VoidSpecialDiscountCommand
            {
                SpecialDiscountId = id
            };

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class VoidSpecialDiscountCommand : IRequest<Result>
    {
        public int SpecialDiscountId { get; set; }
    }

    public class Handler : IRequestHandler<VoidSpecialDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(VoidSpecialDiscountCommand request, CancellationToken cancellationToken)
        {
            var specialDiscount = await _context.SpecialDiscounts
                .Include(rq => rq.Request)
                .FirstOrDefaultAsync(sp => sp.Id == request.SpecialDiscountId, cancellationToken);

            if (specialDiscount == null)
            {
                return SpecialDiscountErrors.NotFound();
            }

            if(specialDiscount.Status == Status.Voided)
            {
                return SpecialDiscountErrors.AlreadyRejected();
            }

            specialDiscount.Request.Status = Status.Voided;
            specialDiscount.Status = Status.Voided;
            specialDiscount.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
