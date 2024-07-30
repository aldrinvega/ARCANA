
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

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
    public async Task<IActionResult> GetApproverByRangeAsync([FromQuery] GetApproverByRangeQuery query)
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

    public class GetAppproverByModuleByRangeResult
    {
        public ICollection<Approver> Approvers { get; set; }

        public class Approver
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string ModuleName { get; set; }
            public decimal? MinValue { get; set; }
            public bool IsActive { get; set; }
            public int Level { get; set; }
        }
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


            var approvers = existingApprovers.Select(a => new GetAppproverByModuleByRangeResult.Approver
            {
                Id = a.Id,
                UserId = a.UserId,
                FullName = a.User.Fullname,
                ModuleName = a.ModuleName,
                MinValue = a.MinValue,
                Level = a.Level,
                IsActive = a.IsActive

            }).ToList();

            var result = new GetAppproverByModuleByRangeResult
            {
                Approvers = approvers
            };
            return Result.Success(result);
        }
    }

}
