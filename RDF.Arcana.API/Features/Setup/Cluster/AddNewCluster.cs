using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Setup.Cluster;

[Route("api/Cluster"), ApiController]

public class AddNewCluster : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewCluster(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewCluster")]
    public async Task<IActionResult> Add([FromBody] AddNewClusterCommand command)
    {
        try
        {
            var cluster = await _mediator.Send(command);

            return cluster.IsFailure ? BadRequest(cluster) : Ok(cluster);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class AddNewClusterCommand : IRequest<Result>
    {
        public string Cluster { get; set; }
    }

    public class Handler : IRequestHandler<AddNewClusterCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewClusterCommand request, CancellationToken cancellationToken)
        {
            var existingUserInCluster = await _context.Clusters.FirstOrDefaultAsync(user => user.ClusterType == request.Cluster, cancellationToken);

            if (existingUserInCluster != null)
            {
                return ClusterErrors.AlreadyExist();
            }

            var cluster = new Domain.Cluster
            {
                ClusterType = request.Cluster
            };

            await _context.Clusters.AddAsync(cluster, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}