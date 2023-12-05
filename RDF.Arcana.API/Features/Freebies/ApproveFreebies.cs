using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class ApproveFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("ApproveFreebieRequest/{id:int}")]
    public async Task<IActionResult> ApproveFreebieRequest([FromRoute] int id)
    {
        try
        {
            var command = new ApproveFreebiesCommand
            {
                RequestId = id
            };
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.ApprovedBy = userId;
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

    public class ApproveFreebiesCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class Handler : IRequestHandler<ApproveFreebiesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        } 

        public async Task<Result> Handle(ApproveFreebiesCommand request, CancellationToken cancellationToken)
        {
            var requestedFreebies = await _context.Requests
                .Include(freebie => freebie.FreebieRequest)
                .Where(freebie => freebie.Id == request.RequestId)
                .FirstOrDefaultAsync(cancellationToken);

            var approvers = await _context.Approvers
                .Where(module => module.ModuleName == Modules.FreebiesApproval)
                .ToListAsync(cancellationToken);
            
            var currentApproverLevel = approvers
                .FirstOrDefault(approver => approver.UserId == requestedFreebies.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.FreebiesApproval);
            }
            
            var nextLevel = currentApproverLevel.Value + 1;
            var nextApprover = approvers
                .FirstOrDefault(approver => approver.Level == nextLevel);
            
            var newApproval = new Approval(
                requestedFreebies.Id,
                requestedFreebies.CurrentApproverId,
                Status.Approved,
                null,
                true
            );
            
            if (nextApprover == null)
            {
                requestedFreebies.Status = Status.Approved;
            }
            else
            {
                requestedFreebies.CurrentApproverId = nextApprover.UserId;
            }
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}