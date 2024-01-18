using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Notification;

[Route("api/Notification"), ApiController]

public class ReadNotification : ControllerBase
{

    private readonly IMediator _mediator;

    public ReadNotification(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch]
    public async Task<IActionResult> Read([FromQuery]ReadNotificationCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.UserId = userId;
            }

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class ReadNotificationCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string Tab { get; set; }
    }
    
    public class Handler : IRequestHandler<ReadNotificationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ReadNotificationCommand request, CancellationToken cancellationToken)
        {
            var unreadNotification = await _context.Notifications
                .Where(user =>
                    user.Status == request.Tab &&
                    user.UserId == request.UserId
                ).ToListAsync(cancellationToken);

            foreach (var notification in unreadNotification)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Result.Success();
        }
    }
}