using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Special_Discount;

[Route("api/special-discount"), ApiController]

public class RequestSpecialDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestSpecialDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RequestSpecialDiscountCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

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

    public class RequestSpecialDiscountCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public decimal Discount { get; set; }
        public bool IsOnetime { get; set; }
        public int AddedBy { get; set; }
    }
    public class RequestSpecialDiscountResult
    {
        public string Requestor { get; set; }
        public string RequestorMobileNumber { get; set; }
        public string Approver { get; set; }
        public string ApproverMobileNumber { get; set; }
    }

    public class Handler : IRequestHandler<RequestSpecialDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RequestSpecialDiscountCommand request, CancellationToken cancellationToken)
        {
            var requestor = await _context.Users.FirstOrDefaultAsync(usr => usr.Id == request.AddedBy, cancellationToken);
            if (requestor == null)
            {
                return UserErrors.NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(cl =>
                    cl.Id == request.ClientId &&
                    cl.RegistrationStatus == Status.Approved,
                    cancellationToken);

            if (client == null)
            {
                return ClientErrors.NotFound();
            }

            var hasPendingRequest = await _context.SpecialDiscounts
                .AnyAsync(x =>
                    x.ClientId == request.ClientId &&
                    x.Status == Status.UnderReview,
                    cancellationToken);

            if (hasPendingRequest)
            {
                return SpecialDiscountErrors.PendingRequest(client.BusinessName);
            }
           

            var approvers = await _context.ApproverByRange
                .Include(usr => usr.User)
                .Where(x => x.ModuleName == Modules.SpecialDiscountApproval)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            if (!approvers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.SpecialDiscountApproval);
            }

            var applicableApprovers = approvers.Where(a => a.MinValue <= Math.Ceiling(request.Discount)).ToList();
            if (!applicableApprovers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.SpecialDiscountApproval);
            }

            var maxLevelApprover = applicableApprovers.OrderByDescending(a => a.Level).First();
            var approverLevels = approvers.Where(a => a.Level <= maxLevelApprover.Level).OrderBy(a => a.Level).ToList();

            var newRequest = new Request(
                Modules.SpecialDiscountApproval,
                request.AddedBy,
                approverLevels.First().UserId,
                approverLevels.Count > 1 ? approverLevels[1].UserId : (int?)null,
                Status.UnderReview
            );

            await _context.Requests.AddAsync(newRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var approver in approverLevels)
            {
                var requestApprover = new RequestApprovers
                {
                    ApproverId = approver.UserId,
                    RequestId = newRequest.Id,
                    Level = approver.Level
                };

                _context.RequestApprovers.Add(requestApprover);

                var notificationForApprover = new Domain.Notification
                {
                    UserId = approver.UserId,
                    Status = Status.PendingSPDiscount
                };

                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            }

            var discount = decimal.Round(request.Discount / 100, 4);

            var specialDiscount = new SpecialDiscount
            {
                ClientId = client.Id,
                AddedBy = request.AddedBy,
                Discount = discount,
                RequestId = newRequest.Id,
                IsOneTime = request.IsOnetime,
                Status = Status.UnderReview
            };

            await _context.SpecialDiscounts.AddAsync(specialDiscount, cancellationToken);

            var notification = new Domain.Notification
            {
                UserId = request.AddedBy,
                Status = Status.PendingSPDiscount
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            

            return Result.Success();
        }
    }
}
