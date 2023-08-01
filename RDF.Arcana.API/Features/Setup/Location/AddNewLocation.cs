using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Location.Exception;

namespace RDF.Arcana.API.Features.Setup.Location;

[Route("api/Location")]
[ApiController]

public class AddNewLocation : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewLocation(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewLocationCommand : IRequest<Unit>
    {
        public string LocationName { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewLocationCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewLocationCommand request, CancellationToken cancellationToken)
        {
            var existingLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.LocationName == request.LocationName,
                    cancellationToken);
            if (existingLocation is not null)
            {
                throw new LocationAlreadyExist();
            }
           
            var location = new Domain.Location
                {
                    LocationName = request.LocationName,
                    AddedBy = request.AddedBy,
                    IsActive = true
                };

            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    
    [HttpPost("AddNewLocation")]
    public async Task<IActionResult> Add(AddNewLocationCommand command)
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
            response.Messages.Add($"Location {command.LocationName} successfully added");
            response.Status = StatusCodes.Status200OK;
            return Ok(response);

        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}