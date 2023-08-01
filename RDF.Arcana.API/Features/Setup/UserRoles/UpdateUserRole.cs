using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/UserRole")]
[ApiController]

public class UpdateUserRole : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUserRole(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateUserRoleCommand : IRequest<Unit>
    {
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserRoleCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (existingUserRole is null)
            {
                throw new UserRoleNotFoundException();
            }

            var validateRoleName =
                await _context.UserRoles.Where(x => x.UserRoleName == request.RoleName).FirstOrDefaultAsync(cancellationToken);

            if(validateRoleName is null)
            {
                existingUserRole.UserRoleName = request.RoleName;
                existingUserRole.UpdatedAt = DateTime.Now;
    
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
    
            if (validateRoleName.UserRoleName == request.RoleName && validateRoleName.Id == request.UserRoleId)
            {
                throw new Exception("No changes");
            }

            if (validateRoleName.UserRoleName == request.RoleName && validateRoleName.Id != request.UserRoleId)
            {
                throw new Exception("Role already exists.");
            }

            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateUserRole/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateUserRoleCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserRoleId = id;
            command.ModifiedBy = User.Identity?.Name;
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
}