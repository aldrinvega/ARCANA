using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.UserRoles;

[Route("api/UserRole")]
[ApiController]

public class GetUserRolesAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUserRolesAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetUserRoleAsyncQuery : UserParams, IRequest<PagedList<GetUserRoleAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetUserRoleAsyncResult
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<string> Permissions { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string User { get; set; }
    }
    
    public class Handler : IRequestHandler<GetUserRoleAsyncQuery, PagedList<GetUserRoleAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUserRoleAsyncResult>> Handle(GetUserRoleAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable <Domain.UserRoles> userRoles = _context.UserRoles.Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                userRoles = userRoles.Where(x => x.UserRoleName.Contains(request.Search));
            }

            if (request.Status is not null)
            {
                userRoles = userRoles.Where(x => x.IsActive == request.Status);
            }

            var result = userRoles.Select(x => x.ToGetUserRoleAsyncQueryResult());

            return await PagedList<GetUserRoleAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
    
    [HttpGet("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles([FromQuery]GetUserRolesAsync.GetUserRoleAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var userRoles = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                userRoles.CurrentPage,
                userRoles.PageSize,
                userRoles.TotalCount,
                userRoles.TotalPages,
                userRoles.HasPreviousPage,
                userRoles.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    userRoles,
                    userRoles.CurrentPage,
                    userRoles.PageSize,
                    userRoles.TotalCount,
                    userRoles.TotalPages,
                    userRoles.HasPreviousPage,
                    userRoles.HasNextPage
                }
            };
            
            response.Messages.Add("Successfully fetch data");
            return Ok(result);

        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}