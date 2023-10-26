using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Regular;

[Route("api/RegularClients")]
[ApiController]
public class ApprovedRegularRegistration : ControllerBase
{
    private readonly DataContext _context;
    private readonly IMediator _mediator;

    public ApprovedRegularRegistration(IMediator mediator, DataContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    [HttpPut("ApprovedForRegularRegistration/{id:int}")]
    public async Task<IActionResult> ApprovedForRegularRegistration([FromRoute] int clientId)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is not ClaimsIdentity identity || !IdentityHelper.TryGetUserId(identity, out var userId))
                return Unauthorized();
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return Forbid("You don't have permission to approve registrations.");
            }

            var command = new ApprovedRegularRegistrationCommand
            {
                ClientId = clientId
            };
            await _mediator.Send(command);
            response.Messages.Add("Registration approved.");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Messages.Add(ex.Message);
            response.Status = StatusCodes.Status409Conflict;
            return BadRequest(response);
        }
    }

    public class ApprovedRegularRegistrationCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class Handler : IRequestHandler<ApprovedRegularRegistrationCommand, Unit>
    {
        private const string FOR_REGULAR = "For regular approval";
        private const string UNDER_REVIEW = "Under review";
        private const string APPROVED = "Approved";

        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ApprovedRegularRegistrationCommand request, CancellationToken cancellationToken)
        {
            var forRegularClients = await _context.Approvals
                .Include(x => x.Client)
                .Where(x => x.ApprovalType == FOR_REGULAR)
                .Where(x => x.Client.RegistrationStatus == UNDER_REVIEW)
                .FirstOrDefaultAsync(x => x.ClientId == request.ClientId, cancellationToken);

            if (forRegularClients == null)
            {
                throw new ClientIsNotFound(request.ClientId);
            }

            forRegularClients.IsApproved = true;
            forRegularClients.ApprovedBy = request.ApprovedBy;
            forRegularClients.Client.RegistrationStatus = APPROVED;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}