using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Client.Regular;

[Route("api/RegularClients")]
[ApiController]
public class RejectRegularRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectRegularRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectRegularRegistration/{id:int}")]
    public async Task<IActionResult> RejectRegularClients(RejectRegularRegistrationCommand command, int id)
    {
        try
        {
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

    public class RejectRegularRegistrationCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectRegularRegistrationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectRegularRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            var regularClients =
                await _context.Requests
                    .Include(x => x.Clients)
                    .Include(approval => approval.Approvals)
                    .FirstOrDefaultAsync(
                        x => x.Id == request.RequestId &&
                             x.Status != Status.Rejected,
                        cancellationToken);

            if (regularClients == null)
            {
                return ClientErrors.NotFound();
            }

            if (regularClients.Clients.RegistrationStatus == Status.Rejected)
            {
                return ClientErrors.AlreadyRejected(regularClients.Clients.BusinessName);
            }
            
            var approvers = await _context.Approvers
                .Where(module => module.ModuleName == Modules.RegistrationApproval)
                .ToListAsync(cancellationToken);
            
            var currentApproverLevel = approvers
                .FirstOrDefault(approver => approver.UserId == regularClients.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.FreebiesApproval);
            }

            var newApproval = new Approval(
                regularClients.Id,
                regularClients.CurrentApproverId,
                Status.Rejected,
                request.Reason,
                true
            );

            await _context.Approval.AddAsync(newApproval, cancellationToken);

            regularClients.Clients.RegistrationStatus = Status.Rejected;
            regularClients.Status = Status.Rejected;
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}