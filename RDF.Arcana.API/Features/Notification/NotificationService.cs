using Microsoft.AspNetCore.Mvc;
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

    public class NotificationServiceQuery : IRequest<NotificationServiceQueryResult>{}

    public class NotificationServiceQueryResult
    {
        public int RequestedClient  { get; set; }
        public int ArchivedRequestedClient { get; set; }
        public int RejectedClient { get; set; }
        public int ArchivedRejectedClient { get; set; }
        public int ApprovedClient { get; set; }
        public int ArchivedApprovedClient { get; set; }
        
        /// Freebies  
        public int RequestedFreebies { get; set; }
    }

    public class Handler : IRequestHandler<NotificationServiceQuery, NotificationServiceQueryResult>
     {
         private readonly DataContext _context;
     
         public Handler(DataContext context)
         {
             _context = context;
         }
         
         public async Task<NotificationServiceQueryResult> Handle(NotificationServiceQuery request, CancellationToken cancellationToken)
         {
             var getAllRequestedClient = _context.Approvals.Count(x => x.ApprovalType == "Prospecting Approval" && x.IsActive == true && x.IsApproved);
             var getAllArchivedRequestedClient = _context.Approvals.Count(x => x.ApprovalType == "Prospecting Approval" && x.IsActive == false);
     
             var getAllRejectedClients = _context.Approvals.Count(x => x.ApprovalType == "Prospecting Approval" && x.IsApproved == false);
             var getAllArchivedRejectedClients = _context.Approvals.Count(x => x.ApprovalType == "Prospecting Approval" && x.IsActive == false && x.Client.RegistrationStatus == "Rejected");
     
             var getAllApprovedClients = _context.Approvals.Count(x => x.ApprovalType == "Prospecting Approval" && x.IsActive == true && x.IsApproved == true);
             var getAllArchivedApprovedClients = _context.Approvals.Count(x => x.ApprovalType == "Prospecting Approval" && x.IsActive == true && x.IsApproved == true && x.Client.RegistrationStatus == "Approed");
             // var getAllRequestedFreebies = _context.FreebieRequests.Count(x => x.StatusId == 2 && x.IsActive == true);
     
             var result = new NotificationServiceQueryResult
             {
                 RequestedClient = getAllRequestedClient,
                 ArchivedRequestedClient = getAllArchivedRequestedClient,
                 RejectedClient = getAllRejectedClients,
                 ArchivedRejectedClient = getAllArchivedRejectedClients,
                 ApprovedClient = getAllApprovedClients,
                 ArchivedApprovedClient = getAllArchivedApprovedClients,
                 // RequestedFreebies = getAllRequestedFreebies
             };
     
             return await Task.FromResult(result);
         }
     }

    [HttpGet("NotificationService")]
    public async Task<IActionResult> NotificationServices()
    {
        try
        {
            var query = new NotificationServiceQuery();
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