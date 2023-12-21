using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount"), ApiController]

public class AddNewDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewDiscount")]
    public async Task<IActionResult> Add([FromBody] AddNewDiscountCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class AddNewDiscountCommand : IRequest<Result>
    {
        public string DiscountType { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewDiscountCommand request, CancellationToken cancellationToken)
        {
            var validateDiscountType = await _context.Discounts.FirstOrDefaultAsync(dt => dt.DiscountType == request.DiscountType,
                    cancellationToken);

            if (validateDiscountType is not null)
            {
                return DiscountErrors.AlreadyExist(request.DiscountType);
            }

            var discount = new Domain.Discount
            {
                DiscountType = request.DiscountType,
                AddedBy = request.AddedBy
            };

            await _context.Discounts.AddAsync(discount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}