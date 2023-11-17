using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Requests_Approval;

[Route("api/Approver")]

public class GetApproverByModule : ControllerBase
{
    private readonly IMediator _mediator;

    public GetApproverByModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetApproverByModule")]
    public async Task<IActionResult> GetApprover([FromQuery] GetApproverByModuleQuery query)
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

    public record GetApproverByModuleQuery : IRequest<Result<GetApproverByModuleResult>>
    {
        public string ModuleName { get; set; }
    }

    public record GetApproverByModuleResult
    {
        public string ModuleName { get; set; }
        public ICollection<Approver> Approvers { get; set; }

        public class Approver
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Fullname { get; set; }
            public int Level { get; set; }
        }
    }
    
    public class Handler : IRequestHandler<GetApproverByModuleQuery, Result<GetApproverByModuleResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetApproverByModuleResult>> Handle(GetApproverByModuleQuery request, CancellationToken cancellationToken)
        {
            var existingApprovers = await _context.Approvers
                .Include(user => user.User)
                .Where(module => module.ModuleName == request.ModuleName).ToListAsync(cancellationToken);

            if (!existingApprovers.Any())
            {
                return Result<GetApproverByModuleResult>.Failure(
                    ApprovalErrors.NoApproversFound(request.ModuleName));
            }

            var approvers = existingApprovers.Select(approver => new GetApproverByModuleResult.Approver
            {
                Id = approver.Id,
                UserId = approver.UserId,
                Fullname = approver.User.Fullname,
                Level = approver.Level
            }).ToList();

            var result = new GetApproverByModuleResult
            {
                ModuleName = request.ModuleName,
                Approvers = approvers
            };

            return Result<GetApproverByModuleResult>.Success(result, "Approvers fetch successfully");

        }
    }
}