
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment.UpdateAdvancePaymentStatus;
using System.Security.Claims;


namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    [Route("api/clearing-payment")]
    public class UpdateClearingTransaction : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateClearingTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("status/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var command = new UpdateClearingTransactionCommand
                {
                    PaymentRecordId = id
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

        public class UpdateClearingTransactionCommand : IRequest<Result> 
        {
            public int PaymentRecordId { get; set; }
            public int ModifiedBy{ get; set; }
        }

        public class Handler : IRequestHandler<UpdateClearingTransactionCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateClearingTransactionCommand request, CancellationToken cancellationToken)
            {
                var existingTransaction = await _context.ClearedPayments
                    .FirstOrDefaultAsync(tr =>
                        tr.Id == request.PaymentRecordId,
                        cancellationToken);

                if (existingTransaction is null)
                {
                    return ClearingErrors.NotFound();
                }

                existingTransaction.IsActive = !existingTransaction.IsActive;
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
