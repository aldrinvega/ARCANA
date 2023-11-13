using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

[Route("api/Department")]
[ApiController]
public class AddNewDepartment : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewDepartment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewDepartment")]
    public async Task<IActionResult> AddDepartment(AddNewDepartmentCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add($"Department {command.DepartmentName} successfully added");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    public class AddNewDepartmentCommand : IRequest<Unit>
    {
        public string DepartmentName { get; set; }
        public int AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<AddNewDepartmentCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewDepartmentCommand request, CancellationToken cancellationToken)
        {
            var validateDepartment = await _context.Departments.SingleOrDefaultAsync(
                x => x.DepartmentName == request.DepartmentName,
                cancellationToken);


            if (validateDepartment is not null)
            {
                throw new DepartmentAlreadyExistException(request.DepartmentName);
            }

            var department = new Domain.Department
            {
                DepartmentName = request.DepartmentName,
                AddedBy = request.AddedBy,
                IsActive = true
            };

            await _context.Departments.AddAsync(department, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}