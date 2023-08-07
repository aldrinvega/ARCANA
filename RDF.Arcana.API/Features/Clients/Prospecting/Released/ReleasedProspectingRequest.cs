using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Released;

[Route("api/[controller]")]
[ApiController]

public class ReleasedProspectingRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ReleasedProspectingRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class ReleasedProspectingRequestCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public IFormFile PhotoProof { get; set; }
    }
    
    public class Handler : IRequestHandler<ReleasedProspectingRequestCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async  Task<Unit> Handle(ReleasedProspectingRequestCommand request, CancellationToken cancellationToken)
        {
            var validateClientRequest = await _context.Approvals
                .Include(x => x.FreebieRequest)
                .Include(x => x.Client)
                .FirstOrDefaultAsync(x =>
                    x.ClientId == request.ClientId &&
                    x.ApprovalType == "For Freebie Approval" &&
                    x.IsActive == true &&
                    x.IsApproved == true, cancellationToken);
            
            if (validateClientRequest is null)
            {
                throw new NoProspectClientFound();
            }

            validateClientRequest.FreebieRequest.IsDelivered = true;
            validateClientRequest.Client.RegistrationStatus = "Released";
            
            if (request.PhotoProof != null)
            {
                var savePath = Path.Combine($@"F:\images\{validateClientRequest.Client.Fullname}", request.PhotoProof.FileName);
            
                var directory = Path.GetDirectoryName(savePath);
                if (directory != null && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
            
                await using var stream = System.IO.File.Create(savePath);
                await request.PhotoProof.CopyToAsync(stream, cancellationToken);

                validateClientRequest.FreebieRequest.PhotoProofPath = savePath;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }

    [HttpPut("ReleasedProspectingRequest/{id:int}")]
    public async Task<IActionResult> ReleasedProspecting([FromForm] ReleasedProspectingRequestCommand command,
        [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ClientId = id;

            await _mediator.Send(command);
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("Freebie Request has been approved");
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}