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
        public int ModifiedBy { get; set; }
        public decimal Discount { get; set; }
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

            var discount = request.Discount / 100;
            var specialDiscount = await _context.SpecialDiscounts
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.Approvals)
                .FirstOrDefaultAsync(sp => sp.Id == request.SpDiscountId);

            if (specialDiscount == null)
            {
                return SpecialDiscountErrors.NotFound();
            }
            //Get all the existing approver for the request
            var approver = await _context.RequestApprovers
                .Where(x => x.RequestId == specialDiscount.RequestId)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            specialDiscount.Discount = discount;
            specialDiscount.UpdatedAt = DateTime.Now;
            specialDiscount.ModifiedBy = request.ModifiedBy;

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
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
