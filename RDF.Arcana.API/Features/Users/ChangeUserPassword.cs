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

    public class ChangeUserPasswordCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string ModifiedBy { get; set; }
        public string Password { get; set; }
    }
    
    public class Handler : IRequestHandler<ChangeUserPasswordCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NoUserFoundException();
            }

            if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.Password))
            {
                throw new System.Exception("Old password is not correct");
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    [HttpPatch("ChangeUserPassword/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChangeUserPasswordCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.UserId = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Messages.Add("Password has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}