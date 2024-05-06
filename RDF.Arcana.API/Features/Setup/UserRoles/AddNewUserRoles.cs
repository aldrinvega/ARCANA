using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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

    [AllowAnonymous]
    [HttpPost("AddNewUserRole")]
    public async Task<IActionResult> Add(AddNewUserRolesCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class AddNewUserRolesCommand : IRequest<Result>
    {
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }
        public int? AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewUserRolesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewUserRolesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUserRole = await _context.UserRoles
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.UserRoleName == request.RoleName, cancellationToken);

                if (existingUserRole is not null)
                {
                    return UserRoleErrors.AlreadyExist(request.RoleName);
                }

                var userRole = new Domain.UserRoles
                {
                    UserRoleName = request.RoleName,
                    Permissions = request.Permissions,
                    AddedBy = request.AddedBy,
                    IsActive = true
                };

                // Use the execution strategy for transaction handling
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

                    await _context.UserRoles.AddAsync(userRole, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                });

                return Result.Success();
            }
            catch (Exception)
            {
                return UserRoleErrors.AlreadyExist(request.RoleName);
            }
        }
    }
}
