using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Expenses;

public class UpdateExpenseInformation
{
    public record UpdateExpenseInformationCommand : IRequest<Result>
    {
        public int ExpenseId { get; set; }
        public decimal Amount { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateExpenseInformationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateExpenseInformationCommand request, CancellationToken cancellationToken)
        {
            var existingExpenses = await _context.Expenses
                .Include(expenses => expenses.Request)
                .FirstOrDefaultAsync(e => e.Id == 
                                          request.ExpenseId, 
                                          cancellationToken);

            if (existingExpenses is null)
            {
                return ExpensesErrors.NotFound();
            }
            
            var approver = await _context.Approvers
                .Where(x => x.ModuleName == Modules.OtherExpensesApproval && x.Level == 1)
                .FirstOrDefaultAsync(cancellationToken);

            if (approver is null)
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }

            existingExpenses.Request.Status = Status.UnderReview;
            existingExpenses.Request.CurrentApproverId = approver.UserId;
            existingExpenses.Status = Status.UnderReview;
            existingExpenses.Amount = request.Amount;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}