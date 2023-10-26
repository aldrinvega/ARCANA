using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Client.Prospecting.Request;

public class ValidationException : Exception
{
    // Constructor for a single error message.
    public ValidationException(string message) : base(message)
    {
        Errors = new List<string> { message };
    }

    // Constructor for multiple error messages.
    public ValidationException(IEnumerable<string> errors) : base("There are validation errors.")
    {
        Errors = errors;
    }

    // Constructor for a single error message with inner exception.
    public ValidationException(string message, System.Exception innerException) : base(message, innerException)
    {
        Errors = new List<string> { message };
    }

    public IEnumerable<string> Errors { get; }
}

public class AddNewProspectCommand : IRequest<AddNewProspectResult>
{
    [Required] public string OwnersName { get; set; }
    [Required] public string EmailAddress { get; set; }

    public string HouseNumber { get; set; }
    public string StreetName { get; set; }
    public string BarangayName { get; set; }
    public string City { get; set; }
    public string Province { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] public string BusinessName { get; set; }

    [Required] public int StoreTypeId { get; set; }

    public int AddedBy { get; set; }
}

public class AddNewProspectResult
{
    public int Id { get; set; }
    public string OwnersName { get; set; }
    public OwnersAddressCollection OwnersAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string BusinessName { get; set; }
    public int AddedBy { get; set; }

    public class OwnersAddressCollection
    {
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string BarangayName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
    }
}

public class Handler : IRequestHandler<AddNewProspectCommand, AddNewProspectResult>
{
    private const string APPROVED_STATUS = "Requested";
    private const string ORIGIN = "Prospecting";
    private const string APPROVER_APPROVAL = "Approver Approval";
    private readonly DataContext _context;

    public Handler(DataContext context)
    {
        _context = context;
    }

    public async Task<AddNewProspectResult> Handle(AddNewProspectCommand request, CancellationToken cancellationToken)
    {
        var validationErrors = new List<string>();

        // Check if business name is null or empty
        if (string.IsNullOrEmpty(request.BusinessName))
        {
            validationErrors.Add("Business Name cannot be empty");
        }

        var existingProspectCustomer =
            await _context.Clients.FirstOrDefaultAsync(
                x => x.BusinessName == request.BusinessName
                     && x.Fullname == request.OwnersName
                     && x.StoreTypeId == request.StoreTypeId
                , cancellationToken);

        if (existingProspectCustomer != null)
        {
            validationErrors.Add("The prospect with the given details already exists");
        }

        if (validationErrors.Any())
        {
            throw new ValidationException(validationErrors);
        }

        var address = new OwnersAddress
        {
            HouseNumber = request.HouseNumber,
            StreetName = request.StreetName,
            Barangay = request.BarangayName,
            City = request.City,
            Province = request.Province
        };

        await _context.Address.AddAsync(address, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var prospectingClients = new Domain.Clients
        {
            Fullname = request.OwnersName,
            OwnersAddressId = address.Id,
            PhoneNumber = request.PhoneNumber,
            EmailAddress = request.EmailAddress,
            BusinessName = request.BusinessName,
            StoreTypeId = request.StoreTypeId,
            RegistrationStatus = APPROVED_STATUS,
            Origin = ORIGIN,
            AddedBy = request.AddedBy
        };

        await _context.Clients.AddAsync(prospectingClients, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var approval = new Approvals
        {
            ClientId = prospectingClients.Id,
            ApprovalType = APPROVER_APPROVAL,
            IsApproved = true,
            IsActive = true,
            RequestedBy = request.AddedBy,
            ApprovedBy = request.AddedBy
        };

        // Add the new request to the database
        await _context.Approvals.AddAsync(approval, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new AddNewProspectResult
        {
            Id = prospectingClients.Id,
            OwnersName = prospectingClients.Fullname,
            OwnersAddress = new AddNewProspectResult.OwnersAddressCollection
            {
                HouseNumber = prospectingClients.OwnersAddress.HouseNumber,
                StreetName = prospectingClients.OwnersAddress.StreetName,
                BarangayName = prospectingClients.OwnersAddress.Barangay,
                City = prospectingClients.OwnersAddress.City,
                Province = prospectingClients.OwnersAddress.Province
            },
            PhoneNumber = prospectingClients.PhoneNumber,
            BusinessName = prospectingClients.BusinessName,
            AddedBy = prospectingClients.AddedBy
        };
    }
}

[Route("api/Prospecting")]
[ApiController]
public class AddNewProspect : ControllerBase
{
    private readonly IMediator _mediator;

    public AddNewProspect(IMediator mediator)
    {
        _mediator = mediator;
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

            var result = await _mediator.Send(command);
            response.Data = result;
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add("The new prospect is requested successfully");
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