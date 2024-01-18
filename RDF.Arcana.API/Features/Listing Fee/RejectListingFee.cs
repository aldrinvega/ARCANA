using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class RejectListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectListingFee/{id:int}")]
    public async Task<IActionResult> RejectListingFees([FromRoute] int id, [FromBody] RejectListingFeeCommand command)
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
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class RejectListingFeeCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectListingFeeCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectListingFeeCommand request, CancellationToken cancellationToken)
        {
            var existingApprovalsForListingFee =
                await _context.Requests
                    .Include(x => x.ListingFee)
                    .Include(approval => approval.Approvals)
                    .FirstOrDefaultAsync(
                        x => x.Id == request.RequestId &&
                             x.Status != Status.Rejected,
                        cancellationToken);

            if (existingApprovalsForListingFee == null)
            {
                return ListingFeeErrors.NotFound();
            }
            
            var approvers = await _context.Approvers
                .Where(module => module.ModuleName == Modules.ListingFeeApproval)
                .ToListAsync(cancellationToken);
            
            var currentApproverLevel = approvers
                .FirstOrDefault(approver => approver.UserId == existingApprovalsForListingFee.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.FreebiesApproval);
            }

            var newApproval = new Approval(
                existingApprovalsForListingFee.Id,
                existingApprovalsForListingFee.CurrentApproverId,
                Status.Rejected,
                request.Reason,
                true
            );
            
            existingApprovalsForListingFee.ListingFee.Status = Status.Rejected;
            existingApprovalsForListingFee.Status = Status.Rejected;
            
            var notification = new Domain.Notification
            {
                UserId = existingApprovalsForListingFee.RequestorId,
                Status = Status.RejectedListingFee
            };
                
            await _context.Notifications.AddAsync(notification, cancellationToken);
                
            var notificationForApprover = new Domain.Notification
            {
                UserId = existingApprovalsForListingFee.CurrentApproverId,
                Status = Status.RejectedListingFee
            };
                
            await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

            await _context.Approval.AddAsync(newApproval, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}