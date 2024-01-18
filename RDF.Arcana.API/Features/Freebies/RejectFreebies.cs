using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class RejectFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectFreebies/{freebieId}")]
    public async Task<IActionResult> RejectFreebie(
        [FromRoute] int freebieId)
    {
        try
        {
            var command = new RejectFreebiesCommand
            {
                FreebieRequestId = freebieId
            };
            
           var result = await _mediator.Send(command);
           return Ok(result);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class RejectFreebiesCommand : IRequest<Result>
    {
        public int FreebieRequestId { get; set; }
    }

    public class Handler : IRequestHandler<RejectFreebiesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectFreebiesCommand request, CancellationToken cancellationToken)
        {
            var existingFreebies = await _context.FreebieRequests
                .Include(x => x.Clients)
                .Include(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .FirstOrDefaultAsync(
                    x =>
                        x.Id == request.FreebieRequestId &&
                        x.IsActive &&
                        x.Status != Status.Rejected &&
                        x.IsActive,
                        cancellationToken);

            if (existingFreebies is null)
            {
                return FreebieErrors.NoFreebieFound();
            }

            // Reject the specific FreebieRequest
            existingFreebies.Status = Status.Rejected;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}