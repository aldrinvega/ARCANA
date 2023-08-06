using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]

public class UpdateProspectRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProspectRequest(IMediator mediator)
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
                await _context.Clients.FirstOrDefaultAsync(x => x.Id == request.ClientId && x.IsActive == true, cancellationToken);

            if (existingClient is null)
            {
                throw new ClientIsNotFound();
            }

            existingClient.Fullname = request.OwnersName;
            existingClient.Address = request.OwnersAddress;
            existingClient.PhoneNumber = request.PhoneNumber;
            existingClient.BusinessName = request.BusinessName;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    [HttpPut("UpdateProspectRequest/{id:int}")]
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