using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Requests_Approval;

[Route("api/Approvers"), ApiController]
public class AddApproversPerModuleByRange : ControllerBase
{
    private readonly IMediator _mediator;

    public AddApproversPerModuleByRange(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AssignApproverWithRange")]
    public async Task<IActionResult> AssignApprover([FromBody] AddApproversPerModuleByRangeCommand command)
    {
        try
        {
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


    public record AddApproversPerModuleByRangeCommand : IRequest<Result>
    {
        public string ModuleName { get; set; }
        public IEnumerable<ApproverDto> Approvers { get; set; }

        public class ApproverDto
        {
            public int UserId { get; set; }
            public decimal MinValue { get; set; }
            public decimal MaxValue { get; set; }
        }
    }

    public class Handler : IRequestHandler<AddApproversPerModuleByRangeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddApproversPerModuleByRangeCommand request, CancellationToken cancellationToken)
        {
            foreach (var approver in request.Approvers)
            {

                var overlappingApprover = await _context.ApproverByRange
                    .FirstOrDefaultAsync(ap =>
                        ap.ModuleName == request.ModuleName &&
                        ((approver.MinValue >= ap.MinValue && approver.MinValue <= ap.MaxValue) ||
                         (approver.MaxValue >= ap.MinValue && approver.MaxValue <= ap.MaxValue) ||
                         (approver.MinValue <= ap.MinValue && approver.MaxValue >= ap.MaxValue)),
                    cancellationToken);

                if (overlappingApprover != null)
                {
                    return ApprovalErrors.ExistingFeeRangeOrModule();
                }

                var newApprover = new ApproverByRange
                {
                    UserId = approver.UserId,
                    ModuleName = request.ModuleName,
                    MinValue = approver.MinValue,
                    MaxValue = approver.MaxValue
                };
                _context.ApproverByRange.Add(newApprover);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
