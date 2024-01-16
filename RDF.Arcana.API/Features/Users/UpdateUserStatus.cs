using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Users.Exceptions;

namespace RDF.Arcana.API.Features.Users;

[Route("api/User")]
[ApiController]

public class UpdateUserStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateUserStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateUserStatusCommand : IRequest<Result>
    {
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (existingUser is null)
                return UserErrors.NotFound();

            existingUser.IsActive = !existingUser.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPatch("UpdateUserStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateUserStatusCommand
            {
                UserId = id,
                ModifiedBy = User.Identity?.Name
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
}