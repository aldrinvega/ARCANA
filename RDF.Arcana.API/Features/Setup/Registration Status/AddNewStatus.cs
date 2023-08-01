using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Registration_Status;

[Route("api/Status")]
[ApiController]

public class AddNewStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewStatusCommand : IRequest<Unit>
    {
        public string Status { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewStatusCommand request, CancellationToken cancellationToken)
        {
            var existingStatus =
                await _context.Status.FirstOrDefaultAsync(x => x.StatusName == request.Status, cancellationToken);

            if (existingStatus is not null)
            {
                throw new Exception($"Status {request.Status} is already exist");
            }

            var status = new Status
            {
                StatusName = request.Status,
                CreatedAt = DateTime.Now
            };

            await _context.Status.AddAsync(status, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    [HttpPost("AddNewStatus")]
    public async Task<IActionResult> AddStatus(AddNewStatusCommand command)
    {
        var response = new QueryOrCommandResult<object>();

        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add($"{command.Status} is added successfully");
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}