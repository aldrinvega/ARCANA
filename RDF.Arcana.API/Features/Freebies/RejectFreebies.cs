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

    [HttpPut("RejectFreebies/{id}")]
    public async Task<IActionResult> RejectFreebie([FromBody] RejectFreebiesCommand command, [FromRoute] int id,
        [FromQuery] int freebieId)
    {
        try
        {
            command.ClientId = id;
            command.FreebieRequestId = freebieId;
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
        public int ClientId { get; set; }
        public int FreebieRequestId { get; set; }
        public string Reason { get; set; }
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
            var existingFreebies = await _context.Approvals
                .Include(x => x.Client)
                .Include(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .FirstOrDefaultAsync(
                    x =>
                        x.ClientId == request.ClientId &&
                        x.IsActive &&
                        x.IsApproved == true &&
                        x.ApprovalType == Status.ForFreebieApproval, cancellationToken);

            if (existingFreebies is null)
            {
                return FreebieErrors.NoFreebieFound();
            }

            // Locate the specific FreebieRequest to be Rejected
            var freebieToReject = existingFreebies.FreebieRequest
                .FirstOrDefault(fr => fr.Id == request.FreebieRequestId && fr.Status != Status.Released);

            if (freebieToReject == null)
            {
                return FreebieErrors.NoFreebieFound();
            }

            // Reject the specific FreebieRequest
            freebieToReject.Status = Status.Rejected;
            existingFreebies.Reason = request.Reason;
            existingFreebies.IsApproved = false;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}