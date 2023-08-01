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

    public class UpdateLocationStatusCommand : IRequest<Unit>
    {
        public int LocationId { get; set; }
        public string ModifiedBy { get; set; }
    }
    public class Handler : IRequestHandler<UpdateLocationStatusCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateLocationStatusCommand request, CancellationToken cancellationToken)
        {
            var validateLocation =
                await _context.Locations.FirstOrDefaultAsync(x => x.Id == request.LocationId,
                    cancellationToken);

            if (validateLocation is null)
            {
                throw new NoLocationFoundException();
            }

            validateLocation.IsActive = !validateLocation.IsActive;
            validateLocation.ModifiedBy = request.ModifiedBy ?? "Admin";
            validateLocation.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
    [HttpPatch("UpdateLocationStatus/{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new UpdateLocationStatusCommand
            {
                LocationId = id,
                ModifiedBy = User.Identity?.Name
            };
            await _mediator.Send(command);
            response.Success = true;
            response.Messages.Add("Successfully updated the status");
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            return Conflict(response);
        }      
    }

}