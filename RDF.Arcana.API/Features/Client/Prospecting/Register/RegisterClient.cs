using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

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
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                request.RequestedBy = userId;
            }

            request.ClientId = id;
            var result = await _mediator.Send(request, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    public class RegisterClientCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string BarangayName { get; set; }
        public int? StoreTypeId { get; set; }
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

        public class Handler : IRequestHandler<RegisterClientCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _context.Clients
                    .Where(x => x.RegistrationStatus == "Pending registration")
                    .FirstOrDefaultAsync(client => client.Id == request.ClientId, cancellationToken);

                if (existingClient == null)
                {
                    return ClientErrors.NotFound();
                }

                var businessAddress = new BusinessAddress
                {
                    HouseNumber = request.HouseNumber,
                    StreetName = request.StreetName,
                    Barangay = request.BarangayName,
                    City = request.City,
                    Province = request.Province
                };
                
                
                var approvers = await _context.Approvers
                    .Where(x => x.ModuleName == Modules.RegistrationApproval)
                    .OrderBy(x => x.Level)
                    .ToListAsync(cancellationToken);

                if (!approvers.Any())
                {
                    return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
                }

                var newRequest = new Domain.Request
                (
                    Modules.RegistrationApproval, 
                    request.RequestedBy, 
                    approvers.First().UserId, 
                    Status.UnderReview
                );

                await _context.Requests.AddAsync(newRequest, cancellationToken);
                await _context.BusinessAddress.AddAsync(businessAddress, cancellationToken);
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
                existingClient.RequestId = newRequest.Id;
                existingClient.StoreTypeId = request.StoreTypeId;
                
                foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
                         {
                             ApproverId = approver.UserId,
                             RequestId = newRequest.Id,
                             Level = approver.Level,
                         }))
                {
                    _context.RequestApprovers.Add(newRequestApprover);
                }
                
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}