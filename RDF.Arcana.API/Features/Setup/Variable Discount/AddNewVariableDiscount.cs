using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Variable_Discount;

[Route("api/VariableDiscount")]
[ApiController]
public class AddNewVariableDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewVariableDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewVariableDiscount")]
    public async Task<IActionResult> Add(AddNewVariableDiscountCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

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

    public class AddNewVariableDiscountCommand : IRequest<Result>
    {
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal MinimumPercentage { get; set; }
        public decimal MaximumPercentage { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewVariableDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewVariableDiscountCommand request, CancellationToken cancellationToken)
        {
            var commissionRateLower = request.MinimumPercentage / 100;
            var commissionRateUpper = request.MaximumPercentage / 100;
            var overlapExists = await _context.VariableDiscounts
                .AnyAsync(x =>
                        ((request.MinimumAmount >= x.MinimumAmount && request.MinimumAmount <= x.MaximumAmount) ||
                         (request.MaximumAmount >= x.MinimumAmount && request.MaximumAmount <= x.MaximumAmount) ||
                         (request.MinimumAmount <= x.MinimumAmount && request.MaximumAmount >= x.MaximumAmount) ||
                         (request.MinimumAmount >= x.MinimumAmount && request.MaximumAmount <= x.MaximumAmount)) ||
                        ((commissionRateLower >= x.MinimumPercentage && commissionRateLower <= x.MaximumPercentage) ||
                         (commissionRateUpper >= x.MinimumPercentage && commissionRateUpper <= x.MaximumPercentage) ||
                         (commissionRateLower <= x.MinimumPercentage && commissionRateUpper >= x.MaximumPercentage) ||
                         (commissionRateLower >= x.MinimumPercentage && commissionRateUpper <= x.MaximumPercentage)) && 
                        x.IsActive,
                    cancellationToken);

            if (overlapExists)
            {
                return VariableDiscountErrors.Overlap();
            }

            var discount = new Domain.VariableDiscounts
            {
                MinimumAmount = request.MinimumAmount,
                MaximumAmount = request.MaximumAmount,
                MinimumPercentage = commissionRateLower,
                MaximumPercentage = commissionRateUpper,
                IsSubjectToApproval = false
            };

            await _context.VariableDiscounts.AddAsync(discount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}