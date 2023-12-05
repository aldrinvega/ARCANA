using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Location.Exception;

namespace RDF.Arcana.API.Features.Setup.Location;

[Route("api/Location")]
[ApiController]

public class UpdateLocation : ControllerBase
{

    private readonly IMediator _mediator;

    public UpdateLocation(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateLocationCommand : IRequest<Result>
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateLocationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var existingLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);

            if (existingLocation is null)
            {
                return LocationErrors.NotFound();
            }

            var isLocationAlreadyExist = await _context.Locations
                .AnyAsync(x => x.Id != request.LocationId && x.LocationName == request.LocationName, cancellationToken);

            if (isLocationAlreadyExist)
            {
                throw new NoLocationFoundException();
            }

            existingLocation.LocationName = request.LocationName;
            existingLocation.ModifiedBy = request.ModifiedBy ?? "Admin";
            existingLocation.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
    
    [HttpPut("UpdateLocation/{id:int}")]
    public async Task<IActionResult> Update(UpdateLocationCommand command, [FromRoute] int id)
    {
        try
        {
            command.LocationId = id;
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
}