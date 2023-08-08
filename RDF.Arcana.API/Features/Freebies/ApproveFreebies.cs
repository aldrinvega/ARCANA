using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Freebies;

public class ApproveFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class ApproveFreebiesCommand : IRequest<Unit>
    {
        public int FreebieRequestId { get; set; }
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
            var freebieRequest = await _context.Approvals
                .Include(x => x.Client)
                .Include(x => x.FreebieRequest)
                .FirstOrDefaultAsync(x => x.FreebieRequest.Id == request.FreebieRequestId, cancellationToken);

            if (freebieRequest is null)
            {
                throw new Exception("No freebies found");
            }

            freebieRequest.FreebieRequest.Status = "Approved";
            freebieRequest.IsApproved = true;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }

    [HttpPatch("ApproveFreebieRequest/{id:int}")]
    public async Task<IActionResult> ApproveFreebieRequest([FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {

            var command = new ApproveFreebiesCommand
            {
                FreebieRequestId = id
            };
            
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
}