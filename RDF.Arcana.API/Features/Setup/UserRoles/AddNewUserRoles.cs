using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/UserRole")]
[ApiController]

public class AddNewUserRoles : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewUserRoles(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewUserRolesCommand : IRequest<Unit>
    {
        public string RoleName { get; set; }
        public List<string> Permission { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewUserRolesCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewUserRolesCommand request, CancellationToken cancellationToken)
        {
            var existingUserRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserRoleName == request.RoleName, cancellationToken);
            if (existingUserRole is not null)
            {
                throw new UserRoleAlreadyExistException();
            }

            var userRole = new Domain.UserRoles
            {
                UserRoleName = request.RoleName,
                Permissions = request.Permission,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.UserRoles.AddAsync(userRole, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewUserRole")]
    public async Task<IActionResult> Add(AddNewUserRolesCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("User Role has been added successfully");
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