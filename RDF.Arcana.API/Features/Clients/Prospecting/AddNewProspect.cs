using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Clients.Prospecting;

[Route("api/Prospecting")]
[ApiController]


public class AddNewProspect : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewProspect(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class AddNewProspectCommand : IRequest<Unit>
    {
        public string OwnersName { get; set; }
        public string OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int AddedBy { get; set; }
    }
    public class Handler : IRequestHandler<AddNewProspectCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddNewProspectCommand request, CancellationToken cancellationToken)
        {
            // Check if business name is null or empty
            if (string.IsNullOrEmpty(request.BusinessName))
            {
                throw new System.Exception("BusinessName cannot be empty");
            }

            var existingProspectCustomer =
                await _context.Clients.FirstOrDefaultAsync(x => x.BusinessName == request.BusinessName, cancellationToken);

            if (existingProspectCustomer != null)
            {
                throw new System.Exception($"Client with business name {request.BusinessName} already exists.");
            }

            var prospectingClients = new Client
            {
                OwnersName = request.OwnersName,
                OwnersAddress = request.OwnersAddress,
                PhoneNumber = request.PhoneNumber,
                BusinessName = request.BusinessName,
                CustomerType = "Prospect",
                AddedBy = request.AddedBy
            };

            // Add the new client to the database
            await _context.Clients.AddAsync(prospectingClients, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var requestedClient = new RequestedClient
            {
                ClientId = prospectingClients.Id,
                DateRequest = DateTime.Now,
                RequestedBy = request.AddedBy,
                IsActive = true,
                Status = 1,
            };

            // Add the new request to the database
            await _context.RequestedClients.AddAsync(requestedClient, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    [HttpPost("AddNewProspect")]
    public async Task<IActionResult> Add(AddNewProspectCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("The new prospect is requested successfully");
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