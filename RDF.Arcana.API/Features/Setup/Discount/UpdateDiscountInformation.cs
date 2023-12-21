using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount"), ApiController]

public class UpdateDiscountInformation : ControllerBase
{

    private readonly IMediator _mediator;

    public UpdateDiscountInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateDiscountInformation/{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateDiscountInformationCommand command, [FromRoute] int id)
    {
        try
        {
            
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.ModifiedBy = userId;
            }

            command.DiscountId = id;
            var result = await _mediator.Send(command);
            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateDiscountInformationCommand : IRequest<Result>
    {
        public int DiscountId { get; set; }
        public string DiscountType { get; set; }
        public int ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateDiscountInformationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateDiscountInformationCommand request, CancellationToken cancellationToken)
        {
            var validateDiscount = await _context.Discounts.FirstOrDefaultAsync(dc => dc.Id == request.DiscountId, cancellationToken);

            if (validateDiscount is null)
            {
                return DiscountErrors.NotFound();
            }

            validateDiscount.DiscountType = request.DiscountType;
            validateDiscount.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}