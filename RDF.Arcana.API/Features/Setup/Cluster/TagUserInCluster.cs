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
            var existingCluster = await _context.Clusters
                .FirstOrDefaultAsync(ct =>
                ct.Id == request.ClusterId && ct.IsActive, cancellationToken);

            if (existingCluster is null)
            {
                return ClusterErrors.NotFound();
            }


            //Validate the users that will be tagged to the cluster is existing

            var validateUser = await _context.Users.FirstOrDefaultAsync(user =>
                    user.Id == request.UserId,
                cancellationToken: cancellationToken);

            if (validateUser is null)
            {
                return UserErrors.NotFound();
            }

            //Validate the users if already tagged to the cluster
            var existingTaggedUser = await _context.CdoClusters
                .FirstOrDefaultAsync(ct =>
                    ct.ClusterId == request.ClusterId &&
                    ct.UserId == request.UserId,
                cancellationToken);

            if (existingTaggedUser is not null)
            {
                return ClusterErrors.AlreadyTagged();
            }

            //Add cluster per user

            var taggedUsers = new CdoCluster
            {
                Id = request.ClusterId,
                UserId = request.UserId
            };

            await _context.CdoClusters.AddAsync(taggedUsers, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}