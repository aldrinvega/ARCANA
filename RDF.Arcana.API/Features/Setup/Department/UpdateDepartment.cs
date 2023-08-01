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

    public class UpdateDepartmentCommand : IRequest<Unit>
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ModifiedBy { get; set; }
    }
     
     public class Handler : IRequestHandler<UpdateDepartmentCommand, Unit>
     {
         private readonly DataContext _context;

         public Handler(DataContext context)
         {
             _context = context;
         }

         public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
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
                 return Unit.Value;

             }
            
             if (validateDepartmentName.DepartmentName == request.DepartmentName && validateDepartmentName.Id == request.DepartmentId)
             {
                 throw new System.Exception("No changes");
             }
            
             if (validateDepartmentName.DepartmentName == request.DepartmentName && validateDepartmentName.Id != request.DepartmentId)
             {
                 throw new DepartmentAlreadyExistException(request.DepartmentName);
             }
             
             return Unit.Value;
         }
     }
     
     [HttpPut("UpdateDepartment/{id:int}")]
     public async Task<IActionResult> Update(UpdateDepartmentCommand command, [FromRoute]int id)
     {
         var response = new QueryOrCommandResult<object>();
         try
         {
             command.DepartmentId = id;
             command.ModifiedBy = User.Identity?.Name;
             await _mediator.Send(command);
             response.Success = true;
             response.Messages.Add("Department successfully updated");
             return Ok(response);
         }
         catch (System.Exception e)
         {
             response.Messages.Add(e.Message);
             return Conflict(response);
         }
     }
}