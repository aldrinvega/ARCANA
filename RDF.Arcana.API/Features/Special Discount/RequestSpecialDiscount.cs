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

            var discount = decimal.Round(request.Discount / 100, 4);

            var client = await _context.Clients
                .FirstOrDefaultAsync(cl => 
                cl.Id == request.ClientId && 
                cl.RegistrationStatus == Status.Approved, 
                cancellationToken);

            if (client == null)
            {
                return ClientErrors.NotFound();
            }
            
            var withPendinRequest = await _context.SpecialDiscounts
                .AnyAsync(x => 
                x.ClientId == request.ClientId && 
                x.Status == Status.UnderReview, 
                cancellationToken);

            if (withPendinRequest)
            {
                return SpecialDiscountErrors.PendingRequest(client.BusinessName);
            }

            decimal total = request.Discount;

            var approvers = await _context.Approvers
                .Include(usr => usr.User)
                .Where(x => x.ModuleName == Modules.SpecialDiscountApproval)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);


            if (!approvers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.SpecialDiscountApproval);
            }

            if (total <= 5 && approvers.Count >= 2)
            {
                approvers = new List<Approver> { approvers[1] };
            }
            else
            {
                approvers = new List<Approver> { approvers[0] };
            }


            //has fixed discount 6-13-24
            //if (client.FixedDiscountId is not null)
            //{
            //    var fixedDiscount = await _context.FixedDiscounts
            //        .Where(fd => fd.Id == client.FixedDiscountId)
            //        .FirstOrDefaultAsync();

            //    var totalFix_Sp = (fixedDiscount.DiscountPercentage + discount) * 100;

            //    if (totalFix_Sp <= 10)
            //    {
            //        //this should be CDO
            //        approvers.Where(a => a.ModuleName == Modules.RegistrationApproval)
            //            .OrderBy(x => x.Level);
            //    }
            //    else if (totalFix_Sp > 10 && totalFix_Sp <= 15)
            //    {
            //        approvers.Where(a => a.ModuleName == Modules.RegistrationApproval)
            //            .OrderBy(x => x.Level);
            //    }
            //    else
            //    {
            //        approvers.Where(a => a.ModuleName == Modules.SpecialDiscountApproval)
            //            .OrderBy(x => x.Level);
            //    }

            //}

            var newRequest = new Request(
                Modules.SpecialDiscountApproval,
                request.AddedBy,
                approvers.First().UserId,
                approvers.FirstOrDefault(x => x.Level == 2)?.UserId,
                Status.UnderReview
            );

            await _context.Requests.AddAsync(newRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
            {
                ApproverId = approver.UserId,
                RequestId = newRequest.Id,
                Level = approver.Level,
            }))
            {
                _context.RequestApprovers.Add(newRequestApprover);
            }

            var requestSpecialDiscount = new SpecialDiscount
            {
                ClientId = client.Id,
                AddedBy = request.AddedBy,
                Discount = discount,
                RequestId = newRequest.Id,
                IsOneTime = request.IsOnetime,
                Status = Status.UnderReview
            };

            await _context.SpecialDiscounts.AddAsync(requestSpecialDiscount, cancellationToken);

            var notification = new Domain.Notification
            {
                UserId = request.AddedBy,
                Status = Status.PendingSPDiscount
            };

            await _context.Notifications.AddAsync(notification, cancellationToken);

            var notificationForApprover = new Domain.Notification
            {
                UserId = approvers.First().UserId,
                Status = Status.PendingSPDiscount
            };

            await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = new RequestSpecialDiscountResult
            {
                Requestor = requestor.Fullname,
                RequestorMobileNumber = requestor.MobileNumber,
                Approver = approvers.First().User.Fullname,
                ApproverMobileNumber = approvers.First().User.MobileNumber
            };

            return Result.Success(result);
        }
    }
}
