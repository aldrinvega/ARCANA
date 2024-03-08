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
        public int? UserRoleId { get; set; }
        public int? ClusterId { get; set; }
        public string MobileNumber { get; set; }

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
                .Include(x => x.CdoCluster)
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            var validateUserRole =
                await _context.UserRoles.FirstOrDefaultAsync(x => x.Id == request.UserRoleId, cancellationToken);
            //Validate if the clusters are existing

            

            if (user.Username != request.Username)
            {
                var existingUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
                if (existingUser != null)
                {
                    return UserErrors.UserAlreadyExist();
                }
            }
            
            if (validateUserRole is null)
                UserRoleErrors.NotFound();

            user.FullIdNo = request.FullIdNo;
            user.Fullname = request.Fullname;
            user.Username = request.Username;
            user.UserRolesId = request.UserRoleId;
            user.UpdatedAt = DateTime.Now;
            user.MobileNumber = request.MobileNumber;

            if(request.ClusterId != null)
            {
                var existingCluster = await _context.Clusters.FirstOrDefaultAsync(ct =>
                ct.Id == request.ClusterId && ct.IsActive, cancellationToken);

                if (existingCluster is null)
                {
                    return ClusterErrors.NotFound();
                }

                var cdoCluster =
                await _context.CdoClusters.FirstOrDefaultAsync(
                    cl => cl.ClusterId == request.ClusterId && cl.UserId == user.Id, cancellationToken);

                if (cdoCluster != null)
                {
                    user.CdoCluster.UserId = user.Id;
                }
                else
                {
                    var newCdoCluster = new CdoCluster
                    {
                        ClusterId = request.ClusterId.Value,
                        UserId = user.Id
                    };

                    await _context.CdoClusters.AddAsync(newCdoCluster, cancellationToken);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}