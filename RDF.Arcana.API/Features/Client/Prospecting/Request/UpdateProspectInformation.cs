using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]
public class UpdateProspectInformation : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProspectInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateProspectInformation/{id:int}")]
    public async Task<IActionResult> UpdateProspect([FromRoute] int id, [FromBody] UpdateProspectRequestCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            command.ClientId = id;
            await _mediator.Send(command);
            response.Messages.Add("Client information updated successfully");
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }

    public class UpdateProspectRequestCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string OwnersName { get; set; }

        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string BarangayName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int StoreTypeId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateProspectRequestCommand, Unit>
    {
        private const string APPROVED_STATUS = "Approved";
        private const string PROSPECT_TYPE = "Prospect";
        private const string APPROVER_APPROVAL = "Approver Approval";

        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateProspectRequestCommand request, CancellationToken cancellationToken)
        {
            var existingClient =
                await _context.Approvals
                    .Include(x => x.Client)
                    .ThenInclude(x => x.OwnersAddress)
                    .FirstOrDefaultAsync(
                        x => x.ClientId == request.ClientId
                             && x.IsActive == true
                             && x.ApprovalType == APPROVER_APPROVAL,
                        cancellationToken);

            if (existingClient is null)
            {
                throw new ClientIsNotFound(request.ClientId);
            }

            existingClient.Client.Fullname = request.OwnersName;
            existingClient.Client.PhoneNumber = request.PhoneNumber;
            existingClient.Client.BusinessName = request.BusinessName;
            existingClient.IsApproved = true;
            existingClient.Client.RegistrationStatus = APPROVED_STATUS;
            existingClient.Client.StoreTypeId = request.StoreTypeId;

            existingClient.Client.OwnersAddress.HouseNumber = request.HouseNumber;
            existingClient.Client.OwnersAddress.StreetName = request.StreetName;
            existingClient.Client.OwnersAddress.Barangay = request.BarangayName;
            existingClient.Client.OwnersAddress.City = request.City;
            existingClient.Client.OwnersAddress.Province = request.Province;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}