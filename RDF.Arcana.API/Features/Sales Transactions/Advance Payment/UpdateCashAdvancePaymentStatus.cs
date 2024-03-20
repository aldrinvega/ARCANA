using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment.AdvancePaymentErrors;

namespace RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;

public class UpdateCashAdvancePaymentStatus
{
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
                    return NotFound();
                }

                existingAdvancePayment.IsActive = !existingAdvancePayment.IsActive;

                return Result.Success();
        };
    }
}