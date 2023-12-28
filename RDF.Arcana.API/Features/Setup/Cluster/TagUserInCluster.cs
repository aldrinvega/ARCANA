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
        
        public ICollection<UserClusterToTagged> Clusters { get; set; }
        public class UserClusterToTagged
        {
            public int ClusterId { get; set; }
            public int UserId { get; set; }
        }
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
            //Validate if the clusters are existing
            foreach (var cluster in request.Clusters)
            {
                var existingCluster = await _context.Clusters.FirstOrDefaultAsync(ct =>
                    ct.Id == cluster.ClusterId && ct.IsActive, cancellationToken);

                if (existingCluster is null)
                {
                    return ClusterErrors.NotFound();
                }
            }
            
            //Validate the users that will be tagged to the cluster is existing
            foreach (var cluster in request.Clusters)
            {
                var validateUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == cluster.UserId, cancellationToken: cancellationToken);

                if (validateUser is null)
                {
                    return UserErrors.NotFound();
                }
            }
            
            //Validate the users if already tagged to the cluster
            foreach (var cluster in request.Clusters)
            {
                var existingTaggedUser = await _context.CdoClusters.FirstOrDefaultAsync(
                    ct => ct.ClusterId == cluster.ClusterId && ct.UserId == cluster.UserId, cancellationToken);

                if (existingTaggedUser is not null)
                {
                    return ClusterErrors.AlreadyTagged();
                }
            }

            //Add multiple cluster per user
            foreach (var cluster in request.Clusters)
            {
                var taggedUsers = new CdoCluster
                {
                    ClusterId = cluster.ClusterId,
                    UserId = cluster.UserId
                };
                
                await _context.CdoClusters.AddAsync(taggedUsers, cancellationToken);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}