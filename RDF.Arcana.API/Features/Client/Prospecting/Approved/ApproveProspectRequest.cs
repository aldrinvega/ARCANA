/*using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Approved;

[Route("api/Prospecting")]
[ApiController]
public class ApproveProspectRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveProspectRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ApproveProspectRequest/{id}")]
    public async Task<IActionResult> Approved(int id)
    {
        try
        {
            var command = new ApprovedProspectRequestCommand
            {
                ProspectId = id,
            };
            await _mediator.Send(command);
            return Ok();
        }
        catch (System.Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class ApprovedProspectRequestCommand : IRequest<Result>
    {
        public int ProspectId { get; set; }
    }

    public class Handler : IRequestHandler<ApprovedProspectRequestCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ApprovedProspectRequestCommand request, CancellationToken cancellationToken)
        {

            var requestedClients =
                await _context.Approvals
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(
                        x => x.ClientId == request.ProspectId &&
                             x.ApprovalType == Status.ApproverApproval &&
                             x.IsActive == true &&
                             x.IsApproved == false &&
                             x.Client.RegistrationStatus == "Requested" &&
                             x.Client.IsActive,
                        cancellationToken);

            if (requestedClients is null)
            {
                return ClientErrors.NotFound();
            }

            if (requestedClients.IsApproved)
            {
                return ClientErrors.AlreadyApproved(requestedClients.Client.BusinessName);
            }

            requestedClients.IsApproved = true;
            requestedClients.Client.RegistrationStatus = "Approved";
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}*/