using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies"), ApiController]

public class UpdateFreebieStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateFreebieStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateFreebieRequestStatus/{id}")]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        try
        {
            var command = new UpdateFreebieStatusCommand
            {
                FreebieRequestId = id
            };

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateFreebieStatusCommand : IRequest<Result>
    {
        public int FreebieRequestId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateFreebieStatusCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateFreebieStatusCommand request, CancellationToken cancellationToken)
        {
            var existingFreebieRequest =
                await _context.FreebieRequests
                    .FirstOrDefaultAsync(fb => 
                            fb.Id == request.FreebieRequestId, 
                            cancellationToken);

            if (existingFreebieRequest is null)
            {
                return FreebieErrors.NoFreebieFound();
            }

            existingFreebieRequest.IsActive = !existingFreebieRequest.IsActive;
            
            return Result.Success();
        }
    }
}