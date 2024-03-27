using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;
[Route("api/advance-payment")]

public class UpdateAdvancePaymentStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateAdvancePaymentStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("status/{id}")]
    public async Task<IActionResult> Update(int id)
    {
        try
        {
            var command = new UpdateCashAdvancePaymentStatusCommand
            {
                AdvancePaymentId = id
            };
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.ModifiedBy = userId;
            }
            

            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateCashAdvancePaymentStatusCommand : IRequest<Result>
    {
        public int AdvancePaymentId { get; set; }
        public int ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateCashAdvancePaymentStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateCashAdvancePaymentStatusCommand request, CancellationToken cancellationToken)
        {
                var existingAdvancePayment =
                    await _context.AdvancePayments.FirstOrDefaultAsync(cap => cap.Id == request.AdvancePaymentId, cancellationToken);

                if (existingAdvancePayment is null)
                {
                    return AdvancePaymentErrors.NotFound();
                }

                existingAdvancePayment.IsActive = !existingAdvancePayment.IsActive;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
        }
    }
}