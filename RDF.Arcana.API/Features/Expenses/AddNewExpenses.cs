using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Expenses;

[Route("api/Expenses"), ApiController]

public class AddNewExpenses : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewExpenses(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewExpenses")]
    public async Task<IActionResult> Add([FromBody] AddNewExpensesCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
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

    public record AddNewExpensesCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public ICollection<ExpensesCollection> Expenses { get; set; }
        public class ExpensesCollection
        {
            public int OtherExpenseId { get; set; }
            public string Remarks { get; set; }
            public decimal Amount { get; set; }
            public bool IsOneTime { get; set; }
        }
        public int AddedBy { get; set; }
       
    }

    public class AddNewExpensesResult
    {
        public string Requestor { get; set; }
        public string RequestorMobileNumber { get; set; }
        public string Approver { get; set; }
        public string ApproverMobileNumber { get; set; }
    }

    public class Handler : IRequestHandler<AddNewExpensesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewExpensesCommand request, CancellationToken cancellationToken)
        {
            var requestor = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == request.AddedBy, cancellationToken);

            if (requestor == null)
            {
                return UserErrors.NotFound();
            }

            if (!await _context.Clients.AnyAsync(client => client.Id == request.ClientId, cancellationToken))
            {
                return ClientErrors.NotFound();
            }

            decimal total = request.Expenses.Sum(a => a.Amount);

            var approvers = await _context.ApproverByRange
                .Include(usr => usr.User)
                .Where(x => x.ModuleName == Modules.OtherExpensesApproval)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            if (!approvers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }

            // Assign the approvers based on MinValue
            var applicableApprovers = approvers.Where(a => a.MinValue == null || a.MinValue < total).ToList();

            var maxLevelApprover = applicableApprovers.OrderByDescending(a => a.Level).FirstOrDefault();
            if (maxLevelApprover == null)
            {
                maxLevelApprover = approvers.Last();
            }

            var nextLevel = maxLevelApprover.Level + 1;

            if (!applicableApprovers.Any())
            {
                applicableApprovers = approvers.Where(l => l.Level == 1).ToList();
                nextLevel = approvers.Where(l => l.Level == 1).FirstOrDefault()?.Level ?? 1;
            }

            var approverLevels = approvers.Where(a => a.Level <= nextLevel).OrderBy(a => a.Level).ToList();

            // Create a new Request
            var newRequest = new Request(
                Modules.OtherExpensesApproval,
                request.AddedBy,
                approverLevels.First().UserId, // Initially set to the first approver
                approverLevels.Count > 1 ? approverLevels[1].UserId : (int?)null, // Next approver if exists
                Status.UnderReview
            );

            await _context.Requests.AddAsync(newRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Create RequestApprovers for all levels
            for (int i = 0; i < approverLevels.Count; i++)
            {
                var requestApprover = new RequestApprovers
                {
                    ApproverId = approverLevels[i].UserId,
                    RequestId = newRequest.Id,
                    Level = approverLevels[i].Level
                };

                _context.RequestApprovers.Add(requestApprover);

                var notificationForApprover = new Domain.Notification
                {
                    UserId = approverLevels[i].UserId,
                    Status = Status.PendingExpenses
                };

                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            }

            var newExpenses = new Domain.Expenses
            {
                ClientId = request.ClientId,
                RequestId = newRequest.Id,
                Status = Status.UnderReview,
                AddedBy = request.AddedBy,
                Total = total
            };

            await _context.Expenses.AddAsync(newExpenses, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var expense in request.Expenses)
            {
                var newExpensesRequest = new ExpensesRequest
                {
                    ExpensesId = newExpenses.Id,
                    OtherExpenseId = expense.OtherExpenseId,
                    Remarks = expense.Remarks,
                    Amount = expense.Amount,
                    IsOneTime = expense.IsOneTime
                };

                await _context.ExpensesRequests.AddAsync(newExpensesRequest, cancellationToken);
            }

            var notification = new Domain.Notification
            {
                UserId = request.AddedBy,
                Status = Status.PendingExpenses
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = new AddNewExpensesResult
            {
                Requestor = requestor.Fullname,
                RequestorMobileNumber = requestor.MobileNumber,
                Approver = approverLevels.First().User.Fullname,
                ApproverMobileNumber = approverLevels.First().User.MobileNumber
            };

            return Result.Success();
        }
    }

}