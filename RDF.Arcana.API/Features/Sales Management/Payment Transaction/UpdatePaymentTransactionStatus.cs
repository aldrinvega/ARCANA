

using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using System.Security.Claims;
using static RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment.UpdateAdvancePaymentStatus;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction
{
    [Route("api/payment-transaction"), ApiController]
    public class UpdatePaymentTransactionStatus : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdatePaymentTransactionStatus(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("status/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var command = new UpdatePaymentTransactionStatusCommand
                {
                    ClientId = id
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

        public class UpdatePaymentTransactionStatusCommand : IRequest<Result>
        {
            public int ClientId { get; set; }
            public int ModifiedBy { get; set; }
        }

        public class Handler : IRequestHandler<UpdatePaymentTransactionStatusCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdatePaymentTransactionStatusCommand request, CancellationToken cancellationToken)
            {
                var existingPaymentTransaction =
                    await _context.Transactions.FirstOrDefaultAsync(tr => tr.ClientId == request.ClientId);

                if (existingPaymentTransaction is null)
                {
                    return PaymentTransactionsErrors.NotFound();
                }

                existingPaymentTransaction.IsActive = !existingPaymentTransaction.IsActive;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
