using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Items;
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

    public class AddNewLocationCommand : IRequest<Result>
    {
        public string LocationName { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddNewLocationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddNewLocationCommand request, CancellationToken cancellationToken)
        {
            var existingLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.LocationName == request.LocationName,
                    cancellationToken);
            if (existingLocation is not null)
            {
                return LocationErrors.AlreadyExist(request.LocationName);
            }
           
            var location = new Domain.Location
                {
                    LocationName = request.LocationName,
                    AddedBy = request.AddedBy,
                    IsActive = true
                };

            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    [HttpPost("AddNewLocation")]
    public async Task<IActionResult> Add(AddNewLocationCommand command)
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
            return Ok(result);

        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }
}