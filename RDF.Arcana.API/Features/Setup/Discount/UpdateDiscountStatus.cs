using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Discount.Exception;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]

public class UpdateDiscountStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDiscountStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateDiscountStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateDiscountStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDiscountStatusCommand request, CancellationToken cancellationToken)
        {
            var existingDiscount =
                await _context.Discounts.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (existingDiscount is null)
            {
                throw new DiscountNotFoundException();
            }

            existingDiscount.IsActive = request.Status;
            existingDiscount.UpdateAt = DateTime.Now;
            existingDiscount.ModifiedBy = request.ModifiedBy ?? "Admin";

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateDiscountStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateDiscountStatusCommand
            {
                Id = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Messages.Add("Discount status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
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