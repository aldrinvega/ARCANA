using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Requests_Approval;
[Route("api/Approver")]

public class GetApproversPerModule : ControllerBase
{
    private readonly IMediator _mediator;

    public GetApproversPerModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetApproversPerModules")]
    public async Task<IActionResult> GetApprovers()
    {
        try
        {
            var query = new GetApproverPerModuleQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
       
    }

    public class GetApproverPerModuleQuery : IRequest<Result>{}

    public class GetApproversPerModuleResult
    {
        public string ModuleName { get; set; }
        public IEnumerable<Approver> Approvers { get; set; }

        public class Approver
        {
            public int UserId { get; set; }
            public string Fullname { get; set; }
            public string Role { get; set; }
            public int Level { get; set; }
        }
    }
    
    public class Handler : IRequestHandler<GetApproverPerModuleQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetApproverPerModuleQuery request, CancellationToken cancellationToken)
        {
            var approverGroups = await _context.Approvers
                .Include(approver => approver.User)
                .ThenInclude(user => user.UserRoles)
                .GroupBy(approver => new
                {
                    approver.ModuleName
                })
                .ToListAsync(cancellationToken);

            var result = approverGroups.Select(approverGroup => new GetApproversPerModuleResult
            {
                ModuleName = approverGroup.Key.ModuleName,
                Approvers = approverGroup
                    .OrderBy(c => c.Level)
                    .Select(approver => new GetApproversPerModuleResult.Approver
                    {
                    UserId = approver.User.Id,
                    Fullname = approver.User.Fullname,
                    Role = approver.User.UserRoles.UserRoleName,
                    Level = approver.Level
                }).ToList()
            }).ToList();

            return Result.Success(result);
        }
    }
}