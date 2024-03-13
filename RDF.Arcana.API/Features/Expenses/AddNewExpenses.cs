using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

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

            var approvers = await _context.Approvers
                .Include(usr => usr.User)
                .Where(x => x.ModuleName == Modules.OtherExpensesApproval)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);
                
            if (!approvers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
            }
            
            var newRequest = new Request(
                Modules.OtherExpensesApproval,
                request.AddedBy,
                approvers.First().UserId,
                approvers.FirstOrDefault(x => x.Level == 2)?.UserId,
                Status.UnderReview
            );

            await _context.Requests.AddAsync(newRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
                     {
                         ApproverId = approver.UserId,
                         RequestId = newRequest.Id,
                         Level = approver.Level,
                     }))
            {
                _context.RequestApprovers.Add(newRequestApprover);
            }

            var newExpenses = new Domain.Expenses
            {
                ClientId = request.ClientId,
                RequestId = newRequest.Id,
                Status = Status.UnderReview,
                AddedBy = request.AddedBy
            };
                
            await _context.Expenses.AddAsync(newExpenses, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            

            foreach (var expenses in request.Expenses)
            {
                var newExpensesRequest = new ExpensesRequest
                {
                    ExpensesId = newExpenses.Id,
                    OtherExpenseId = expenses.OtherExpenseId,
                    Remarks = expenses.Remarks,
                    Amount = expenses.Amount
                };

                await _context.ExpensesRequests.AddAsync(newExpensesRequest, cancellationToken);
            }
            
            var notification = new Domain.Notification
            {
                UserId = request.AddedBy,
                Status = Status.PendingExpenses
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);
                
            var notificationForApprover = new Domain.Notification
            {
                UserId = approvers.First().UserId,
                Status = Status.PendingExpenses
            };
            
            await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = new AddNewExpensesResult
            {
                Requestor = requestor.Fullname,
                RequestorMobileNumber = requestor.MobileNumber,
                Approver = approvers.First().User.Fullname,
                ApproverMobileNumber = approvers.First().User.MobileNumber
            };
            return Result.Success(result);
        }
    }
}