using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Setup.Location.Exception;


namespace RDF.Arcana.API.Features.Setup.Location;

[Route("api/Location")]
[ApiController]

public class UpdateLocationStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateLocationStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateLocationStatusCommand : IRequest<Result>
    {
        public int LocationId { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateLocationStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateLocationStatusCommand request, CancellationToken cancellationToken)
        {
            var validateLocation = await _context.Locations.FirstOrDefaultAsync(x => 
                    x.Id == request.LocationId,
                    cancellationToken);

            if (validateLocation is null)
            {
                throw new NoLocationFoundException();
            }

            validateLocation.IsActive = !validateLocation.IsActive;
            validateLocation.ModifiedBy = request.ModifiedBy ?? "Admin";
            validateLocation.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    [HttpPatch("UpdateLocationStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateLocationStatusCommand
            {
                LocationId = id,
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