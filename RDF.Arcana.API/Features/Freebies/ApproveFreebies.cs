using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class ApproveFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("ApproveFreebieRequest/{id:int}")]
    public async Task<IActionResult> ApproveFreebieRequest([FromRoute] int id, [FromQuery] int freebieId)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var command = new ApproveFreebiesCommand
            {
                ClientId = id,
                FreebieRequestId = freebieId
            };
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.ApprovedBy = userId;
            }

            //
            // if (User.Identity is ClaimsIdentity identity
            //     && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            // {
            //     command.AddedBy = userId;
            // };

            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Freebie Request has been approved");
            response.Success = true;
            return Ok(response);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }

    public class ApproveFreebiesCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public int FreebieRequestId { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class Handler : IRequestHandler<ApproveFreebiesCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(ApproveFreebiesCommand request, CancellationToken cancellationToken)
        {
            var approvals = await _context.Approvals
                .Include(x => x.Client)
                .Include(x => x.FreebieRequest)
                .FirstOrDefaultAsync(x => x.ClientId == request.ClientId, cancellationToken);

            if (approvals == null || !approvals.FreebieRequest.Any())
            {
                throw new Exception("No freebies found");
            }

            var freebieRequest = approvals.FreebieRequest.FirstOrDefault(x => x.Id == request.FreebieRequestId);

            if (freebieRequest == null)
            {
                throw new Exception("No matching freebie request found");
            }

            freebieRequest.Status = "Approved";
            approvals.IsApproved = true;
            approvals.ApprovedBy = request.ApprovedBy;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}