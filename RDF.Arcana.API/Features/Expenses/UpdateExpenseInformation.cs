using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Expenses;

[Route("api/Expenses"), ApiController]

public class UpdateExpenseInformation : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateExpenseInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateExpensesInformation/{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateExpenseInformationCommand command, [FromRoute] int id)
    {
        try
        {
            command.ExpenseId = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public record UpdateExpenseInformationCommand : IRequest<Result>
    {
        public int ExpenseId { get; set; }
        public ICollection<ExpensesToUpdate> Expenses { get; set; }

        public class ExpensesToUpdate
        {
            public int Id { get; set; }
            public int OtherExpenseId { get; set; }
            public decimal Amount { get; set; }
        }
        
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
            
           var expenses = await _context.Expenses
                .Include(x => x.ExpensesRequests)
                .Where(lf => lf.Id == request.ExpenseId)
                .Include(x => x.Request)
                .ThenInclude(x => x.Approvals)
                .FirstOrDefaultAsync(cancellationToken);
            
             var approver = await _context.RequestApprovers
                .Where(x => x.RequestId == expenses.RequestId)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);
                
            if (!approver.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }

            if (expenses == null)
            {
                return ExpensesErrors.NotFound();
            }

            var otherExpenses = request.Expenses.Select(x => x.Id).ToList();
            var existingExpenses = expenses.ExpensesRequests.Select(x => x.Id).ToList();
            var toRemove = existingExpenses.Except(otherExpenses);

            foreach (var id in toRemove)
            {
                var forRemove = expenses.ExpensesRequests.First(i => i.Id == id);
                expenses.ExpensesRequests.Remove(forRemove);
            }

            foreach (var expense in request.Expenses)
            {
                var expensesToAdd = expenses.ExpensesRequests.FirstOrDefault(x => x.Id == expense.Id);

                if (expensesToAdd != null)
                {
                    expensesToAdd.Amount = expense.Amount;
                    expensesToAdd.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    expenses.ExpensesRequests.Add(new ExpensesRequest
                    {
                        ExpensesId = request.ExpenseId,
                        Amount = expense.Amount,
                        OtherExpenseId = expense.OtherExpenseId
                    });
                }
            }
            expenses.Request.Status = Status.UnderReview;
            expenses.Request.CurrentApproverId = approver.First().ApproverId;
            expenses.Status = Status.UnderReview;
            
            foreach (var approval in expenses.Request.Approvals)
            {
                approval.IsActive = false;
            }
            
            var newUpdateHistory = new UpdateRequestTrail(
                expenses.RequestId,
                Modules.RegistrationApproval,
                DateTime.Now,
                expenses.AddedBy);
            
            await _context.UpdateRequestTrails.AddAsync(newUpdateHistory, cancellationToken);
            
            var notification = new Domain.Notification
            {
                UserId = expenses.AddedBy,
                Status = Status.PendingExpenses
            };
                
            await _context.Notifications.AddAsync(notification, cancellationToken);
            
            var notificationForApprover = new Domain.Notification
            {
                UserId = approver.First().ApproverId,
                Status = Status.PendingExpenses
            };
                
            await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}