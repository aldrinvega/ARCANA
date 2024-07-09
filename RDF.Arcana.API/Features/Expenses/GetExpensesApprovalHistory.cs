using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using static RDF.Arcana.API.Features.Client.All.GetClientApprovalHistory;
using static RDF.Arcana.API.Features.Expenses.GetExpensesApprovalHistory;

namespace RDF.Arcana.API.Features.Expenses
{
    [Route("api/other-expenses")]
    public class GetExpensesApprovalHistory : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetExpensesApprovalHistory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/approval-history")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var query = new GetExpensesApprvalHistoryQuery
                {
                    Id = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetExpensesApprvalHistoryQuery : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class OtherExpensesApprovalHistory
        {
            public IEnumerable<ExpensesApprovalHistory> ApprovalHistories { get; set; }
            public IEnumerable<UpdateHistory> UpdateHistories { get; set; }
            public IEnumerable<RequestApproversForExpenses> Approvers { get; set; }
            public class ExpensesApprovalHistory
            {
                public string Module { get; set; }
                public string Approver { get; set; }
                public DateTime CreatedAt { get; set; }
                public string Status { get; set; }
                public int? Level { get; set; }
                public string Reason { get; set; }
            }
            public class UpdateHistory
            {
                public string Module { get; set; }
                public DateTime UpdatedAt { get; set; }
            }
            public class RequestApproversForExpenses
            {
                public string Name { get; set; }
                public int Level { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetExpensesApprvalHistoryQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetExpensesApprvalHistoryQuery request, CancellationToken cancellationToken)
            {
                var otherExpensesApproval = await _context
                    .Expenses
                    .Include(r => r.Request)
                    .ThenInclude(up => up.UpdateRequestTrails)
                    .Include(r => r.Request)
                    .ThenInclude(x => x.Approvals)
                    .ThenInclude(x => x.Approver)
                    .Include(r => r.Request)
                    .ThenInclude(x => x.RequestApprovers)
                    .ThenInclude(x => x.Approver)
                    .FirstOrDefaultAsync(x => x.RequestId == request.Id, cancellationToken);

                if (otherExpensesApproval is null)
                {
                    return ExpensesErrors.NotFound();
                }

                var approvalHistories = otherExpensesApproval.Request.Approvals == null
                    ? null
                    : otherExpensesApproval.Request.Approvals
                        .OrderByDescending(a => a.CreatedAt)
                        .Select(a => new OtherExpensesApprovalHistory.ExpensesApprovalHistory
                        {
                            Module = a.Request.Module,
                            Approver = a.Approver.Fullname,
                            CreatedAt = a.CreatedAt,
                            Status = a.Status,
                            Level = otherExpensesApproval.Request.RequestApprovers.FirstOrDefault(ra => ra.ApproverId == a.ApproverId)?.Level,
                            Reason = a.Reason
                        });

                var result = new OtherExpensesApprovalHistory
                {
                    ApprovalHistories = approvalHistories,
                    UpdateHistories = otherExpensesApproval.Request.UpdateRequestTrails?.Select(uh => new OtherExpensesApprovalHistory.UpdateHistory
                    {
                        Module = uh.ModuleName,
                        UpdatedAt = uh.UpdatedAt
                    }),
                    Approvers = otherExpensesApproval.Request.RequestApprovers.Select(x => new OtherExpensesApprovalHistory.RequestApproversForExpenses
                    {
                        Name = x.Approver.Fullname,
                        Level = x.Level
                    })
                };

                return Result.Success(result);
            }
        }
    }
}
