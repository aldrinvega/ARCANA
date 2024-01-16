using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Cluster;

[Route("api/Cluster"), ApiController]

public class GetAllCluster : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllCluster(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllCluster")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllClusterAsync query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
                
                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim != null)
                {
                    query.RoleName = roleClaim.Value;
                }
            }
            var cluster = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                cluster.CurrentPage,
                cluster.PageSize,
                cluster.TotalCount,
                cluster.TotalPages,
                cluster.HasPreviousPage,
                cluster.HasNextPage
            );
            var results = new
            {
                cluster,
                cluster.CurrentPage,
                cluster.PageSize,
                cluster.TotalCount,
                cluster.TotalPages,
                cluster.HasPreviousPage,
                cluster.HasNextPage
            };

            var successResult = Result.Success(results);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllClusterAsync : UserParams, IRequest<PagedList<GetAllClusterResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public int AccessBy { get; set; }
        public string RoleName { get; set; }
        public bool? IsInUse { get; set; }
        public int? UserId { get; set; }
        public string ModuleName { get; set; }
    }

    public class GetAllClusterResult
    {
        public int Id { get; set; }
        public string Cluster { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public int? UserId { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        
    }

    public class Handler : IRequestHandler<GetAllClusterAsync, PagedList<GetAllClusterResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllClusterResult>> Handle(GetAllClusterAsync request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Cluster> cluster = _context.Clusters
                .Include(user => user.User)
                .ThenInclude(role => role.UserRoles);

            if (!string.IsNullOrEmpty(request.Search))
            {
                cluster = cluster.Where(x => x.ClusterType.Contains(request.Search));
            }

            if (request.Status != null)
            {
                cluster = cluster.Where(status => status.IsActive == request.Status);
            }

            if (request.IsInUse != null)
            {
                    cluster = request.IsInUse == true ? 
                    cluster.Where(cl => cl.UserId != null && cl.User.IsActive) : 
                    cluster.Where(cl => cl.UserId == null || (cl.UserId != null && !cl.User.IsActive));
            }

            if (!string.IsNullOrWhiteSpace(request.ModuleName) && 
                request.ModuleName is Modules.Registration && 
                request.RoleName != Roles.Admin)
            {
                cluster = cluster.Where(x => x.UserId == request.AccessBy);
            }

            if (request.RoleName == Roles.Admin)
            {
                var forAdmin = cluster.Select(x => new GetAllClusterResult
                {
                    Id = x.Id,
                    Cluster = x.ClusterType,
                    CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                    UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                    IsActive = x.IsActive,
                    UserId = x.UserId,
                    Fullname = x.User.Fullname,
                    Role = x.User.UserRoles.UserRoleName
                });
                
                return await PagedList<GetAllClusterResult>.CreateAsync(forAdmin, request.PageNumber, request.PageSize);
            }
            

            var result = cluster.Select(x => new GetAllClusterResult
            {
                Id = x.Id,
                Cluster = x.ClusterType,
                CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                IsActive = x.IsActive,
                UserId = x.UserId,
                Fullname = x.User.Fullname,
                Role = x.User.UserRoles.UserRoleName
            });
            
            return await PagedList<GetAllClusterResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}