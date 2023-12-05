using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User")]
[ApiController]
public class GetUsersAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUsersAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetUser")]
    public async Task<IActionResult> Get([FromQuery] GetUsersAsync.GetUserAsyncQuery query)
    {
        try
        {
            var users = await _mediator.Send(query);

            Response.AddPaginationHeader(
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages,
                users.HasPreviousPage,
                users.HasNextPage
            );

            var result = new
            {
                users,
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages,
                users.HasPreviousPage,
                users.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class GetUserAsyncQuery : UserParams, IRequest<PagedList<GetUserAsyncQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetUserAsyncQueryResult
    {
        public int Id { get; set; }
        public string FullIdNo { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public string DepartmentName { get; set; }
        public string LocationName { get; set; }
        public string RoleName { get; set; }
        public ICollection<string> Permission { get; set; }
    }

    public class Handler : IRequestHandler<GetUserAsyncQuery, PagedList<GetUserAsyncQueryResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUserAsyncQueryResult>> Handle(GetUserAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<User> users = _context.Users
                .Include(x => x.AddedByUser)
                .Include(x => x.UserRoles)
                .Include(x => x.Department)
                .Include(x => x.Company)
                .Include(x => x.Location);

            if (!string.IsNullOrEmpty(request.Search))
            {
                users = users.Where(x => x.Fullname.Contains(request.Search));
            }

            if (request.Status != null)
            {
                users = users.Where(x => x.IsActive == request.Status);
            }

            var result = users.Select(x => x.ToGetUserAsyncQueryResult());

            return await PagedList<GetUserAsyncQueryResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}