
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using static RDF.Arcana.API.Features.Requests_Approval.GetApproverByModule;

namespace RDF.Arcana.API.Features.Requests_Approval;

[Route("api/Approver")]
public class GetApproverByRange : ControllerBase
{
    private readonly IMediator _mediator;

    public GetApproverByRange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetApproverByRange")]
    public async Task<IActionResult> GetApprover([FromQuery] GetApproverByRangeQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
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
    public class GetApproverByRangeQuery : IRequest<Result>
    {
        public string ModuleName { get; set; }

    }

    public class GetAppproverByModuleResult
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ModuleName { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetApproverByRangeQuery, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetApproverByRangeQuery request, CancellationToken cancellationToken)
        {
            var existingApprovers = await _context.ApproverByRange
                .Include(u => u.User)
                .Where(m => m.ModuleName == request.ModuleName)
                .ToListAsync(cancellationToken);

            if (!existingApprovers.Any())
            {
                return ApprovalErrors.NoApproversFound(request.ModuleName);
            }

            var result = existingApprovers.Select(approver => new GetAppproverByModuleResult
            {
                Id = approver.Id,
                UserId = approver.UserId,
                ModuleName = approver.ModuleName,
                MinValue = approver.MinValue,
                MaxValue = approver.MaxValue,
                IsActive = approver.IsActive
            }).ToList();

            return Result.Success(result);
        }
    }

}
