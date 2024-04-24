
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using static RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment.AddAdvancePayment;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Sales_Management.Clearing_Transaction
{
    [Microsoft.AspNetCore.Mvc.Route("api/clearing-transation")]
    public class AddClearingTransaction : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddClearingTransaction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("cleared")]
        public async Task<IActionResult> Add([FromBody] AddClearingTransactionCommand command)
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public class AddClearingTransactionCommand : IRequest<Result>
        {
            public int PaymentRecordId { get; set; }
            public int AddedBy { get; set; }
            public int? ModifiedBy { get; set; }

        }

        public class Handler : IRequestHandler<AddClearingTransactionCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddClearingTransactionCommand request, CancellationToken cancellationToken)
            {
                //var existingTransaction = await _context.PaymentRecords
                //    .FirstOrDefaultAsync(tr =>
                //        tr.Id == request.PaymentRecordId,
                //        cancellationToken);

                //if (existingTransaction != null)
                //{
                //    return ClearingErrors.NotFound();
                //}



                var clearedPayment = new ClearedPayments
                {
                    PaymentRecordId = request.PaymentRecordId,
                    AddedBy = request.AddedBy,
                    ModifiedBy = request.ModifiedBy,
                    Status = Status.Cleared
                };

                var statusCleared = await _context.PaymentRecords
                        .FirstOrDefaultAsync(tr =>
                            tr.Id == request.PaymentRecordId,
                            cancellationToken);

                statusCleared.Status = Status.Cleared;

                await _context.ClearedPayments.AddAsync(clearedPayment, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
