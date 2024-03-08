using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Special_Discount;

[Route("api/special-discount"), ApiController]

public class CancelSpecialDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public CancelSpecialDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        try
        {
            var command = new CancelSpecialDiscountCommand
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

    public class CancelSpecialDiscountCommand : IRequest<Result>
    {
        public int SpecialDiscountId { get; set; }
    }

    public class Handler : IRequestHandler<CancelSpecialDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CancelSpecialDiscountCommand request, CancellationToken cancellationToken)
        {
            var pendingSpecialDiscount = await _context.SpecialDiscounts.FirstOrDefaultAsync(sp => sp.Id == request.SpecialDiscountId && sp.Status == Status.UnderReview);

            if (pendingSpecialDiscount != null)
            {
                _context.SpecialDiscounts.Remove(pendingSpecialDiscount);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }

            return SpecialDiscountErrors.NotFound();
        }
    }
}
