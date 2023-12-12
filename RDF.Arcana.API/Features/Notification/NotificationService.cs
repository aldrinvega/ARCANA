using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Notification;

[Route("api/Notification")]
[ApiController]

public class NotificationService : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public sealed record NotificationServiceQuery : IRequest<Result>
    {
        public int AddedBy { get; set; }
        public string Role { get; set; }
    }

    private class NotificationServiceQueryResult
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
    }

    public class Handler : IRequestHandler<NotificationServiceQuery, Result>
     {
         private readonly ArcanaDbContext _context;
     
         public Handler(ArcanaDbContext context)
         {
             _context = context;
         }
         
         public Task<Result> Handle(NotificationServiceQuery request, CancellationToken cancellationToken)
         {
             var pendingCount = 0;
             var approvedCount = 0;
             var rejectedCount = 0;
             var pendingListingFeeCount = 0;
             var approveListingFeeCount = 0;
             var rejectedListingFeeCount = 0;
             var forFreebiesCount = 0;
             var forReleasingCount = 0;
             var releasedCount = 0;
             
             switch (request.Role)
             {
                 case Roles.Approver:
                     pendingCount = _context.Clients
                         .Include(x => x.Request)
                         .ThenInclude(ap => ap.Approvals)
                         .Count(x => x.Request.CurrentApproverId == request.AddedBy && x.Request.Status == Status.UnderReview && x.Request.Module == Modules.RegistrationApproval );

                     approvedCount = _context.Clients
                         .Include(x => x.Request)
                         .ThenInclude(ap => ap.Approvals)
                         .Where(x => x.Approvals != null)
                         .SelectMany(x => x.Request.Approvals)
                         .Count(ap => ap.ApproverId == request.AddedBy && ap.Status == Status.Approved && ap.Request.Module == Modules.RegistrationApproval);
                 
                     rejectedCount = _context.Clients
                         .Include(x => x.Request)
                         .ThenInclude(ap => ap.Approvals)
                         .Where(x => x.Approvals != null)
                         .SelectMany(x => x.Request.Approvals)
                         .Count(ap => ap.ApproverId == request.AddedBy && ap.Status == Status.Rejected && ap.Request.Module == Modules.RegistrationApproval);
                     
                     pendingListingFeeCount = _context.ListingFees
                         .Include(x => x.Request)
                         .Count(approval => approval.Request.CurrentApproverId == request.AddedBy
                                            && approval.Request.Status == Status.UnderReview
                                            && approval.Request.Module == Modules.ListingFeeApproval);

                    approveListingFeeCount = _context.ListingFees
                        .Include(x => x.Request)
                        .ThenInclude(ap => ap.Approvals)
                        .Where(x => x.Request.Approvals != null)
                        .SelectMany(x => x.Request.Approvals)
                         .Count(approval => approval.ApproverId == request.AddedBy && approval.Status == Status.Approved && approval.Request.Module == Modules.ListingFeeApproval);

                    rejectedListingFeeCount = _context.ListingFees
                        .Include(x => x.Request)
                        .ThenInclude(ap => ap.Approvals)
                        .Where(x => x.Request.Approvals != null)
                        .SelectMany(x => x.Request.Approvals)
                        .Count(approval => approval.ApproverId == request.AddedBy
                                            && approval.Status == Status.Rejected
                                            && approval.Request.Module == Modules.ListingFeeApproval);
                     break;
                 
                 case Roles.Cdo or Roles.Admin:
                     
                     pendingCount = _context.Clients
                         .Count(x => x.AddedBy == request.AddedBy && 
                                     x.RegistrationStatus == Status.UnderReview && 
                                     x.IsActive );

                     approvedCount = _context.Clients
                         .Count(x => x.AddedBy == request.AddedBy && 
                                     x.RegistrationStatus == Status.Approved && 
                                     x.IsActive);
                 
                     rejectedCount = _context.Clients
                         .Include(x => x.Request)
                         .ThenInclude(ap => ap.Approvals)
                         .Count(x => x.AddedBy == request.AddedBy && 
                                     x.Request.Status == Status.Rejected && 
                                     x.IsActive);
                 
                     pendingListingFeeCount= _context.ListingFees
                         .Count(ap => ap.RequestedBy == request.AddedBy && 
                                      ap.Status == Status.UnderReview && 
                                      ap.IsActive);

                     approveListingFeeCount= _context.ListingFees
                         .Count(ap => ap.RequestedBy == request.AddedBy && 
                                      ap.Status == Status.Approved && 
                                      ap.IsActive );
                 
                     rejectedListingFeeCount = _context.ListingFees
                         .Count(ap => 
                             ap.RequestedBy == request.AddedBy && 
                             ap.Status == Status.Rejected && ap.IsActive);

                     forFreebiesCount = _context.Clients
                         .Where(x =>
                             x.RegistrationStatus == Status.Requested &&
                             x.AddedBy == request.AddedBy &&
                             x.IsActive &&
                             x.Origin == Origin.Prospecting)
                         .Include(fr => fr.FreebiesRequests)
                         .Count(x => !x.FreebiesRequests.Any());
                     
                     forReleasingCount = _context.Clients
                         .Where(client => client.IsActive &&
                                          client.RegistrationStatus == Status.Requested)
                         .Include(x => x.FreebiesRequests)
                         .Count(x => x.FreebiesRequests.Any(x => 
                             x.Status == Status.ForReleasing && 
                             x.RequestedBy == request.AddedBy));
                     
                     releasedCount = _context.Clients
                         .Where(x => 
                             x.IsActive && 
                             x.Origin == Origin.Prospecting &&
                             x.RegistrationStatus == Status.PendingRegistration)
                         .Include(x => x.FreebiesRequests)
                         .Count(x => x.FreebiesRequests.Any(x => 
                             x.Status == Status.Released && x.RequestedBy == request.AddedBy));
                     break;
             }

             var result = new NotificationServiceQueryResult
             {
                 ForFreebies = forFreebiesCount,
                 ForReleasing = forReleasingCount,
                 Released = releasedCount,
                 PendingClient = pendingCount,
                 ApprovedClient = approvedCount,
                 RejectedClient = rejectedCount,
                 PendingListingFee = pendingListingFeeCount,
                 ApprovedListingFee = approveListingFeeCount,
                 RejectedListingFee = rejectedListingFeeCount
             };

             return Task.FromResult<Result>(Result.Success(result));
         }
     }

    [HttpGet("NotificationService")]
    public async Task<IActionResult> NotificationServices()
    {
        try
        {
            
            var query = new NotificationServiceQuery();
            if (User.Identity is ClaimsIdentity identity)
            {
                if (IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    query.AddedBy = userId;
                }

                var role = IdentityHelper.GetRole(identity);
                if (!string.IsNullOrEmpty(role))
                {
                    query.Role = role;
                }
            }
            var notification = await _mediator.Send(query);
            return Ok(notification);
        }
        catch (Exception e)
        {
            return Conflict(new
            {
                e.Message
            });
        }
    }
}