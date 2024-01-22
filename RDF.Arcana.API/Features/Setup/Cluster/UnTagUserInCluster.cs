using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Cluster;

[Route("api/Cluster"), ApiController]

public class UnTagUserInCluster : ControllerBase
{
    private readonly IMediator _mediator;

    public UnTagUserInCluster(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("UntagUserInCluster/{id}")]
    public async Task<IActionResult> UntagUser([FromRoute] int id, [FromQuery] int clusterId)
    {
        try
        {
            var command = new UntagUserInClusterCommand
            {
                ClusterId = clusterId,
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UntagUserInClusterCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public int ClusterId { get; set; }
    }
    
    public class Handler : IRequestHandler<UntagUserInClusterCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UntagUserInClusterCommand request, CancellationToken cancellationToken)
        {
            var validateUserInCluster = await _context.CdoClusters.FirstOrDefaultAsync(cluster =>
                cluster.ClusterId == request.ClusterId && 
                cluster.UserId == request.UserId, cancellationToken);

            if (validateUserInCluster is  null)
            {
                return ClusterErrors.NotFound();
            }
            _context.Remove(validateUserInCluster);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}