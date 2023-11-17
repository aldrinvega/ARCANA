using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/UserRole")]
[ApiController]

public class UntagAndTagUserRolePermission : ControllerBase
{
    private readonly IMediator _mediator;

    public UntagAndTagUserRolePermission(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UntagAndTagUserRoleCommand : IRequest<Result<Unit>>
    {
        public int UserRoleId { get; set; }
        public ICollection<string> Permissions { get; set; } = new List<string>();
        public string ModifiedBy { get; set; }

        public class Handler : IRequestHandler<UntagAndTagUserRoleCommand, Result<Unit>>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(UntagAndTagUserRoleCommand request, CancellationToken cancellationToken)
            {
                var existingUserRole = await _context.UserRoles
                    .FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

                if (existingUserRole is null)
                {
                    throw new UserRoleNotFoundException();
                }

                if (existingUserRole.UserRoleName.Contains(Roles.Approver))
                {
                    // Get the permissions/modules that are being removed
                    var removedPermissions = existingUserRole.Permissions?.Except(request.Permissions ?? new List<string>()) ?? Enumerable.Empty<string>();

                    // Get all user ids who have the user role being updated
                    var userIdsWithRole = await _context.Users
                        .Where(u => u.UserRolesId == existingUserRole.Id)
                        .Select(u => u.Id)
                        .ToListAsync(cancellationToken);

                    // Find first approver that user with role is an approver for any of the removed modules
                    //Kasi hindi pwede okay.!
                    var approver = await _context.Approvers
                        .FirstOrDefaultAsync(a => userIdsWithRole.Contains(a.UserId) && 
                                                  removedPermissions.Contains(a.ModuleName),
                            cancellationToken);

                    if (approver != null)
                    {
                        return Result<Unit>.Failure(UserRoleErrors.CannotUntag(approver.ModuleName));
                    }
                }
                
                
                string changeMessage = "User Role is unchanged"; //default message
                if (existingUserRole.Permissions != null && request.Permissions != null)
                {
                    changeMessage = existingUserRole.Permissions.Count < request.Permissions.Count 
                        ? "User Role has been successfully tagged" 
                        : "User Role has been successfully untagged";
                }
                else if (request.Permissions != null)
                {
                    // When existingUserRole.Permissions is null, but request.Permissions is not null
                    changeMessage = "User Role has been successfully tagged";
                }

                var result = Result<Unit>.Success(Unit.Value, changeMessage);
               
                existingUserRole.Permissions = request.Permissions;
                existingUserRole.UpdatedAt = DateTime.Now;
                
                _context.UserRoles.Update(existingUserRole);
                await _context.SaveChangesAsync(cancellationToken);

                return result;
            }
        }
    }
    
    [HttpPut("UntagAndTagUserRole/{id:int}")]
    public async Task<IActionResult> UntagUserRolePermission([FromRoute] int id,
        [FromBody] UntagAndTagUserRoleCommand command)
    {
        try
        {
            command.UserRoleId = id;
            command.ModifiedBy = User.Identity?.Name;
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
}