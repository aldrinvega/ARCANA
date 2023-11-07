using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.Regular;

[Route("api/RegularClients")]
[ApiController]
public class RejectRegularRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectRegularRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectRegularRegistration/{id:int}")]
    public async Task<IActionResult> RejectRegularClients(RejectRegularRegistrationCommand command, int id)
    {
        try
        {
            command.ClientId = id;
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class RejectRegularRegistrationCommand : IRequest<Result<object>>
    {
        public int ClientId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectRegularRegistrationCommand, Result<object>>
    {
        private const string FOR_REGULAR = "For regular approval";
        private const string REJECTED = "Rejected";
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<object>> Handle(RejectRegularRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            var regularClients =
                await _context.Approvals
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(
                        x => x.ClientId == request.ClientId &&
                             x.ApprovalType == FOR_REGULAR &&
                             x.IsApproved == false &&
                             x.IsActive == true,
                        cancellationToken);

            if (regularClients == null)
            {
                return Result<object>.Failure(ClientErrors.NotFound());
            }

            if (regularClients.Client.RegistrationStatus == REJECTED)
            {
                return Result<object>.Failure(ClientErrors.AlreadyRejected(regularClients.Client.BusinessName));
            }

            regularClients.Reason = request.Reason;
            regularClients.Client.RegistrationStatus = REJECTED;
            await _context.SaveChangesAsync(cancellationToken);

            return Result<object>.Success(null, "Regular registration rejected successfully");
        }
    }
}