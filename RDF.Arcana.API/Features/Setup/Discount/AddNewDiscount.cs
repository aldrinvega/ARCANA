using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]
public class AddNewDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewDiscount")]
    public async Task<IActionResult> Add(AddNewDiscount.AddNewDiscountCommand command)
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

    public class AddNewDiscountCommand : IRequest<Unit>
    {
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewDiscountCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewDiscountCommand request, CancellationToken cancellationToken)
        {
            var overlapExists = await _context.Discounts
                .AnyAsync(x => x.LowerBound <= request.LowerBound && x.UpperBound >= request.LowerBound ||
                               x.LowerBound <= request.UpperBound && x.UpperBound >= request.UpperBound ||
                               x.LowerBound >= request.LowerBound && x.UpperBound <= request.UpperBound,
                    cancellationToken);

            if (overlapExists)
            {
                throw new DiscountOverlapsToTheExistingOneException();
            }

            var discount = new Domain.Discount
            {
                LowerBound = request.LowerBound,
                UpperBound = request.UpperBound,
                CommissionRateLower = request.CommissionRateLower / 100,
                CommissionRateUpper = request.CommissionRateUpper / 100,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.Discounts.AddAsync(discount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}