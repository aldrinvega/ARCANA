using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User"), ApiController]

public class ResetPassword : ControllerBase
{

    private readonly IMediator _mediator;

    public ResetPassword(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class ResetPasswordCommand : IRequest<Result>
    {
        public int UserId { get; set; }
    }

    [HttpPatch("ResetPassword/{id:int}")]
    public async Task<IActionResult> Reset([FromRoute] int id)
    {
        try
        {
            var command = new ResetPasswordCommand
            {
                UserId = id
            };

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public class Handler : IRequestHandler<ResetPasswordCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (existingUser is null)
            {
                return UserErrors.NotFound();
            }

            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(existingUser.Username);
            existingUser.IsPasswordChanged = false;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}