using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/UserRole")]
[ApiController]
public class UpdateUserRoleStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUserRoleStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateUserRoleStatus/{id:int}")]
    public async Task<IActionResult> UpdateUserRole([FromRoute] int id)
    {
        try
        {
            var command = new UpdateUserRoleStatusCommand
            {
                UserRoleId = id,
                ModifiedBy = User.Identity?.Name
            };
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

    public class UpdateUserRoleStatusCommand : IRequest<Result>
    {
        public int UserRoleId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserRoleStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateUserRoleStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole =
                await _context.UserRoles
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (existingUserRole is null)
            {
                throw new UserRoleNotFoundException();
            }

            if (existingUserRole.IsActive && existingUserRole.Permissions.Count == 1)
            {
                throw new UserRoleDeactivationException();
            }

            if (existingUserRole.IsActive && existingUserRole.Users != null && existingUserRole.IsActive)
            {
                return UserRoleErrors.CannotArchive(existingUserRole.Users.FirstOrDefault()?.Fullname);
            }

            existingUserRole.IsActive = !existingUserRole.IsActive;
            existingUserRole.ModifiedBy = request.ModifiedBy;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}