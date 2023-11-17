using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.UserRoles.Exceptions;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User")]
[ApiController]
public class UpdateUser : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateUser/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserId = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("User has been updated successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    public class UpdateUserCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string FullIdNo { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string ModifiedBy { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public int? UserRoleId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            /*var validateCompany =
                await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);
            var validateDepartment =
                await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
            var validateLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);*/
            var validateUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (user.Username != request.Username)
            {
                var existingUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
                if (existingUser != null)
                {
                    throw new UsernameAlreadyExistException();
                }
            }

            /*if (validateCompany is null)
                throw new NoCompanyFoundException();
            if (validateDepartment is null)
                throw new NoDepartmentFoundException();
            if (validateLocation is null)
                throw new NoLocationFoundException();*/
            if (validateUserRole is null)
                throw new UserRoleNotFoundException();

            user.FullIdNo = request.FullIdNo;
            user.Fullname = request.Fullname;
            user.Username = request.Username;
            /*user.CompanyId = request.CompanyId;
            user.LocationId = request.LocationId;
            user.DepartmentId = request.DepartmentId;*/
            user.UserRolesId = request.UserRoleId;
            user.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}