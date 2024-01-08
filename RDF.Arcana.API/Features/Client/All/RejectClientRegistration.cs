using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/Clients"), ApiController]
public class RejectClientRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectClientRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectClientRegistration/{id:int}")]
    public async Task<IActionResult> RejectClients([FromRoute] int id, [FromBody] RejectClientCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AccessBy = userId;
            }
            
            command.RequestId = id;

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

    public sealed record RejectClientCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public string Reason { get; set; }
        public int AccessBy { get; set; }
    }

    public sealed record RejectedClientResult
    {
        public int ClientId { get; set; }
        public string Fullname { get; set; }
        public string BusinessName { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectClientCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectClientCommand request,
            CancellationToken cancellationToken)
        {
            
            var existingClientRequest = await _context.Requests
                .Include(client => client.Clients)
                .Where(rq => rq.Id == request.RequestId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingClientRequest == null)
            {
                return ClientErrors.NotFound();
            }

            if (existingClientRequest.Status == Status.Rejected)
            {
                return ClientErrors.AlreadyRejected(existingClientRequest.Clients.BusinessName);
            }

            if (existingClientRequest.CurrentApproverId != request.AccessBy)
            {
                return ApprovalErrors.NotAllowed(Modules.RegistrationApproval);
            }

            if (existingClientRequest.Status == Status.Rejected)
            {
                return ClientErrors.AlreadyRejected(existingClientRequest.Status);
            }

            var newApproval = new Approval
            (
                existingClientRequest.Id,
                existingClientRequest.CurrentApproverId,
                Status.Rejected,
                request.Reason,
                true
            );
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            
            existingClientRequest.Status = Status.Rejected;
            existingClientRequest.Clients.RegistrationStatus = Status.Rejected;

            await _context.SaveChangesAsync(cancellationToken);

            var result = new RejectedClientResult
            {
                ClientId = existingClientRequest.Id,
                Fullname = existingClientRequest.Clients.Fullname,
                BusinessName = existingClientRequest.Clients.BusinessName,
                Reason = request.Reason
            };

            return Result.Success(result);
        }
    }
}