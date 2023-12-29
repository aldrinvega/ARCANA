using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Expenses;
[Route("api/Expenses"), ApiController]

public class ApproveExpense : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveExpense(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ApproveExpense{id:int}")]
    public async Task<IActionResult> Approve([FromRoute] int id)
    {
        try
        {
            var command = new ApproveExpenseCommand
            {
                RequestId = id
            };
            
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.ApprovedBy = userId;
            }

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

    public class ApproveExpenseCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class Handler : IRequestHandler<ApproveExpenseCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ApproveExpenseCommand request, CancellationToken cancellationToken)
        {
            var expenses = await _context.Requests
                .Include(e => e.Expenses)
                .Where(lf => lf.Id == request.RequestId)
                .FirstOrDefaultAsync(cancellationToken);

            if (expenses is null)
            {
                return ExpensesErrors.NotFound();
            }

            var approvers = await _context.RequestApprovers
                .Where(module => module.RequestId == request.RequestId)
                .ToListAsync(cancellationToken);
            var currentApproverLevel = approvers
                .FirstOrDefault(approver => 
                    approver.ApproverId == expenses.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }
            
            var newApproval = new Approval(
                expenses.Id,
                expenses.CurrentApproverId,
                Status.Approved,
                null,
                true
            );
            
            var nextLevel = currentApproverLevel.Value + 1;
            var nextApprover = approvers
                .FirstOrDefault(approver => approver.Level == nextLevel);
            
            if (nextApprover == null)
            {
                expenses.Status = Status.Approved;
                expenses.Expenses.Status = Status.Approved;
            }
            else
            {
                expenses.CurrentApproverId = nextApprover.ApproverId;
            }
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}