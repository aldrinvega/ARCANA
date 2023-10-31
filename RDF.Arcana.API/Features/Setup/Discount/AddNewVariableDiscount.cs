using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
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
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Discount has been added successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class AddNewVariableDiscountCommand : IRequest<Unit>
    {
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewVariableDiscountCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewVariableDiscountCommand request, CancellationToken cancellationToken)
        {
            var commissionRateLower = request.CommissionRateLower / 100;
            var commissionRateUpper = request.CommissionRateUpper / 100;
            var overlapExists = await _context.VariableDiscounts
                .AnyAsync(x =>
                        ((request.LowerBound >= x.MinimumAmount && request.LowerBound <= x.MaximumAmount) ||
                         (request.UpperBound >= x.MinimumAmount && request.UpperBound <= x.MaximumAmount) ||
                         (request.LowerBound <= x.MinimumAmount && request.UpperBound >= x.MaximumAmount) ||
                         (request.LowerBound >= x.MinimumAmount && request.UpperBound <= x.MaximumAmount)) ||
                        ((commissionRateLower >= x.MinimumPercentage && commissionRateLower <= x.MaximumPercentage) ||
                         (commissionRateUpper >= x.MinimumPercentage && commissionRateUpper <= x.MaximumPercentage) ||
                         (commissionRateLower <= x.MinimumPercentage && commissionRateUpper >= x.MaximumPercentage) ||
                         (commissionRateLower >= x.MinimumPercentage && commissionRateUpper <= x.MaximumPercentage)),
                    cancellationToken);

            if (overlapExists)
            {
                throw new DiscountOverlapsToTheExistingOneException();
            }

            var discount = new Domain.VariableDiscounts
            {
                MinimumAmount = request.LowerBound,
                MaximumAmount = request.UpperBound,
                MinimumPercentage = commissionRateLower,
                MaximumPercentage = commissionRateUpper,
                IsSubjectToApproval = false
            };

            await _context.VariableDiscounts.AddAsync(discount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}