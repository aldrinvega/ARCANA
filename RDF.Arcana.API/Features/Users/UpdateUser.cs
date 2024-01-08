using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Cluster;
using RDF.Arcana.API.Features.Setup.UserRoles;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User")]
[ApiController]
public class UpdateUser : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPut("UpdateUser/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserCommand command)
    {
        try
        {
            command.UserId = id;
            command.ModifiedBy = User.Identity?.Name;
           var result = await _mediator.Send(command);
           if (result.IsFailure)
           {
               return BadRequest(result);
           }
            return Ok(result);
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class UpdateUserCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string FullIdNo { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string ModifiedBy { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public ICollection<CdoCluster> Clusters { get; set; }
        public int? UserRoleId { get; set; }

        public class CdoCluster
        {
            public int ClusterId { get; set; }
        }
    }

    public class Handler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(user => user.CdoCluster)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            /*var validateCompany =
                await _context.Companies.FirstOrDefaultAsync(x => x.Id == request.CompanyId, cancellationToken);
            var validateDepartment =
                await _context.Departments.FirstOrDefaultAsync(x => x.Id == request.DepartmentId, cancellationToken);
            var validateLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);*/
            var validateUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);

            if (user.Username != request.Username)
            {
                var existingUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
                if (existingUser != null)
                {
                    return UserErrors.UserAlreadyExist();
                }
            }

            /*if (validateCompany is null)
                throw new NoCompanyFoundException();
            if (validateDepartment is null)
                throw new NoDepartmentFoundException();
            if (validateLocation is null)
                throw new NoLocationFoundException();*/
            if (validateUserRole is null)
                UserRoleErrors.NotFound();

            user.FullIdNo = request.FullIdNo;
            user.Fullname = request.Fullname;
            user.Username = request.Username;
            /*user.CompanyId = request.CompanyId;
            user.LocationId = request.LocationId;
            user.DepartmentId = request.DepartmentId;*/
            user.UserRolesId = request.UserRoleId;
            user.UpdatedAt = DateTime.Now;
            
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
             var validateUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken: cancellationToken);

              if (validateUser is null)
              {
                  return UserErrors.NotFound();
              }


              // Remove clusters that are not present in the request body
              var existingClusters = user.CdoCluster.Select(x => x.ClusterId).ToList();
              var clustersToRemove = existingClusters.Except(request.Clusters.Select(c => c.ClusterId)).ToList();

              foreach (var clusterIdToRemove in clustersToRemove)
              {
                  var forRemove = user.CdoCluster.FirstOrDefault(x => x.ClusterId == clusterIdToRemove);
                  if (forRemove != null)
                  {
                      user.CdoCluster.Remove(forRemove);
                  }
              }
              
            //Validate the users if already tagged to the cluster
            foreach (var cluster in request.Clusters)
            {
                var existingTaggedUser = await _context.CdoClusters.FirstOrDefaultAsync(
                    ct =>  ct.UserId == user.Id && ct.ClusterId == cluster.ClusterId, cancellationToken);

                if (existingTaggedUser is not null)
                {
                    existingTaggedUser.ClusterId = cluster.ClusterId;
                }
                else
                {
                    var userCluster = new CdoCluster
                    {
                        ClusterId = cluster.ClusterId,
                        UserId = user.Id
                    };
                    
                    await _context.CdoClusters.AddAsync(userCluster, cancellationToken);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}