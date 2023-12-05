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

    public class UpdateUserRoleCommand : IRequest<Result>
    {
        public int UserRoleId { get; set; }
        public string RoleName { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserRoleCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (existingUserRole is null)
            {
                return UserRoleErrors.NotFound();
            }

            var validateRoleName =
                await _context.UserRoles.Where(x => x.UserRoleName == request.RoleName).FirstOrDefaultAsync(cancellationToken);

            if(validateRoleName is null)
            {
                existingUserRole.UserRoleName = request.RoleName;
                existingUserRole.UpdatedAt = DateTime.Now;
    
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
            

            if (validateRoleName.UserRoleName == request.RoleName && validateRoleName.Id != request.UserRoleId)
            {
                return UserRoleErrors.AlreadyExist(request.RoleName);
            }

            return Result.Success();
        }
    }
    
    [HttpPut("UpdateUserRole/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody]UpdateUserRoleCommand command)
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