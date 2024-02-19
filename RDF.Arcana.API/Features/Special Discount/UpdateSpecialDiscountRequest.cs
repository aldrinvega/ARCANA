using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Special_Discount;

[Route("api/special-discount"), ApiController]

public class UpdateSpecialDiscountRequest : ControllerBase
{

    private readonly IMediator _mediator;

    public UpdateSpecialDiscountRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody]UpdateSpecialDiscountRequestCommand command, [FromRoute] int id)
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

        }catch(Exception ex)
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

            var discount = decimal.Round(request.Discount / 100, 4);
            var specialDiscount = await _context.SpecialDiscounts
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.Approvals)
                .FirstOrDefaultAsync(sp => sp.Id == request.SpDiscountId, cancellationToken);

            if (specialDiscount == null)
            {
                return SpecialDiscountErrors.NotFound();
            }

            //Get all the existing approver for the request
            var approver = await _context.RequestApprovers
                .Where(x => x.RequestId == specialDiscount.RequestId)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            if (request.RoleName == Roles.Cdo)
            {
                var haveUnderReviewRequest = await _context.SpecialDiscounts
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.Id != request.SpDiscountId && x.Status == Status.UnderReview);

                if (haveUnderReviewRequest != null)
                {
                    return SpecialDiscountErrors.PendingRequest(haveUnderReviewRequest.Client.Fullname);
                }

                specialDiscount.Request.Status = Status.UnderReview;
                specialDiscount.Status = Status.UnderReview;
                specialDiscount.Request.CurrentApproverId = approver.First().ApproverId;

                foreach (var approval in specialDiscount.Request.Approvals)
                {
                    approval.IsActive = false;
                }

                var newUpdateHistory = new UpdateRequestTrail(
                    specialDiscount.RequestId,
                    Modules.RegistrationApproval,
                    DateTime.Now,
                    request.ModifiedBy);

                await _context.UpdateRequestTrails.AddAsync(newUpdateHistory, cancellationToken);

                var notificationForCurrentApprover = new Domain.Notification
                {
                    UserId = approver.First().ApproverId,
                    Status = Status.PendingClients
                };

                await _context.Notifications.AddAsync(notificationForCurrentApprover, cancellationToken);

                var notification = new Domain.Notification
                {
                    UserId = specialDiscount.AddedBy,
                    Status = Status.PendingClients
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);
            }

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
