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
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateUserRoleStatusCommand
            {
                UserRoleId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("User Role has been updated successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    public class UpdateUserRoleStatusCommand : IRequest<Unit>
    {
        public int UserRoleId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserRoleStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserRoleStatusCommand request, CancellationToken cancellationToken)
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
                throw new Exception(
                    $"Operation failed: User Role cannot be archived because it is currently associated with the user '{existingUserRole.Users.FirstOrDefault()?.Fullname}'.");
            }

            existingUserRole.IsActive = !existingUserRole.IsActive;
            existingUserRole.ModifiedBy = request.ModifiedBy;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}