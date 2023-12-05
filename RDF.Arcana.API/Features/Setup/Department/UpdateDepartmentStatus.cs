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

    public class UpdateDepartmentStatusCommand : IRequest<Result>
    {
        public int DepartmentId { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateDepartmentStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateDepartmentStatusCommand request, CancellationToken cancellationToken)
        {
            var validateDepartment =
                await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
            if (validateDepartment is null)
            {
                return DepartmentErrors.NotFound();
            }

            validateDepartment.IsActive = !validateDepartment.IsActive;
            validateDepartment.UpdatedAt = DateTime.Now;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateDepartmentStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        try
        {
            var command = new UpdateDepartmentStatusCommand
            {
                DepartmentId = id,
                ModifiedBy = User.Identity?.Name
            };
            var result = await _mediator.Send(command);
            if (result.IsFailure)
                return BadRequest(result);
            return Ok(result);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }
}