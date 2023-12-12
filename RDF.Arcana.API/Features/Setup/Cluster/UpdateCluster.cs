using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Cluster;
[Route("api/Cluster"), ApiController]

public class UpdateCluster : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateCluster(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateCluster/{id:int}")]
    public async Task<IActionResult> Update([FromBody]UpdateClusterCommand command, [FromRoute]int id)
    {
        try
        {
            command.ClusterId = id;
            var result = await _mediator.Send(command);
            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateClusterCommand : IRequest<Result>
    {
        public int ClusterId { get; set; }
        public string Cluster { get; set; }
    }
    public class Handler : IRequestHandler<UpdateClusterCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateClusterCommand request, CancellationToken cancellationToken)
        {
            var existingCluster = await _context.Clusters.FirstOrDefaultAsync(x => x.Id == request.ClusterId, cancellationToken);

            if ( existingCluster is null)
            {
                return ClusterErrors.NotFound();
            }

            existingCluster.ClusterType = request.Cluster;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}