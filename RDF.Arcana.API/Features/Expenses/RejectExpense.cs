using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Expenses;

[Route("api/Expenses"), ApiController]

public class RejectExpense : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectExpense(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("RejectExpenseRequest/{id:int}")]
    public async Task<IActionResult> Reject([FromRoute] int id, [FromBody] RejectExpenseCommand command)
    {
        try
        {
            command.RequestId = id;
            
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

    public class RejectExpenseCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public string Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<RejectExpenseCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async  Task<Result> Handle(RejectExpenseCommand request, CancellationToken cancellationToken)
        {
            var existingExpense =
                await _context.Requests
                    .Include(x => x.Expenses)
                    .Include(approval => approval.Approvals)
                    .FirstOrDefaultAsync(
                        x => x.Id == request.RequestId &&
                             x.Status != Status.Rejected,
                        cancellationToken);

            if (existingExpense == null)
            {
                return ExpensesErrors.NotFound();
            }
            
            var approvers = await _context.Approvers
                .Where(module => module.ModuleName == Modules.OtherExpensesApproval)
                .ToListAsync(cancellationToken);
            
            var currentApproverLevel = approvers
                .FirstOrDefault(approver => approver.UserId == existingExpense.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }

            var newApproval = new Approval(
                existingExpense.Id,
                existingExpense.CurrentApproverId,
                Status.Rejected,
                request.Reason,
                true
            );
            
            existingExpense.Expenses.Status = Status.Rejected;
            existingExpense.Status = Status.Rejected;
            
            var notification = new Domain.Notification
            {
                UserId = existingExpense.RequestorId,
                Status = Status.RejectedExpenses
            };
                
            await _context.Notifications.AddAsync(notification, cancellationToken);
                
            var notificationForApprover = new Domain.Notification
            {
                UserId = existingExpense.CurrentApproverId,
                Status = Status.RejectedExpenses
            };
                
            await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

            await _context.Approval.AddAsync(newApproval, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}