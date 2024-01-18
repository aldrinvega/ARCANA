using System.Security.Claims;
using CloudinaryDotNet; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.Cluster;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User")]
[ApiController]
public class AddNewUser : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewUser(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("AddNewUser")]
    public async Task<ActionResult> Add([FromBody] AddNewUserCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

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

    public class AddNewUserCommand : IRequest<Result>
    {
        public string FullIdNo { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int AddedBy { get; set; }
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
        public int? UserRoleId { get; set; }
        public int? CompanyId { get; set; }
        public string ProfilePicture { get; set; }
        public int? ClusterId { get; set; }


        public class Handler : IRequestHandler<AddNewUserCommand, Result>
        {
            private readonly Cloudinary _cloudinary;
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context, IOptions<CloudinaryOptions> config)
            {
                _context = context;
                var account = new Account(
                    config.Value.Cloudname,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );

                _cloudinary = new Cloudinary(account);
            }

            public async Task<Result> Handle(AddNewUserCommand command, CancellationToken cancellationToken)
            {
                // Check if the username already exists
                var existingUserWithSameUsername =
                    await _context.Users.FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);
                if (existingUserWithSameUsername != null)
                {
                    return UserErrors.UsernameAlreadyExist(command.Username);
                }

                // Check if the fullname and fullidno already exist together
                var existingUserWithSameFullnameAndId = await _context.Users
                    .FirstOrDefaultAsync(x => x.FullIdNo == command.FullIdNo && x.Fullname == command.Fullname,
                        cancellationToken);
                if (existingUserWithSameFullnameAndId != null)
                {
                    return UserErrors.UserAlreadyExist();
                }
                
                var user = new User
                {
                    FullIdNo = command.FullIdNo,
                    Fullname = command.Fullname,
                    Username = command.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                    AddedBy = command.AddedBy,
                    CompanyId = command.CompanyId,
                    LocationId = command.LocationId,
                    DepartmentId = command.DepartmentId,
                    UserRolesId = command.UserRoleId,
                    IsActive = true,
                    ProfilePicture = URL.ProfilePicture,
                };

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                if (command.ClusterId is not null)
                {
                    // Validate if the cluster exists
                    var existingCluster = await _context.Clusters.FirstOrDefaultAsync(ct =>
                        ct.Id == command.ClusterId && ct.IsActive, cancellationToken);

                    if (existingCluster is null)
                    {
                        return ClusterErrors.NotFound();
                    }

                    // Check if the new cluster is already in use
                    var alreadyUsed = await _context.Clusters
                        .AnyAsync(ct => (ct.UserId != null && ct.UserId != user.Id) && ct.Id == command.ClusterId,
                            cancellationToken);

                    if (alreadyUsed)
                    {
                        // If the requested cluster is already in use, return an error
                        return ClusterErrors.InUse();
                    }

                    // Validate the user that will be tagged to the cluster exists
                    var validateUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id,
                        cancellationToken: cancellationToken);

                    if (validateUser is null)
                    {
                        return UserErrors.NotFound();
                    }

                    // Check if the cluster is already tagged with the user
                    var existingTaggedUser = await _context.Clusters.FirstOrDefaultAsync(
                        ct => ct.Id == command.ClusterId && ct.UserId == user.Id, cancellationToken);

                    if (existingTaggedUser is not null)
                    {
                        return ClusterErrors.AlreadyTagged();
                    }

                    var cluster =
                        await _context.Clusters.FirstOrDefaultAsync(x => x.Id == command.ClusterId, cancellationToken);

                    if (cluster != null)
                    {
                        var validateUserStatus =
                            await _context.Users.FirstOrDefaultAsync(x => x.Id == cluster.UserId, cancellationToken);

                        if (validateUserStatus is null || validateUserStatus.IsActive == false)
                        {
                            // Update UserId only if it's null or the user is inactive
                            cluster.UserId = user.Id;

                            // If the user is inactive, update notifications
                            if (validateUserStatus?.IsActive == false)
                            {
                                var notifications = await _context.Notifications.Where(x => x.UserId == cluster.UserId)
                                    .ToListAsync(cancellationToken);

                                foreach (var notification in notifications)
                                {
                                    notification.UserId = user.Id; 

                                    await _context.Notifications.AddAsync(notification, cancellationToken);
                                }
                            }
                        }
                    }
                }


                await _context.SaveChangesAsync(cancellationToken);
                
                return Result.Success();
            }
        }
    }
}