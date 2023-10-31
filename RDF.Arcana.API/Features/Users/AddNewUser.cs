using System.Security.Claims;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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


    [HttpPost]
    [Route("AddNewUser")]
    public async Task<ActionResult> Add([FromBody] AddNewUserCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            response.Success = true;
            response.Data = result;
            response.Messages.Add("User added successfully");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Success = false;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    public class AddNewUserCommand : IRequest<Unit>
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
        public IFormFile ProfilePicture { get; set; }


        public class Handler : IRequestHandler<AddNewUserCommand, Unit>
        {
            private readonly Cloudinary _cloudinary;
            private readonly DataContext _context;

            public Handler(DataContext context, IOptions<CloudinarySettings> config)
            {
                _context = context;
                var account = new Account(
                    config.Value.Cloudname,
                    config.Value.ApiKey,
                    config.Value.ApiSecret
                );

                _cloudinary = new Cloudinary(account);
            }

            public async Task<Unit> Handle(AddNewUserCommand command, CancellationToken cancellationToken)
            {
                // Check if the username already exists
                var existingUserWithSameUsername =
                    await _context.Users.FirstOrDefaultAsync(x => x.Username == command.Username, cancellationToken);
                if (existingUserWithSameUsername != null)
                {
                    throw new UserAlreadyExistWithUsername(command.Username);
                }

                // Check if the fullname and fullidno already exist together
                var existingUserWithSameFullnameAndId = await _context.Users
                    .FirstOrDefaultAsync(x => x.FullIdNo == command.FullIdNo && x.Fullname == command.Fullname,
                        cancellationToken);
                if (existingUserWithSameFullnameAndId != null)
                {
                    throw new UserAlreadyExistException();
                }

                await using var stream = command.ProfilePicture.OpenReadStream();

                var attachmentParams = new ImageUploadParams
                {
                    File = new FileDescription(command.ProfilePicture.FileName, stream),
                    PublicId = $"{command.Username}/{command.ProfilePicture.FileName}"
                };

                var attachmentResult = await _cloudinary.UploadAsync(attachmentParams);

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
                    ProfilePicture = attachmentResult.SecureUrl.ToString()
                };
                await _context.Users.AddAsync(user, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}