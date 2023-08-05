using System.Data;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Request;

[Route("api/Propspecting")]
[ApiController]

public class GetRequestedProspectById : ControllerBase
{
    private readonly IMediator _mediator;

    public GetRequestedProspectById(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetRequestedProspectByIdQuery : IRequest<GetRequestedProspectByIdResult>
    {
        public int ClientId { get; set; }
    }

    public class GetRequestedProspectByIdResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddedBy { get; set; }
        public string CustomerType { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
    public class Handler : IRequestHandler<GetRequestedProspectByIdQuery, GetRequestedProspectByIdResult>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetRequestedProspectByIdResult> Handle(GetRequestedProspectByIdQuery request, CancellationToken cancellationToken)
        {
            var requestedClient =
                await _context.RequestedClients
                    .Include(x => x.Client)
                    .ThenInclude(x => x.AddedByUser)
                    .FirstOrDefaultAsync(x => x.ClientId == request.ClientId, cancellationToken);

            var result = new GetRequestedProspectByIdResult
            {
                Id = requestedClient.ClientId,
                OwnersName = requestedClient.Client.OwnersName,
                PhoneNumber = requestedClient.Client.PhoneNumber,
                AddedBy = requestedClient.Client.AddedByUser.Fullname,
                CustomerType = requestedClient.Client.CustomerType,
                BusinessName = requestedClient.Client.BusinessName,
                Address = requestedClient.Client.OwnersAddress,
                CreatedAt = requestedClient.DateRequest,
                IsActive = requestedClient.IsActive
            };
            return result;
        }
    }

    [HttpGet("GetRequestedProspectById/{id:int}")]
    public async Task<IActionResult> GetRequestedProspect([FromRoute]int id)
    {
        var response = new QueryOrCommandResult<GetRequestedProspectByIdResult>();
        try
        {
            var query = new GetRequestedProspectByIdQuery
            {
                ClientId = id
            };
            var result = await _mediator.Send(query);
            response.Data = result;
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Messages.Add("Successfully fetch data");
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