using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]

public class UpdateProspectInformation : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProspectInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class UpdateProspectRequestCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string OwnersName { get; set; }
        public string OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int StoreTypeId { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateProspectRequestCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context ;
        }

        public async Task<Unit> Handle(UpdateProspectRequestCommand request, CancellationToken cancellationToken)
        {
            var existingClient =
                await _context.Approvals
                    .Include(x => x.Client)
                    .FirstOrDefaultAsync(
                    x => x.ClientId == request.ClientId 
                         && x.IsActive == true
                         && x.ApprovalType == "Approver Approval",
                    cancellationToken);

            if (existingClient is null)
            {
                throw new ClientIsNotFound();
            }

            existingClient.Client.Fullname = request.OwnersName;
            existingClient.Client.Address = request.OwnersAddress;
            existingClient.Client.PhoneNumber = request.PhoneNumber;
            existingClient.Client.BusinessName = request.BusinessName;
            existingClient.IsApproved = false;
            existingClient.Client.RegistrationStatus = "Requested";
            existingClient.Client.StoreTypeId = request.StoreTypeId;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
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
}