using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Direct;
using RDF.Arcana.API.Features.Client.Direct.Exceptions;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Freebies;
using RDF.Arcana.API.Features.Setup.Items;

namespace RDF.Arcana.API.Features.Client.Direct
{
    public class DirectRegistrationCommand : IRequest<Result<DirectRegisterResult>>
    {
        [Required] public string OwnersName { get; set; }
        [Required] public OwnersAddressCollection OwnersAddress { get; set; }
        [Required] public string EmailAddress { get; set; }
        [Required] public string PhoneNumber { get; set; }
        [Required] public DateOnly DateOfBirth { get; set; }
        [Required] public string TinNumber { get; set; }
        [Required] public string BusinessName { get; set; }
        [Required] public int StoreTypeId { get; set; }
        public int AddedBy { get; set; }
        public BusinessAddressCollection BusinessAddress { get; set; }
        public string AuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativePosition { get; set; }
        public int Cluster { get; set; }
        public bool Freezer { get; set; }
        public string TypeOfCustomer { get; set; }
        public bool DirectDelivery { get; set; }
        public int BookingCoverageId { get; set; }
        public int ModeOfPayment { get; set; }
        public int TermsId { get; set; }
        public int? CreditLimit { get; set; }
        public int? TermDaysId { get; set; }
        public FixedDiscounts FixedDiscount { get; set; }
        public bool VariableDiscount { get; set; }
        public string Longitude { get; set; }

        public string Latitude { get; set; }

        /*public List<Attachment> Attachments { get; set; }*/
        public List<UpdateFreebie> Freebies { get; set; }

        public class FixedDiscounts
        {
            public decimal? DiscountPercentage { get; set; }
        }

        /*public class Attachment
        {
            public IFormFile Attachments { get; set; }
            public string DocumentType { get; set; }
        }*/
        public class BusinessAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class UpdateFreebie
        {
            public int ItemId { get; set; }
        }
    }

