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

    public class UntagAndTagUserRoleCommand : IRequest<Unit>
    {
        public int UserRoleId { get; set; }
        public ICollection<int> ModuleId { get; set; }
        public string ModifiedBy { get; set; }

        public class Handler : IRequestHandler<UntagAndTagUserRoleCommand, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UntagAndTagUserRoleCommand request, CancellationToken cancellationToken)
            {
                var existingUseRole = await _context.UserRoles
                    .FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

                if (existingUseRole is null)
                {
                    throw new UserRoleNotFoundException();
                }

                existingUseRole.ModuleId = request.ModuleId;
                existingUseRole.UpdatedAt = DateTime.Now;
                
                _context.UserRoles.Update(existingUseRole);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
    
    [HttpPut("UntagAndTagUserRole/{id:int}")]
    public async Task<IActionResult> UntagUserRolePermission([FromRoute] int id,
        [FromBody] UntagAndTagUserRoleCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserRoleId = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("User Role has been successfully untagged");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}