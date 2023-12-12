using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Users;

namespace RDF.Arcana.API.Features.Setup.Cluster;
[Route("api/Cluster"), ApiController]

public class TagUserInCluster : ControllerBase
{

    private readonly IMediator _mediator;

    public TagUserInCluster(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("TagUserInCluster")]
    public async Task<IActionResult> TagUser(TagUserInClusterCommand command)
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

    public class TagUserInClusterCommand : IRequest<Result>
    {
        public int ClusterId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<TagUserInClusterCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(TagUserInClusterCommand request, CancellationToken cancellationToken)
        {
            var existingCluster = await _context.Clusters.FirstOrDefaultAsync(cluster =>
                cluster.Id == request.ClusterId, cancellationToken);

            if (existingCluster is null)
            {
                return ClusterErrors.NotFound();
            }

            var validateUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

            if (validateUser is null)
            {
                return UserErrors.NotFound();
            }

            var existingTaggedUser = await _context.CdoClusters.FirstOrDefaultAsync(
                cluster => cluster.Id == request.ClusterId && cluster.UserId == request.UserId, cancellationToken);

            if (existingTaggedUser is not null )
            {
                return ClusterErrors.AlreadyExist();
            }

            var taggedUsers = new CdoCluster
            {
                ClusterId = request.ClusterId,
                UserId = request.ClusterId
            };

            await _context.CdoClusters.AddAsync(taggedUsers, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}