using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.All;

public class GetAllClientsByCluster : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllClientsByCluster(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllClientsByCluster")]
    public async Task<IActionResult> Get([FromQuery] GetAllClientsByClusterQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;
            }
            var clients = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                clients.CurrentPage,
                clients.PageSize,
                clients.TotalCount,
                clients.TotalPages,
                clients.HasPreviousPage,
                clients.HasNextPage
                );

            var result = new
            {
                clients,
                clients.CurrentPage,
                clients.PageSize,
                clients.TotalCount,
                clients.TotalPages,
                clients.HasPreviousPage,
                clients.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllClientsByClusterQuery : UserParams, IRequest<PagedList<GetAllClientsByClusterResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public int AccessBy { get; set; }
    }

    public class GetAllClientsByClusterResult
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string OwnersName { get; set; }
    }

    public class Handler : IRequestHandler<GetAllClientsByClusterQuery, PagedList<GetAllClientsByClusterResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllClientsByClusterResult>> Handle(GetAllClientsByClusterQuery request, CancellationToken cancellationToken)
        {
            IQueryable <Domain.Clients> clients = _context.Clients
                .Include(x => x.AddedByUser)
                .ThenInclude(x => x.CdoCluster);

            if (!string.IsNullOrEmpty(request.Search))
            {
                clients = clients.Where(c => 
                    c.BusinessName.Contains(request.Search) || 
                    c.Fullname.Contains(request.Search));
            }

            if (request.Status != null)
            {
                clients = clients.Where(x => x.IsActive);
            }

            var user = await _context.Users.Include(user => user.CdoCluster).FirstOrDefaultAsync(x => x.Id == request.AccessBy, cancellationToken);
            var userClusters = user?.CdoCluster?.Select(cluster => cluster.ClusterId);
            
             clients = clients
                .Where(x => userClusters != null && (userClusters.Contains(x.ClusterId.Value)) && x.RegistrationStatus == Status.Approved);

             var result = clients.Select(c => new GetAllClientsByClusterResult
             {
                 Id = c.Id,
                 BusinessName = c.BusinessName,
                 OwnersName = c.Fullname
             });

             return await PagedList<GetAllClientsByClusterResult>.CreateAsync(
                 result, 
                 request.PageNumber,
                 request.PageSize);
        }
    }
}