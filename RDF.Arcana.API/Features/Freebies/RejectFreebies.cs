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
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ClientId = id;
            command.FreebieRequestId = freebieId;
            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Freebies is rejected successfully");
            response.Success = false;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class RejectFreebiesCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int FreebieRequestId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectFreebiesCommand, Unit>
    {
        private const string FREEBIE_RELEASED = "Released";
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RejectFreebiesCommand request, CancellationToken cancellationToken)
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
                        x.ApprovalType == "For Freebie Approval", cancellationToken);

            if (existingFreebies is null)
            {
                throw new Exception("No Freebies found");
            }

            // Locate the specific FreebieRequest to be Rejected
            var freebieToReject = existingFreebies.FreebieRequest
                .FirstOrDefault(fr => fr.Id == request.FreebieRequestId && fr.Status != FREEBIE_RELEASED);

            if (freebieToReject == null)
            {
                throw new Exception("Freebie Request not found");
            }

            // Reject the specific FreebieRequest
            freebieToReject.Status = "Rejected";
            existingFreebies.Reason = request.Reason;
            existingFreebies.IsApproved = false;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}