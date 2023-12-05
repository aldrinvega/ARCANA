using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Requests_Approval;

[Route("api/Approver"), ApiController]

public class UpdateApproversPerModule : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateApproversPerModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateApproversPerModule")]
    public async Task<IActionResult> UpdateApprovers([FromBody] UpdateApproversPerModuleCommand command,
        [FromQuery] string moduleName)
    {
        try
        {
            command.ModuleName = moduleName;
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

    public record UpdateApproversPerModuleCommand : IRequest<Result>
    {
        public string ModuleName { get; set; }
        public ICollection<ApproverToUpdatePerModule> Approvers { get; set; }

        public class ApproverToUpdatePerModule
        {
            public int UserId { get; set; }
            public int Level { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateApproversPerModuleCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateApproversPerModuleCommand request,
            CancellationToken cancellationToken)
        {
            var existingApprovers = await _context.Approvers
                .Where(approver => approver.ModuleName == request.ModuleName)
                .ToListAsync(cancellationToken);

            var sentUserIds = request.Approvers.Select(a => a.UserId).ToList();

            // Remove approvers in the database that does not exist in the request
            var approversToRemove =
                existingApprovers.Where(approver => !sentUserIds.Contains(approver.UserId)).ToList();
            _context.Approvers.RemoveRange(approversToRemove);

            foreach (var sentApprover in request.Approvers)
            {
                var existingApprover = existingApprovers.FirstOrDefault(a => a.UserId == sentApprover.UserId);

                if (existingApprover != null) // If the approver exists in the database, update the approver
                {
                    existingApprover.Level = sentApprover.Level;
                }
                else // If the approver does not exist in the database, add the approver
                {
                    _context.Approvers.Add(new Approver
                    {
                        UserId = sentApprover.UserId,
                        Level = sentApprover.Level,
                        ModuleName = request.ModuleName,
                        IsActive = true
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}