using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
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
    }

    public class GetAllClusterResult
    {
        public int Id { get; set; }
        public string Cluster { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UsersCollection> Users { get; set; }

        public class UsersCollection
        {
            public int UserId { get; set; }
            public string Fullname { get; set; }
            public string Role { get; set; }
        }
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
                .Include(cluster => cluster.CdoClusters)
                .ThenInclude(user => user.User)
                .ThenInclude(role => role.UserRoles);

            if (!string.IsNullOrEmpty(request.Search))
            {
                cluster = cluster.Where(x => x.ClusterType.Contains(request.Search));
            }

            if (request.Status != null)
            {
                cluster = cluster.Where(status => status.IsActive == request.Status);
            }

            var result = cluster.Select(x => new GetAllClusterResult
            {
                Id = x.Id,
                Cluster = x.ClusterType,
                CreatedAt = x.CreatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                UpdatedAt = x.UpdatedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                IsActive = x.IsActive,
                Users = x.CdoClusters.Select(x => new GetAllClusterResult.UsersCollection
                {
                    UserId = x.UserId,
                    Fullname = x.User.Fullname,
                    Role = x.User.UserRoles.UserRoleName
                }).ToList()
            });


            return await PagedList<GetAllClusterResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}