using System.Security.Claims;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Users.Exception;
using RDF.Arcana.API.Features.Users.Exceptions;

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
           
            return Ok( result);
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
                    CompanyId = command.CompanyId,
                    LocationId = command.LocationId,
                    DepartmentId = command.DepartmentId,
                    UserRolesId = command.UserRoleId,
                    IsActive = true,
                    ProfilePicture = URL.ProfilePicture,
                };

                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}