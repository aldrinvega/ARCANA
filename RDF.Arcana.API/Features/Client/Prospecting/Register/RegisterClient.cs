using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register;

[Route("api/Registration")]
[ApiController]
public class RegisterClient : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterClient(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RegisterClient/{id}")]
    public async Task<IActionResult> Register([FromBody] RegisterClientCommand request, [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                request.RequestedBy = userId;
            }

            request.ClientId = id;
            await _mediator.Send(request, cancellationToken);
            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status404NotFound;
            response.Messages.Add(ex.Message);

            return Conflict(response);
        }
    }

    public class RegisterClientCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string BarangayName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string TinNumber { get; set; }
        public string AuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativePosition { get; set; }
        public int Cluster { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int RequestedBy { get; set; }

        public class Handler : IRequestHandler<RegisterClientCommand, Unit>
        {
            private const string FOR_REGULAR = "For regular approval";
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _context.Clients
                    .Where(x => x.RegistrationStatus == "Pending registration")
                    .FirstOrDefaultAsync(client => client.Id == request.ClientId, cancellationToken);

                if (existingClient == null)
                {
                    throw new ClientIsNotFound(request.ClientId);
                }

                var businessAddress = new BusinessAddress
                {
                    HouseNumber = request.HouseNumber,
                    StreetName = request.StreetName,
                    Barangay = request.BarangayName,
                    City = request.City,
                    Province = request.Province
                };

                var approval = new Approvals
                {
                    ClientId = existingClient.Id,
                    ApprovalType = FOR_REGULAR,
                    RequestedBy = request.RequestedBy,
                    IsApproved = false,
                    IsActive = true,
                };

                await _context.BusinessAddress.AddAsync(businessAddress, cancellationToken);
                await _context.Approvals.AddAsync(approval, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var dateOfBirth = DateOnly.FromDateTime(request.BirthDate);

                existingClient.BusinessAddressId = businessAddress.Id;
                existingClient.RepresentativeName = request.AuthorizedRepresentative;
                existingClient.RepresentativePosition = request.AuthorizedRepresentativePosition;
                existingClient.TinNumber = request.TinNumber;
                existingClient.Cluster = request.Cluster;
                existingClient.Longitude = request.Longitude;
                existingClient.Latitude = request.Latitude;
                existingClient.DateOfBirthDB = dateOfBirth;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}