using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

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
             var getAllRequestedClient = _context.RequestedClients.Count(x => x.Status == 1 && x.IsActive == true);
             var getAllArchivedRequestedClient = _context.RequestedClients.Count(x => x.Status == 1 && x.IsActive == false);
     
             var getAllRejectedClients = _context.RejectedClients.Count(x => x.Status == 3 && x.IsActive == true);
             var getAllArchivedRejectedClients = _context.RejectedClients.Count(x => x.Status == 3 && x.IsActive == false);
     
             var getAllApprovedClients = _context.ApprovedClients.Count(x => x.Status == 2 && x.IsActive == true);
             var getAllArchivedApprovedClients = _context.ApprovedClients.Count(x => x.Status == 2 && x.IsActive == false);
             var getAllRequestedFreebies = _context.FreebieRequests.Count(x => x.StatusId == 2 && x.IsActive == true);
     
             var result = new NotificationServiceQueryResult
             {
                 RequestedClient = getAllRequestedClient,
                 ArchivedRequestedClient = getAllArchivedRequestedClient,
                 RejectedClient = getAllRejectedClients,
                 ArchivedRejectedClient = getAllArchivedRejectedClients,
                 ApprovedClient = getAllApprovedClients,
                 ArchivedApprovedClient = getAllArchivedApprovedClients,
                 RequestedFreebies = getAllRequestedFreebies
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