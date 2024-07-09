using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Special_Discount
{
    [Route("api/special-discount"), ApiController]
    public class UpdateSpecialDiscountRequest : ControllerBase
    {
        private readonly IMediator _mediator;

        public UpdateSpecialDiscountRequest(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateSpecialDiscountRequestCommand command, [FromRoute] int id)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                    && IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    command.ModifiedBy = userId;

                    var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);
                    if (roleClaim != null)
                    {
                        command.RoleName = roleClaim.Value;
                    }
                }
                command.SpDiscountId = id;

                var result = await _mediator.Send(command);

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

        public class UpdateSpecialDiscountRequestCommand : IRequest<Result>
        {
            public int SpDiscountId { get; set; }
            public int ClientId { get; set; }
            public bool IsOneTime { get; set; }
            public int ModifiedBy { get; set; }
            public decimal Discount { get; set; }
            public string RoleName { get; set; }
        }

        public class Handler : IRequestHandler<UpdateSpecialDiscountRequestCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(UpdateSpecialDiscountRequestCommand request, CancellationToken cancellationToken)
            {
                
                var specialDiscount = await _context.SpecialDiscounts
                    .Include(rq => rq.Request)
                    .ThenInclude(ap => ap.Approvals)
                    .FirstOrDefaultAsync(sp => sp.Id == request.SpDiscountId, cancellationToken);

                if (specialDiscount == null)
                {
                    return SpecialDiscountErrors.NotFound();
                }

                // Check for existing under-review requests
                if (request.RoleName == Roles.Cdo)
                {
                    var haveUnderReviewRequest = await _context.SpecialDiscounts
                        .Include(x => x.Client)
                        .FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.Id != request.SpDiscountId && x.Status == Status.UnderReview);

                    if (haveUnderReviewRequest != null)
                    {
                        return SpecialDiscountErrors.PendingRequest(haveUnderReviewRequest.Client.Fullname);
                    }
                }

                // Determine the appropriate approvers based on the discount using ApproverByRange entity

                var discount = request.Discount;
                var applicableApprovers = await _context.ApproverByRange
                    .Where(ar => ar.ModuleName == Modules.SpecialDiscountApproval && ar.IsActive && Math.Ceiling(discount) >= ar.MinValue)
                    .OrderBy(ar => ar.Level)
                    .ToListAsync(cancellationToken);

                if (!applicableApprovers.Any())
                {
                    return ApprovalErrors.NoApproversFound(Modules.SpecialDiscountApproval);
                }

                discount = decimal.Round(request.Discount / 100, 4);

                var approverLevels = applicableApprovers.OrderBy(a => a.Level).ToList();

                // Update RequestApprovers
                var existingRequestApprovers = await _context.RequestApprovers
                    .Where(x => x.RequestId == specialDiscount.RequestId)
                    .OrderBy(x => x.Level)
                    .ToListAsync(cancellationToken);

                _context.RequestApprovers.RemoveRange(existingRequestApprovers);

                var newRequestApprovers = new List<RequestApprovers>();
                foreach (var approver in approverLevels)
                {
                    var requestApprover = new RequestApprovers
                    {
                        ApproverId = approver.UserId,
                        RequestId = specialDiscount.RequestId,
                        Level = approver.Level
                    };

                    newRequestApprovers.Add(requestApprover);

                    var notificationForApprover = new Domain.Notification
                    {
                        UserId = approver.UserId,
                        Status = Status.PendingSPDiscount
                    };

                    await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
                }

                _context.RequestApprovers.AddRange(newRequestApprovers);

                specialDiscount.Request.Status = Status.UnderReview;
                specialDiscount.Request.CurrentApproverId = approverLevels.First().UserId;

                var newUpdateHistory = new UpdateRequestTrail(
                    specialDiscount.RequestId,
                    Modules.SpecialDiscountApproval,
                    DateTime.Now,
                    request.ModifiedBy);

                await _context.UpdateRequestTrails.AddAsync(newUpdateHistory, cancellationToken);

                var notification = new Domain.Notification
                {
                    UserId = specialDiscount.AddedBy,
                    Status = Status.PendingSPDiscount
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);

                specialDiscount.Discount = discount;
                specialDiscount.UpdatedAt = DateTime.Now;
                specialDiscount.ModifiedBy = request.ModifiedBy;
                specialDiscount.IsOneTime = request.IsOneTime;
                specialDiscount.ClientId = request.ClientId;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
