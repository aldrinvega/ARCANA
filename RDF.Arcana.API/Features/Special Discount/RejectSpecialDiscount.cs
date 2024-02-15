using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Special_Discount;
[Route("api/special-dsicount"), ApiController]

public class RejectSpecialDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectSpecialDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("reject/{id}")]
    public async Task<IActionResult> Reject([FromBody] RejectSpecialDiscountCommand command, int id)
    {
        try
        {
            command.RequestId = id;

            var result = await _mediator.Send(command);

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

    public class RejectSpecialDiscountCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectSpecialDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectSpecialDiscountCommand request, CancellationToken cancellationToken)
        {
            var specialDiscountRequest =
               await _context.Requests
                   .Include(sp => sp.SpecialDiscount)
                   .Include(approval => approval.Approvals)
                   .FirstOrDefaultAsync(
                       x => x.Id == request.RequestId &&
                            x.Status != Status.Rejected,
                       cancellationToken);

            if (specialDiscountRequest == null)
            {
                return SpecialDiscountErrors.NotFound();
            }

            if (specialDiscountRequest.SpecialDiscount.Status == Status.Rejected)
            {
                return SpecialDiscountErrors.AlreadyRejected();
            }

            var approvers = await _context.Approvers
                .Where(module => module.ModuleName == Modules.RegistrationApproval)
                .ToListAsync(cancellationToken);

            var currentApproverLevel = approvers
                .FirstOrDefault(approver => approver.UserId == specialDiscountRequest.CurrentApproverId)?.Level;

            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.FreebiesApproval);
            }

            var newApproval = new Approval(
                specialDiscountRequest.Id,
                specialDiscountRequest.CurrentApproverId,
                Status.Rejected,
                request.Reason,
                true
            );

            await _context.Approval.AddAsync(newApproval, cancellationToken);

            specialDiscountRequest.SpecialDiscount.Status = Status.Rejected;
            specialDiscountRequest.Status = Status.Rejected;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
