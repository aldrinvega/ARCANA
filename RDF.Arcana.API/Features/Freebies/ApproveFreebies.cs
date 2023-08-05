using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

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
        public int AddedBy { get; set; }
        public IFormFile Image { get; set; }
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
            var freebieRequest = await _context.FreebieRequests.Include(x => x.ApprovedClient)
                .FirstOrDefaultAsync(x => x.Id == request.FreebieRequestId, cancellationToken);

            if (freebieRequest is null)
            {
                throw new Exception("No freebies found");
            }

            if (request.Image != null)
            {
                var savePath = Path.Combine(@"F:\images\", request.Image.FileName);

                var directory = Path.GetDirectoryName(savePath);
                if (directory != null && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                await using var stream = System.IO.File.Create(savePath);
                await request.Image.CopyToAsync(stream, cancellationToken);
                
                var approvedFreebie = new ApprovedFreebies
                {
                    FreebiesId = freebieRequest.Id,
                    ApprovedBy = request.AddedBy,
                    PhotoProofPath = savePath
                };

                await _context.ApprovedFreebies.AddAsync(approvedFreebie, cancellationToken);

            }

            freebieRequest.StatusId = 2;
            freebieRequest.ApprovedClient.Status = 4;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;

        }
    }

    [HttpPatch("ApproveFreebieRequest/{id:int}")]
    public async Task<IActionResult> ApproveFreebieRequest([FromForm]ApproveFreebiesCommand command, [FromRoute] int id)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.FreebieRequestId = id;
            
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
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