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

    public class UpdateUserStatusCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
    }

    public class Handler : IRequestHandler<UpdateUserStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

            if (existingUser is null)
                throw new NoUserFoundException();

            existingUser.IsActive = !existingUser.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPatch("UpdateUserStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateUserStatusCommand
            {
                UserId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Messages.Add("User status has been updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}
