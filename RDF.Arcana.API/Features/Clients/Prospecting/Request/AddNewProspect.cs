using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Domain.New_Doamin;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Request;

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
        [Required]
        public string OwnersName { get; set; }
        [Required]
        public string OwnersAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string BusinessName { get; set; }
        [Required] 
        public string StoreType { get; set; }
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

            var prospectingClients = new Domain.New_Doamin.Clients
            {
                Fullname = request.OwnersName,
                Address = request.OwnersAddress,
                PhoneNumber = request.PhoneNumber,
                BusinessName = request.BusinessName,
                StoreType = request.StoreType,
                CustomerType = "Prospect",
                AddedBy = request.AddedBy
            };
            
            await _context.Clients.AddAsync(prospectingClients, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            var approval = new Approvals
            {
                ClientId = prospectingClients.Id,
                ApprovalType = "Approver Approval",
                IsApproved = false,
                IsActive = true 
            };

            // Add the new request to the database
            await _context.Approvals.AddAsync(approval, cancellationToken);
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