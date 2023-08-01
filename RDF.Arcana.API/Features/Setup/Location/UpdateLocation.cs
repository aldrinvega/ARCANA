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

    public class UpdateLocationCommand : IRequest<Unit>
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateLocationCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var existingLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId, cancellationToken);

            if (existingLocation is null)
            {
                throw new NoLocationFoundException();
            }

            if (existingLocation.LocationName == request.LocationName)
            {
                throw new System.Exception("No changes");
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

            return Unit.Value;
        }
    }
    
    [HttpPut("UpdateLocation/{id:int}")]
    public async Task<IActionResult> Update(UpdateLocationCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.LocationId = id;
            command.ModifiedBy = User.Identity?.Name;
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Location updated successfully");
            response.Status = StatusCodes.Status200OK;
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