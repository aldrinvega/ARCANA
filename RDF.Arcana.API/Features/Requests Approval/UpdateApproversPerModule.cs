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
    public async Task<IActionResult> UpdateApprovers([FromBody]UpdateApproversPerModuleCommand command, [FromQuery] string moduleName)
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

    public record UpdateApproversPerModuleCommand : IRequest<Result<UpdateApproversPerModuleResult>>
    {
        public string ModuleName { get; set; }
        
        public ICollection<ApproverToUpdatePerModule> Approvers { get; set; }
        public class ApproverToUpdatePerModule
        {
            public int UserId { get; set; }
            public int Level { get; set; }
        }
    }

    public record UpdateApproversPerModuleResult
    {
        public string ModuleName { get; set; }
        public ICollection<ApproverToUpdate> Approvers { get; set; }
        
        public class ApproverToUpdate
        {
            public int UserId { get; set; }
            public int Level { get; set; }
        }
        
    }
    
    public class Handler : IRequestHandler<UpdateApproversPerModuleCommand, Result<UpdateApproversPerModuleResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<UpdateApproversPerModuleResult>> Handle(UpdateApproversPerModuleCommand request, CancellationToken cancellationToken)
        {
            var existingModule = await _context.Approvers
                .Include(user => user.User)
                .Where(module => module.ModuleName == request.ModuleName).ToListAsync(cancellationToken);

            if (!existingModule.Any())
            {
                return Result<UpdateApproversPerModuleResult>.Failure(ApprovalErrors
                    .NoApproversFound(request.ModuleName));
            }
            
            var approverResult = new Collection<UpdateApproversPerModuleResult.ApproverToUpdate>();

            foreach (var approvers in request.Approvers)
            {

                var approverEntity = new Approver
                {
                    UserId = approvers.UserId,
                    Level = approvers.Level
                };

                var user = await _context.Users
                    .Include(user => user.UserRoles)
                    .FirstOrDefaultAsync(user => user.Id == approvers.UserId, cancellationToken);

                if (!user.UserRoles.Permissions.Contains(request.ModuleName))
                {
                    return Result<UpdateApproversPerModuleResult>
                        .Failure(ApprovalErrors.NoAccess(request.ModuleName, user.Fullname));
                }

                await _context.Approvers.Upsert(approverEntity)
                    .On(c => new { c.ModuleName, c.UserId })
                    .WhenMatched(c => new Approver
                    {
                        UserId = approverEntity.UserId,
                        Level = approverEntity.Level
                    }).RunAsync(cancellationToken);
                
                approverResult.Add(new UpdateApproversPerModuleResult.ApproverToUpdate
                {
                    UserId = approverEntity.UserId,
                    Level = approverEntity.Level
                });
            }

            var result = new UpdateApproversPerModuleResult
            {
                ModuleName = request.ModuleName,
                Approvers = approverResult,
            };
            await _context.SaveChangesAsync(cancellationToken);

            return Result<UpdateApproversPerModuleResult>
                .Success(result, $"Approvers for the module {request.ModuleName} have been successfully updated.");

        }
    }
}