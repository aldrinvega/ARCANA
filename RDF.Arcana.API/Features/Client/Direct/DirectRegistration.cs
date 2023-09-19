using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Direct;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Client.Direct
{
    public class DirectRegistrationCommand : IRequest<Unit>
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
        public int StoreTypeId { get; set; }
        public int AddedBy { get; set; }
        public string BusinessAdress { get; set; }
        public string AuthrizedRepreesentative { get; set; }
        public string AuthrizedRepreesentativePosition { get; set; }
        public int Cluster { get; set; }
        public bool Freezer { get; set; }
        public string TypeOfCustomer { get; set; } 
        public bool DirectDelivery { get; set; }
        //public int BookingCoverage { get; set; }
        public int ModeOfPayment { get; set; }
        //public int Terms { get; set; }
        /*public DiscountTypes Discount { get; set; }*/
    }

    public class Handler : IRequestHandler<DirectRegistrationCommand, Unit>
    {

        private const string APPROVED_STATUS = "Approved";
        private const string PROSPECT_TYPE = "Prospect";
        private const string APPROVER_APPROVAL = "Approver Approval";

        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DirectRegistrationCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(
                x => x.Fullname == request.OwnersName &&
                x.StoreType.Id == request.StoreTypeId &&
                x.BusinessName == request.BusinessName
                );

            var validateStoreType = await _context.StoreTypes.FirstOrDefaultAsync(x => x.Id == request.StoreTypeId);
            //var validateBookingOcverage = await _context.BookingCoverages.FirstOrDefaultAsync(x => x.Id == request.BookingCoverage);
            var validateModeOfPayment = await _context.ModeOfPayments.FirstOrDefaultAsync(x => x.Id == request.ModeOfPayment);
            //var validateTermsAndCondition = await _context.Terms.FirstOrDefaultAsync(x => x.Id == request.Terms);
            

            if (existingClient == null)
            {
                var directClients = new Domain.Clients
                {
                    Fullname = request.OwnersName,
                    Address = request.OwnersAddress,
                    PhoneNumber = request.PhoneNumber,
                    BusinessName = request.BusinessName,
                    StoreTypeId = request.StoreTypeId,
                    BusinessAddress = request.BusinessAdress,
                    RepresentativeName = request.AuthrizedRepreesentative,
                    RepresentativePosition = request.AuthrizedRepreesentativePosition,
                    Cluster = request.Cluster,
                    Freezer = request.Freezer,
                    ClientType = request.TypeOfCustomer,
                    DirectDelivery = request.DirectDelivery,
                    //BookingCoverageId = request.BookingCoverage,
                    ModeOfPayment = request.ModeOfPayment,
                    //Terms = request.Terms,
                    RegistrationStatus = APPROVED_STATUS,
                    CustomerType = PROSPECT_TYPE,
                    IsActive = true,
                    AddedBy = request.AddedBy
                };

                await _context.Clients.AddAsync(directClients, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                var approval = new Approvals
                {
                    ClientId = directClients.Id,
                    ApprovalType = APPROVER_APPROVAL,
                    IsApproved = true,
                    IsActive = true,
                };

                await _context.Approvals.AddAsync(approval, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

            }
            else
            {
                throw new InvalidOperationException($"Client already exist!");
            }

            return Unit.Value;

        }
    }
}

[Route("api/Directregistration")]
[ApiController]
public class DirectRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public DirectRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("DirectRegistration")]
    public async Task<IActionResult> DirectClientRegistration([FromBody]DirectRegistrationCommand command)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity
            && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }
            await _mediator.Send(command);
            response.Success = true;
            response.Status = StatusCodes.Status200OK;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.Status = StatusCodes.Status404NotFound;
            response.Messages.Add(ex.Message);
            return Conflict(response);
        }
    }
}