using System.Security.Claims;
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
        public IFormFile Image { get; set; }
        public string PhotoProofPath { get; set; }
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
            var freebieRequest = await _context.Freebies.Include(x => x.FreebieRequest)
                .ThenInclude(x => x.ApprovedClient)
                .FirstOrDefaultAsync(x => x.FreebieRequest.ClientId == request.FreebieRequestId, cancellationToken);

            if (freebieRequest is null)
            {
                throw new Exception("No client found");
            }

            // if (request.Image != null)
            // {
            //    var savePath = Path.Combine("wwwwroo") 
            // }

            freebieRequest.FreebieRequest.StatusId = 2;
            freebieRequest.FreebieRequest.ApprovedClient.Status = 4;

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
                FreebieRequestId = id,
            };

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