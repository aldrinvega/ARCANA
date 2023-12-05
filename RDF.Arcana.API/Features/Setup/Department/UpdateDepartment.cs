using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

[Route("api/Department")]
[ApiController]

public class UpdateDepartment : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateDepartment(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateDepartmentCommand : IRequest<Result>
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ModifiedBy { get; set; }
    }
     
     public class Handler : IRequestHandler<UpdateDepartmentCommand, Result>
     {
         private readonly ArcanaDbContext _context;

         public Handler(ArcanaDbContext context)
         {
             _context = context;
         }

         public async Task<Result> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
         {
             var validateDepartment =
                 await _context.Departments.FirstOrDefaultAsync(d => d.Id == request.DepartmentId,
                     cancellationToken);
             
             var validateDepartmentName =
                 await _context.Departments.Where(x => x.DepartmentName == request.DepartmentName).FirstOrDefaultAsync(cancellationToken);

             if (validateDepartmentName is null)
             {
                 validateDepartment.DepartmentName = request.DepartmentName;
                 validateDepartment.ModifiedBy = request.ModifiedBy;
                 validateDepartment.UpdatedAt = DateTime.Now;

                 await _context.SaveChangesAsync(cancellationToken);
                 return Result.Success();
             }
            
             if (validateDepartmentName.DepartmentName == request.DepartmentName && validateDepartmentName.Id != request.DepartmentId)
             {
                 return DepartmentErrors.AlreadyExist(request.DepartmentName);
             }

             return Result.Success();
         }
     }
     
     [HttpPut("UpdateDepartment/{id:int}")]
     public async Task<IActionResult> Update(UpdateDepartmentCommand command, [FromRoute]int id)
     {
         try
         {
             command.DepartmentId = id;
             command.ModifiedBy = User.Identity?.Name;
             var result = await _mediator.Send(command);
             if (result.IsFailure)
             {
                 return BadRequest(result);
             }
             return Ok(result);
         }
         catch (System.Exception e)
         {
             return BadRequest(e.Message);
         }
     }
}