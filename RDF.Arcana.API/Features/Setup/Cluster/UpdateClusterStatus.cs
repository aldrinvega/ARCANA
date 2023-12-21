using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Cluster;

[Route("api/Cluster"), ApiController]

public class UpdateClusterStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateClusterStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateClusterStatus/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateClusterStatusCommand
            {
                ClusterId = id
            };
            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }
    public class UpdateClusterStatusCommand : IRequest<Result>
    {
        public int ClusterId { get; set; }
    }
    public class Handler : IRequestHandler<UpdateClusterStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateClusterStatusCommand request, CancellationToken cancellationToken)
        {
            
            //Validate if the Cluster is existing
            var existingCluster = await _context.Clusters.FirstOrDefaultAsync(cluster => cluster.Id == request.ClusterId,
                    cancellationToken);

            if (existingCluster is null)
            {
                return ClusterErrors.NotFound();
            }

            //Validate if the cluster is already used, if already used cannot archive
            var canArchive = await _context.Clients.FirstOrDefaultAsync(client => client.Cluster == request.ClusterId,
                    cancellationToken);

            if (canArchive is not null)
            {
                return ClusterErrors.InUse();
            }
            
            existingCluster.IsActive = !existingCluster.IsActive;
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}