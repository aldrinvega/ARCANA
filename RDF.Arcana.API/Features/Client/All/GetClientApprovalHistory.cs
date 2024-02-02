using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using static RDF.Arcana.API.Features.Expenses.GetAllExpenses;

namespace RDF.Arcana.API.Features.Client.All
{
    [Route("api/client")]
    public class GetClientApprovalHistory : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetClientApprovalHistory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/approval-history")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var query = new GetCLientApprovalHistoryQuery
                {
                    Id = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }
                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetCLientApprovalHistoryQuery : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class ClientApprovalHistoryResult
        {
            public IEnumerable<ClientApprovalhistory> ApprovalHistories { get; set; }
            public IEnumerable<UpdateHistory> UpdateHistories { get; set; }
            public IEnumerable<RequestApproversForClient> Approvers { get; set; }
            public class ClientApprovalhistory
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
            public class RequestApproversForClient
            {
                public string Name { get; set; }
                public int Level { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetCLientApprovalHistoryQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetCLientApprovalHistoryQuery request, CancellationToken cancellationToken)
            {
                var clientApproval = await _context
                     .Clients
                     .Include(r => r.Request)
                     .ThenInclude(up => up.UpdateRequestTrails)
                     .Include(r => r.Request)
                     .ThenInclude(x => x.Approvals)
                     .ThenInclude(x => x.Approver)
                     .ThenInclude(x => x.Approver)
                     .Include(x => x.Request)
                     .ThenInclude(x => x.RequestApprovers)
                     .ThenInclude(x => x.Approver)
                     .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if(clientApproval is null)
                {
                    return ClientErrors.NotFound();
                }

                var result = new ClientApprovalHistoryResult
                {
                    ApprovalHistories = clientApproval.Request.Approvals == null
                    ? null
                    : clientApproval.Request.Approvals.OrderByDescending(a => a.CreatedAt)
                        .Select(a => new ClientApprovalHistoryResult.ClientApprovalhistory
                        {
                            Module = a.Request.Module,
                            Approver = a.Approver.Fullname,
                            CreatedAt = a.CreatedAt,
                            Status = a.Status,
                            Level = a.Approver.Approver.FirstOrDefault().Level,
                            Reason = a.Reason

                        }),
                    UpdateHistories = clientApproval.Request.UpdateRequestTrails?.Select(uh => new ClientApprovalHistoryResult.UpdateHistory
                    {
                        Module = uh.ModuleName,
                        UpdatedAt = uh.UpdatedAt
                    }),
                    Approvers = clientApproval.Request.RequestApprovers.Select(x => new ClientApprovalHistoryResult.RequestApproversForClient
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
