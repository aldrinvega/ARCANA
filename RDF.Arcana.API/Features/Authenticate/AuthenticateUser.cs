using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Authenticate;

public abstract class AuthenticateUser
{
    public sealed record AuthenticateUserQuery : IRequest<Result>
    {
        public AuthenticateUserQuery(string username)
        {
            Username = username;
        }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }

    public class AuthenticateUserResult
    {
        public AuthenticateUserResult(User user, string token)
        {
            Id = user.Id;
            Fullname = user.Fullname;
            Username = user.Username;
            Token = token;
            RoleName = user.UserRoles?.UserRoleName;
            Permission = user.UserRoles?.Permissions;
            IsPasswordChanged = user.IsPasswordChanged;
            ProfilePicture = user.ProfilePicture;
        }

        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string RoleName { get; set; }
        public ICollection<string> Permission { get; set; }
        public string Token { get; set; }
        public bool IsPasswordChanged { get; set; }
        public string ProfilePicture { get; set; }
    }

    public class Handler : IRequestHandler<AuthenticateUserQuery, Result>
    {
        private readonly IConfiguration _configuration;
        private readonly ArcanaDbContext _context;
        private readonly IMapper _mapper;

        public Handler(ArcanaDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Result> Handle(AuthenticateUserQuery command,
            CancellationToken cancellationToken)
        {

            var user = await _context.Users
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Username == command.Username, cancellationToken);

            //Verify if the credentials is correct
            if (user == null || !BCrypt.Net.BCrypt.Verify(command.Password, user.Password))
            {
                return AuthenticateUserErrors.UsernamePasswordIncorrect();
            }

            if (!user.IsActive)
            {
                return AuthenticateUserErrors.UnauthorizedAccess();
            }

            if (user.UserRolesId is null )
            {
                return AuthenticateUserErrors.NoRole();
            }

            await _context.SaveChangesAsync(cancellationToken);

            var token = GenerateJwtToken(user);

            var results = user.ToGetAuthenticatedUserResult(token);

            return Result.Success(results);
        }

        private string GenerateJwtToken(User user)
        {
            var key = _configuration.GetValue<string>("JwtConfig:Key");
            var audience = _configuration.GetValue<string>("JwtConfig:Audience");
            var issuer = _configuration.GetValue<string>("JwtConfig:Issuer");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Fullname),
                    new Claim(ClaimTypes.Role, user.UserRoles.UserRoleName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}