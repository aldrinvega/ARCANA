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
        public int OtherExpensesId { get; set; }
        public decimal Amount { get; set; }
        public int AddedBy { get; set; }
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
            var approvers = await _context.Approvers
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
                OtherExpensesId = request.OtherExpensesId,
                Amount = request.Amount,
                RequestId = newRequest.Id,
                Status = Status.UnderReview,
                AddedBy = request.AddedBy
            };

            await _context.Expenses.AddAsync(newExpenses, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}