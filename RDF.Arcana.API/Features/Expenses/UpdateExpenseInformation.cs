using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
        public decimal Total { get; set; }
        public ICollection<ExpensesToUpdate> Expenses { get; set; }

        public class ExpensesToUpdate
        {
            public int Id { get; set; }
            public int OtherExpenseId { get; set; }
            public decimal Amount { get; set; }
            public bool IsOneTime { get; set; }
            public string Remarks { get; set; }
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
                .ThenInclude(x => x.UpdateRequestTrails)
                .FirstOrDefaultAsync(cancellationToken);

            if (expenses == null)
            {
                return ExpensesErrors.NotFound();
            }

            decimal total = request.Expenses.Sum(a => a.Amount);

            var expenseItems = request.Expenses.Select(x => x.Id).ToList();
            var existingExpenses = expenses.ExpensesRequests.Select(x => x.Id).ToList();
            var toRemove = existingExpenses.Except(expenseItems);

            foreach (var id in toRemove)
            {
                var forRemove = expenses.ExpensesRequests.First(i => i.Id == id);
                expenses.ExpensesRequests.Remove(forRemove);
            }

            if (!expenses.ExpensesRequests.Any())
            {
                _context.UpdateRequestTrails.RemoveRange(expenses.Request.UpdateRequestTrails);
                _context.Expenses.Remove(expenses);
                _context.Approval.RemoveRange(expenses.Request.Approvals);
                _context.Requests.Remove(expenses.Request);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }

            foreach (var expense in request.Expenses)
            {
                var expensesToAdd = expenses.ExpensesRequests.FirstOrDefault(x => x.Id == expense.Id);
                if (expensesToAdd != null)
                {
                    expensesToAdd.Amount = expense.Amount;
                    expensesToAdd.IsOneTime = expense.IsOneTime;
                    expensesToAdd.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    expenses.ExpensesRequests.Add(new ExpensesRequest
                    {
                        ExpensesId = request.ExpenseId,
                        Amount = expense.Amount,
                        IsOneTime = expense.IsOneTime,
                        Remarks = expense.Remarks,
                        OtherExpenseId = expense.OtherExpenseId
                    });
                }
            }

            expenses.Request.Status = Status.UnderReview;
            expenses.Total = total;

            var applicableApprovers = await _context.ApproverByRange
                .Where(ar => ar.ModuleName == Modules.OtherExpensesApproval && ar.IsActive && Math.Ceiling(expenses.Total) >= ar.MinValue)
                .OrderBy(ar => ar.Level)
                .ToListAsync(cancellationToken);

            if (!applicableApprovers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }

            var maxLevelApprover = applicableApprovers.OrderByDescending(a => a.Level).First();
            var approverLevels = applicableApprovers
                .Where(a => a.Level <= maxLevelApprover.Level)
                .OrderBy(a => a.Level).ToList();

            var existingRequestApprovers = await _context.RequestApprovers
                .Where(x => x.RequestId == expenses.RequestId)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            foreach (var requestApprover in existingRequestApprovers)
            {
                _context.RequestApprovers.Remove(requestApprover);
            }

            var newRequestApprovers = new List<RequestApprovers>();
            foreach (var approver in approverLevels)
            {
                var requestApprover = new RequestApprovers
                {
                    ApproverId = approver.UserId,
                    RequestId = expenses.RequestId,
                    Level = approver.Level
                };

                newRequestApprovers.Add(requestApprover);

                var notificationForApprover = new Domain.Notification
                {
                    UserId = approver.UserId,
                    Status = Status.PendingExpenses
                };

                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            }

            _context.RequestApprovers.AddRange(newRequestApprovers);

            expenses.Request.Status = Status.UnderReview;
            expenses.Request.CurrentApproverId = approverLevels.First().UserId;

            var newUpdateHistory = new UpdateRequestTrail(
                expenses.RequestId,
                Modules.OtherExpensesApproval,
                DateTime.Now,
                expenses.AddedBy);

            await _context.UpdateRequestTrails.AddAsync(newUpdateHistory, cancellationToken);

            var notification = new Domain.Notification
            {
                UserId = expenses.AddedBy,
                Status = Status.PendingExpenses
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            //var result = new
            //{
            //    approvalHistories = newRequestApprovers.Select(a => new
            //    {
            //        name = a.ApproverId,
            //        level = a.Level
            //    }).ToList(),
            //    updateHistories = expenses.Request.UpdateRequestTrails.Select(trail => new
            //    {
            //        module = trail.ModuleName,
            //        updatedAt = trail.UpdatedAt
            //    }).ToList(),
            //    approvers = newRequestApprovers.Select(a => new
            //    {
            //        name = a.ApproverId,
            //        level = a.Level
            //    }).ToList()
            //};

            return Result.Success(/*result*/);
        }
    }
}
