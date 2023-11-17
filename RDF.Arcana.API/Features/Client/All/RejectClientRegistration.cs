using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

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
        command.ClientId = id;
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.RequestedBy = userId;
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

    public record RejectClientCommand : IRequest<Result<RejectedClientResult>>
    {
        public int ClientId { get; set; }
        public string Reason { get; set; }
        public int RequestedBy { get; set; }
    }

    public sealed record RejectedClientResult
    {
        public int ClientId { get; set; }
        public string Fullname { get; set; }
        public string BusinessName { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectClientCommand, Result<RejectedClientResult>>
    {
        private const string REJECTED = "Rejected";
        private const string DIRECT_REGISTRATION_APPROVAL = "Direct Registration Approval";
        private const string FOR_REGULAR_APPROVAL = "For regular approval";
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<RejectedClientResult>> Handle(RejectClientCommand request,
            CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                .Include(x => x.Approvals)
                .Where(x => x.Approvals.Any(x =>
                    (x.ApprovalType == DIRECT_REGISTRATION_APPROVAL ||
                     x.ApprovalType == FOR_REGULAR_APPROVAL) &&
                    x.IsActive &&
                    x.IsApproved == false))
                .FirstOrDefaultAsync(x =>
                    x.Id == request.ClientId, cancellationToken);

            if (existingClient == null)
            {
                return Result<RejectedClientResult>.Failure(ClientErrors.NotFound());
            }

            // Adjust this kapag may Approval system na dapat ang approver na naka assign lang ang makaka reject nito

            /*if (existingClient.AddedBy != request.RequestedBy)
            {
                return Result<RejectedClientResult>.Failure(ClientErrors.Unauthorized());
            }*/

            if (existingClient.RegistrationStatus == REJECTED)
            {
                return Result<RejectedClientResult>.Failure(
                    ClientErrors.AlreadyRejected(existingClient.RegistrationStatus));
            }

            existingClient.RegistrationStatus = REJECTED;
            foreach (var approval in existingClient.Approvals.Where(approval =>
                         approval.ApprovalType == DIRECT_REGISTRATION_APPROVAL))
            {
                approval.IsActive = false;
                approval.IsActive = false;
                approval.Reason = request.Reason;
            }

            await _context.SaveChangesAsync(cancellationToken);

            var result = new RejectedClientResult
            {
                ClientId = existingClient.Id,
                Fullname = existingClient.Fullname,
                BusinessName = existingClient.BusinessName,
                Reason = request.Reason
            };

            return Result<RejectedClientResult>.Success(result,
                $"{existingClient.BusinessName} is rejected successfully");
        }
    }
}