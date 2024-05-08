using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Requests_Approval;

[Route("api/Approvers"), ApiController]

public class AddApproversPerModule : ControllerBase
{
    private readonly IMediator _mediator;

    public AddApproversPerModule(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AssignApprover")]
    public async Task<IActionResult> AssignApprover([FromBody]AddApproversPerModuleCommand command)
    {
        try
        {
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

    public record AddApproversPerModuleCommand : IRequest<Result>
    {

        public string? ModuleName { get; set; }
        public IEnumerable<Approver> Approvers { get; set; }
        public class Approver
        {
            public int UserId { get; set; }
            public int Level { get; set; }
        }
        
    }
    public class Handler : IRequestHandler<AddApproversPerModuleCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddApproversPerModuleCommand request, CancellationToken cancellationToken)
        {
            foreach (var approver in request.Approvers)
            {
                var existingApprover = await _context.Approvers
                    .FirstOrDefaultAsync(ap =>
                        ap.UserId == approver.UserId &&
                        ap.ModuleName == request.ModuleName,
                    cancellationToken);

                var user = await _context.Users
                    .Include(user => user.UserRoles)
                    .FirstOrDefaultAsync(user => 
                    user.Id == approver.UserId, 
                    cancellationToken);

                if (existingApprover is not null)
                {
                    return ApprovalErrors.ApproverAlreadyExist(existingApprover.ModuleName);
                }

                if (user is null) continue;
                if (!(user.UserRoles.Permissions == null) &&
                        !user.UserRoles.Permissions.Contains(request.ModuleName))
                {
                    return ApprovalErrors.NoAccess(request.ModuleName, user.Fullname);
                }

                var newApprover = new Approver
                {
                    UserId = approver.UserId,
                    ModuleName = request.ModuleName,
                    Level = approver.Level
                };
                _context.Approvers.Add(newApprover);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}