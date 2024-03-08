using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User")]
[ApiController]
public class ChangeUserPassword : ControllerBase
{
    private readonly IMediator _mediator;

    public ChangeUserPassword(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("ChangeUserPassword/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChangeUserPasswordCommand command)
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

    public class ChangeUserPasswordCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string ModifiedBy { get; set; }
        public string NewPassword { get; set; }
    }

    public class Handler : IRequestHandler<ChangeUserPasswordCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                return UserErrors.NotFound();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                return UserErrors.OldPasswordIncorrect();
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.IsPasswordChanged = true;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}