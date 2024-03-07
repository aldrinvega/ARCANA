using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Special_Discount;
[Route("api/special-discount"), ApiController]

public class ApproveSpecialDiscountRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveSpecialDiscountRequest(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPut("approve/{id}")]
    public async Task<IActionResult> Approve([FromRoute]int id)
    {
        try
        {
            var command = new ApprovedSpecialDiscountRequestCommand
            {
                RequestId = id
            };

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

    public class ApprovedSpecialDiscountRequestCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
    }

    public class Handler : IRequestHandler<ApprovedSpecialDiscountRequestCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ApprovedSpecialDiscountRequestCommand request, CancellationToken cancellationToken)
        {
            var requestedSpDiscount = await _context.Requests
                .Include(sp => sp.SpecialDiscount)
                .Where(client => client.Id == request.RequestId)
                .FirstOrDefaultAsync(cancellationToken);

            if (requestedSpDiscount is null)
            {
                return SpecialDiscountErrors.NotFound();
            }

            var specialDiscountApprovers = await _context.RequestApprovers
                .Where(rq => rq.RequestId == request.RequestId)
                .ToListAsync(cancellationToken);

            var currentApproverLevel = specialDiscountApprovers
                .FirstOrDefault(approver => approver.ApproverId == requestedSpDiscount.CurrentApproverId)?.Level;

            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
            }

            var newApproval = new Approval(
                requestedSpDiscount.Id,
                requestedSpDiscount.CurrentApproverId,
                Status.Approved,
                null,
                true
            );

            var nextLevel = currentApproverLevel.Value + 1;
            var nextApprover = specialDiscountApprovers
                .FirstOrDefault(approver => approver.Level == nextLevel);

            var suceedingApprover = specialDiscountApprovers.FirstOrDefault(ap => ap.Level == nextLevel + 1);

            if (suceedingApprover == null)
            {
                requestedSpDiscount.NextApproverId = null;
            }

            if (nextApprover == null)
            {
                requestedSpDiscount.Status = Status.Approved;
                requestedSpDiscount.SpecialDiscount.Status = Status.Approved;
                requestedSpDiscount.NextApproverId = null;

                var notificationForCurrentApprover = new Domain.Notification
                {
                    UserId = requestedSpDiscount.CurrentApproverId,
                    Status = Status.ApprovedClients
                };

                await _context.Notifications.AddAsync(notificationForCurrentApprover, cancellationToken);

                var notification = new Domain.Notification
                {
                    UserId = requestedSpDiscount.RequestorId,
                    Status = Status.ApprovedClients
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);

            }
            else
            {
                var notificationForCurrentApprover = new Domain.Notification
                {
                    UserId = requestedSpDiscount.CurrentApproverId,
                    Status = Status.ApprovedClients
                };

                await _context.Notifications.AddAsync(notificationForCurrentApprover, cancellationToken);

                requestedSpDiscount.CurrentApproverId = nextApprover.ApproverId;

                var notificationForApprover = new Domain.Notification
                {
                    UserId = nextApprover.ApproverId,
                    Status = Status.PendingClients
                };

                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            }


            await _context.Approval.AddAsync(newApproval, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}
