using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
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

    public class RejectFreebiesCommand : IRequest<Unit>
    {
        public int FreebieRequestId { get; set; }
        public int ClientId { get; set; }
    }
    
    public class Handler : IRequestHandler<RejectFreebiesCommand, Unit>
    {
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
                .Where(
                    x => x.IsActive &&
                    x.IsApproved == false)
                .FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.FreebieRequest.Id == request.FreebieRequestId, cancellationToken);

            if (existingFreebies is null)
            {
                throw new Exception("No Freebies found");
            }

            if (existingFreebies.IsApproved)
            {
                existingFreebies.IsApproved = false;
            }

            existingFreebies.FreebieRequest.Status = "Rejected";

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    [HttpPut("RejectFreebies")]
    public async Task<IActionResult> RejectFreebie([FromQuery] RejectFreebiesCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
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
}