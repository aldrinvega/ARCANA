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
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal MinimumPercentage { get; set; }
        public decimal MaximumPercentage { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewVariableDiscountCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewVariableDiscountCommand request, CancellationToken cancellationToken)
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
                         (commissionRateLower >= x.MinimumPercentage && commissionRateUpper <= x.MaximumPercentage)),
                    cancellationToken);

            if (overlapExists)
            {
                throw new DiscountOverlapsToTheExistingOneException();
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

            return Unit.Value;
        }
    }
}