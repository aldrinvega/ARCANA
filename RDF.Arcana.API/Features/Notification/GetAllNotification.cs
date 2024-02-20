using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Notification;

[Route("api/Notification")]

public class GetAllNotification : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllNotification(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("Notification")]
    public async Task<IActionResult> Notification([FromQuery] GetAllNotificationQuery query)
    {
        if (User.Identity is ClaimsIdentity identity
            && IdentityHelper.TryGetUserId(identity, out var userId))
        {
            query.UserId = userId;

            var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

            if (roleClaim != null)
            {
                query.RoleName = roleClaim.Value;
            }
        }

        var result = await _mediator.Send(query);
        if (result.IsFailure)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    public class GetAllNotificationQuery : IRequest<Result>
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }

    public class GetAllNotificationResult
    {
        //Client (Prospecting)
        public int ForFreebies  { get; set; }
        public int ForReleasing { get; set; }
        public int Released { get; set; }
        
        //Client (Approval)
        public int PendingClient { get; set; }
        public int ApprovedClient { get; set; }
        public int RejectedClient { get; set; }
        
        /// ListingFee  
        public int PendingListingFee { get; set; }
        public int ApprovedListingFee { get; set; }
        public int RejectedListingFee { get; set; }
        
        //OtherExpenses

        public int PendingExpenses { get; set; }
        public int ApprovedExpenses { get; set; }
        public int RejectedExpenses { get; set; }

        //SpecialDiscount

        public int PendingSpDisocunt { get; set; }
        public int ApprovedSpDisocunt { get; set; }
        public int RejectedSpDisocunt { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllNotificationQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
        {
            var forFreebies = 0;
            var forReleasing = 0;
            var released = 0;
            var pendingClient = 0;
            var approvedClient = 0;
            var rejectedClient = 0;
            var pendingListingFee = 0;
            var approvedListingFee = 0;
            var rejectedListingFee = 0;
            var pendingExpenses = 0;
            var approvedExpenses = 0;
            var rejectedExpenses = 0;
            var pendingSpDiscount = 0;
            var approvedSpDisocunt = 0;
            var rejectedSpDiscount = 0;
            
            forFreebies = await _context.Notifications
                .Where(x => x.Status == Status.NoFreebies)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);

            forReleasing = await _context.Notifications
                .Where(x => x.Status == Status.ForReleasing)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);

            released = await _context.Notifications
                .Where(x => x.Status == Status.Released)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                    
            pendingClient = await _context.Notifications
                .Where(x => x.Status == Status.PendingClients)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                
            approvedClient = await _context.Notifications
                .Where(x => x.Status == Status.ApprovedClients)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                
            rejectedClient = await _context.Notifications
                .Where(x => x.Status == Status.RejectedClients)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                    
            pendingListingFee = await _context.Notifications
                .Where(x => x.Status == Status.PendingListingFee)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                
            approvedListingFee = await _context.Notifications
                .Where(x => x.Status == Status.ApprovedListingFee)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                
            rejectedListingFee = await _context.Notifications
                .Where(x => x.Status == Status.RejectedListingFee)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                    
            pendingExpenses = await _context.Notifications
                .Where(x => x.Status == Status.PendingExpenses)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);
                
            approvedExpenses = await _context.Notifications
                .Where(x => x.Status == Status.ApprovedExpenses)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);

            rejectedExpenses = await _context.Notifications
                .Where(x => x.Status == Status.RejectedExpenses)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);

            pendingSpDiscount = await _context.Notifications
                .Where(x => x.Status == Status.PendingSPDiscount)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);

            rejectedExpenses = await _context.Notifications
                .Where(x => x.Status == Status.ApprovedSpDiscount)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);

            rejectedExpenses = await _context.Notifications
                .Where(x => x.Status == Status.RejectedSpDiscount)
                .Where(x => x.UserId == request.UserId)
                .Where(x => x.IsRead == false)
                .CountAsync(cancellationToken);


            var notification = new GetAllNotificationResult
            {
                ForFreebies = forFreebies,
                ForReleasing = forReleasing,
                Released = released,
                PendingClient = pendingClient,
                ApprovedClient = approvedClient,
                RejectedClient = rejectedClient,
                PendingListingFee = pendingListingFee,
                ApprovedListingFee = approvedListingFee,
                RejectedListingFee = rejectedListingFee,
                PendingExpenses = pendingExpenses,
                ApprovedExpenses = approvedExpenses,
                RejectedExpenses = rejectedExpenses
            };

            if(request.RoleName == Roles.Admin)
            {
                var adminNotification = new GetAllNotificationResult
                {
                    ForFreebies = 0,
                    ForReleasing = 0,
                    Released = 0,
                    PendingClient = 0,
                    ApprovedClient = 0,
                    RejectedClient = 0,
                    PendingListingFee = 0,
                    ApprovedListingFee = 0,
                    RejectedListingFee = 0,
                    PendingExpenses = 0,
                    ApprovedExpenses = 0,
                    RejectedExpenses = 0
                };

                return Result.Success(adminNotification);

            }

            return Result.Success(notification);
        }
    }
}