    public class DirectRegisterResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }
    }

    public class Handler : IRequestHandler<DirectRegistrationCommand, Result<DirectRegisterResult>>
    {
        private const string APPROVED_STATUS = "Approved";
        private const string REQUESTED_STATUS = "Under review";
        private const string REGISTRATION_TYPE = "Direct";
        private const string DIRECT_REGISTRATION_APPROVAL = "Direct Registration Approval";

        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<DirectRegisterResult>> Handle(DirectRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(
                x => x.Fullname == request.OwnersName &&
                     x.StoreType.Id == request.StoreTypeId &&
                     x.BusinessName == request.BusinessName,
                cancellationToken
            );

            var validateStoreType = await _context.StoreTypes.FirstOrDefaultAsync(x => x.Id == request.StoreTypeId,
                cancellationToken: cancellationToken);
            if (validateStoreType == null)
                throw new StoreTypeNotFoundException(request.StoreTypeId);

            var validateBookingCoverages =
                await _context.BookingCoverages.FirstOrDefaultAsync(x => x.Id == request.BookingCoverageId,
                    cancellationToken: cancellationToken);
            if (validateBookingCoverages == null)
                throw new BookingCoverageNotFoundException(request.BookingCoverageId);

            var validateModeOfPayment =
                await _context.ModeOfPayments.FirstOrDefaultAsync(x => x.Id == request.ModeOfPayment,
                    cancellationToken: cancellationToken);
            if (validateModeOfPayment == null)
                throw new ModeOfPaymentNotFoundException(request.ModeOfPayment);

            var validateTermsAndCondition =
                await _context.Terms.FirstOrDefaultAsync(x => x.Id == request.TermsId,
                    cancellationToken: cancellationToken);
            if (validateTermsAndCondition == null)
                throw new TermsNotFoundException(request.TermsId);

            if (existingClient == null)
            {
                var ownersAddress = new OwnersAddress
                {
                    HouseNumber = request.OwnersAddress.HouseNumber,
                    StreetName = request.OwnersAddress.StreetName,
                    Barangay = request.OwnersAddress.BarangayName,
                    City = request.OwnersAddress.City,
                    Province = request.OwnersAddress.Province
                };
                _context.Address.Add(ownersAddress);

                var businessAddress = new BusinessAddress
                {
                    HouseNumber = request.BusinessAddress.HouseNumber,
                    StreetName = request.BusinessAddress.StreetName,
                    Barangay = request.BusinessAddress.BarangayName,
                    City = request.BusinessAddress.City,
                    Province = request.BusinessAddress.Province
                };
                _context.BusinessAddress.Add(businessAddress);

                var directClients = new Domain.Clients
                {
                    Fullname = request.OwnersName,
                    OwnersAddressId = ownersAddress.Id,
                    PhoneNumber = request.PhoneNumber,
                    EmailAddress = request.EmailAddress,
                    TinNumber = request.TinNumber,
                    BusinessName = request.BusinessName,
                    DateOfBirthDB = request.DateOfBirth,
                    StoreTypeId = request.StoreTypeId,
                    BusinessAddressId = businessAddress.Id,
                    RepresentativeName = request.AuthorizedRepresentative,
                    RepresentativePosition = request.AuthorizedRepresentativePosition,
                    Cluster = request.Cluster,
                    Freezer = request.Freezer,
                    ClientType = request.TypeOfCustomer,
                    DirectDelivery = request.DirectDelivery,
                    BookingCoverageId = request.BookingCoverageId,
                    ModeOfPayment = request.ModeOfPayment,
                    RegistrationStatus = REQUESTED_STATUS,
                    Origin = REGISTRATION_TYPE,
                    IsActive = true,
                    AddedBy = request.AddedBy,
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                };
                _context.Clients.Add(directClients);

                if (request.FixedDiscount != null)
                {
                    var fixedDiscount = new FixedDiscounts
                    {
                        ClientId = directClients.Id,
                        DiscountPercentage = request.FixedDiscount.DiscountPercentage
                    };
                    _context.FixedDiscounts.Add(fixedDiscount);

                    directClients.FixedDiscountId = fixedDiscount.Id;
                }
                else
                {
                    directClients.VariableDiscount = request.VariableDiscount;
                }

                var termsOptions = new TermOptions
                {
                    TermsId = request.TermsId,
                    CreditLimit = request.CreditLimit,
                    TermDaysId = request.TermDaysId,
                    AddedBy = request.AddedBy
                };
                _context.TermOptions.Add(termsOptions);

                directClients.Terms = termsOptions.Id;

                var approval = new Approvals
                {
                    ClientId = directClients.Id,
                    ApprovalType = DIRECT_REGISTRATION_APPROVAL,
                    RequestedBy = request.AddedBy,
                    IsApproved = false,
                    IsActive = true,
                };
                _context.Approvals.Add(approval);


                if (request.Freebies != null)
                {
                    //Check if the client is existing
                    var existingRegularClient =
                        await _context.Clients.FirstOrDefaultAsync(x => x.Id == directClients.Id, cancellationToken);

                    if (request.Freebies.Count > 5)
                    {
                        return Result<DirectRegisterResult>.Failure(FreebieErrors.Exceed5Items());
                    }

                    if (request.Freebies.Select(x => x.ItemId).Distinct().Count() != request.Freebies.Count)
                    {
                        return Result<DirectRegisterResult>.Failure(FreebieErrors.CannotBeRepeated());
                    }

                    //Validate if the Item is already requested | 1 item per client
                    foreach (var item in request.Freebies)
                    {
                        var existingRequest = await _context.FreebieItems
                            .Include(x => x.Items)
                            .Include(f => f.FreebieRequest)
                            .Where(f => f.ItemId == item.ItemId && f.FreebieRequest.ClientId == directClients.Id)
                            .FirstOrDefaultAsync(cancellationToken);

                        var notExistItems =
                            await _context.Items.FirstOrDefaultAsync(x => x.Id == item.ItemId, cancellationToken);

                        if (notExistItems == null)
                        {
                            return Result<DirectRegisterResult>.Failure(ItemErrors.NotFound(item.ItemId));
                        }

                        if (existingRequest != null)
                        {
                            return Result<DirectRegisterResult>.Failure(
                                FreebieErrors.AlreadyRequested(existingRequest.Items.ItemDescription));
                        }
                    }

                    var newApproval = new Approvals
                    {
                        ClientId = directClients.Id,
                        ApprovalType = "For Freebie Approval",
                        IsApproved = true,
                        IsActive = true,
                        RequestedBy = request.AddedBy,
                        ApprovedBy = request.AddedBy
                    };
                    _context.Approvals.Add(newApproval);

                    var freebieRequest = new FreebieRequest
                    {
                        ClientId = directClients.Id,
                        ApprovalsId = newApproval.Id,
                        Status = Status.ForReleasing,
                        IsDelivered = false,
                        RequestedBy = request.AddedBy
                    };
                    _context.FreebieRequests.Add(freebieRequest);

                    foreach (var freebieItem in request.Freebies.Select(freebie => new FreebieItems
                             {
                                 RequestId = freebieRequest.Id,
                                 ItemId = freebie.ItemId,
                                 Quantity = 1
                             }))
                    {
                        await _context.FreebieItems.AddAsync(freebieItem, cancellationToken);
                    }
                }

                var result = new DirectRegisterResult
                {
                    Id = directClients.Id,
                    OwnersName = directClients.Fullname,
                    OwnersAddress = new DirectRegisterResult.OwnersAddressCollection
                    {
                        HouseNumber = directClients.OwnersAddress.HouseNumber,
                        StreetName = directClients.OwnersAddress.StreetName,
                        BarangayName = directClients.OwnersAddress.Barangay,
                        City = directClients.OwnersAddress.City,
                        Province = directClients.OwnersAddress.Province
                    },
                    PhoneNumber = directClients.PhoneNumber,
                    BusinessName = directClients.BusinessName
                };

                await _context.SaveChangesAsync(cancellationToken);

                return Result<DirectRegisterResult>.Success(result, null);
            }
            else
            {
                return Result<DirectRegisterResult>.Failure(ClientErrors.AlreadyExist(existingClient.Fullname,
                    existingClient.BusinessName));
            }
        }
    }
}

[Route("api/DirectRegistration")]
[ApiController]
public class DirectRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public DirectRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("DirectRegistration")]
    public async Task<IActionResult> DirectClientRegistration([FromBody] DirectRegistrationCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}