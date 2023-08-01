using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

[Route("api/Department")]
[ApiController]

public class UpdateDepartmentStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDepartmentStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateDepartmentStatusCommand : IRequest<Unit>
    {
        public int DepartmentId { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateDepartmentStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateDepartmentStatusCommand request, CancellationToken cancellationToken)
        {
            var validateDepartment =
                await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
            if (validateDepartment is null)
            {
                throw new NoDepartmentFoundException();
            }

            validateDepartment.IsActive = !validateDepartment.IsActive;
            validateDepartment.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateDepartmentStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateDepartmentStatusCommand
            {
                DepartmentId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Status successfully updated");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}