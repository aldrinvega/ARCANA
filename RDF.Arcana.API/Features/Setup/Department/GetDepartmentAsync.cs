using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Department.Exception;

namespace RDF.Arcana.API.Features.Setup.Department;

[Route("api/Department")]
[ApiController]

public class GetDepartmentAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetDepartmentAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetDepartmentAsyncQuery : UserParams, IRequest<PagedList<GetDepartmentAsyncResult>>
    {
        public bool? Status { get; set; }
        public string Search { get; set; }
    }

    public class GetDepartmentAsyncResult
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public List<string> Users { get; set; }
    }
    public class Handler : IRequestHandler<GetDepartmentAsyncQuery, PagedList<GetDepartmentAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetDepartmentAsyncResult>> Handle(GetDepartmentAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Department> department = _context.Departments.Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                department = department
                    .Where(d => d.DepartmentName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                department = department
                    .Where(d => d.IsActive == request.Status);
            }

            var result = department.Select(d => d.ToGetAllDepartmentAsyncResult());

            return await PagedList<GetDepartmentAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }
    
    [HttpGet("GetAllDepartment", Name = "GetAllDepartment")]
    public async Task<IActionResult> GetAllDepartmentByStatus([FromQuery]
        GetDepartmentAsyncQuery command)
    {
        try
        {
            var department = await _mediator.Send(command);

            Response.AddPaginationHeader(
                department.CurrentPage,
                department.PageSize,
                department.TotalCount,
                department.TotalPages,
                department.HasPreviousPage,
                department.HasNextPage
            );

            var results = new QueryOrCommandResult<object>
            {
                Success = true,
                Data = new
                {
                    department,
                    department.CurrentPage,
                    department.PageSize,
                    department.TotalCount,
                    department.TotalPages,
                    department.HasPreviousPage,
                    department.HasNextPage
                }
            };
            return Ok(results);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }
}