using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Direct;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Features.Client.Direct.Exceptions;

namespace RDF.Arcana.API.Features.Client.Direct
{
    public class DirectRegistrationCommand : IRequest<Unit>
    {
        [Required] public string OwnersName { get; set; }

        [Required] public string OwnersAddress { get; set; }

        [Required] public string PhoneNumber { get; set; }

        [Required] public string BusinessName { get; set; }

        [Required] public int StoreTypeId { get; set; }

        public int AddedBy { get; set; }
        public string BusinessAddress { get; set; }
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
        public List<Attachment> Attachments { get; set; }

        public class FixedDiscounts
        {
            public decimal DiscountPercentage { get; set; }
        }

        public class Attachment
        {
            public IFormFile Attachments { get; set; }
            public string DocumentsType { get; set; }
        }
    }

    public class Handler : IRequestHandler<DirectRegistrationCommand, Unit>
    {
        private const string APPROVED_STATUS = "Approved";
        private const string REGISTRATION_TYPE = "Direct";
        private const string APPROVER_APPROVAL = "Approver Approval";

        private readonly Cloudinary _cloudinary;
        private readonly DataContext _context;

        public Handler(DataContext context, IOptions<CloudinarySettings> config)
        {
            _context = context;
            var account = new Account(
                config.Value.Cloudname,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<Unit> Handle(DirectRegistrationCommand request, CancellationToken cancellationToken)
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
                var directClients = new Domain.Clients
                {
                    Fullname = request.OwnersName,
                    Address = request.OwnersAddress,
                    PhoneNumber = request.PhoneNumber,
                    BusinessName = request.BusinessName,
                    StoreTypeId = request.StoreTypeId,
                    BusinessAddress = request.BusinessAddress,
                    RepresentativeName = request.AuthorizedRepresentative,
                    RepresentativePosition = request.AuthorizedRepresentativePosition,
                    Cluster = request.Cluster,
                    Freezer = request.Freezer,
                    ClientType = request.TypeOfCustomer,
                    DirectDelivery = request.DirectDelivery,
                    BookingCoverageId = request.BookingCoverageId,
                    ModeOfPayment = request.ModeOfPayment,
                    Terms = request.TermsId,
                    RegistrationStatus = APPROVED_STATUS,
                    CustomerType = REGISTRATION_TYPE,
                    IsActive = true,
                    AddedBy = request.AddedBy,
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                };

                await _context.Clients.AddAsync(directClients, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                if (request.FixedDiscount != null)
                {
                    var fixedDiscount = new FixedDiscounts
                    {
                        ClientId = directClients.Id,
                        DiscountPercentage = request.FixedDiscount.DiscountPercentage
                    };

                    await _context.FixedDiscounts.AddAsync(fixedDiscount, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    directClients.FixedDiscountId = fixedDiscount.Id;
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    directClients.VariableDiscount = request.VariableDiscount;
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var termsOptions = new TermOptions
                {
                    ClientId = directClients.Id,
                    TermId = request.TermsId,
                    CreditLimit = request.CreditLimit,
                    TermDaysId = request.TermDaysId,
                    AddedBy = request.AddedBy
                };

                await _context.TermOptions.AddAsync(termsOptions, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var approval = new Approvals
                {
                    ClientId = directClients.Id,
                    ApprovalType = APPROVER_APPROVAL,
                    IsApproved = false,
                    IsActive = true,
                };

                await _context.Approvals.AddAsync(approval, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                if (!request.Attachments.Any()) return Unit.Value;

                foreach (var document in request.Attachments.Where(document => document.Attachments.Length > 0))
                {
                    await using var stream = document.Attachments.OpenReadStream();

                    var attachmentsParams = new ImageUploadParams
                    {
                        File = new FileDescription(document.Attachments.FileName, stream),
                        PublicId = $"{directClients.BusinessName}/{document.Attachments.FileName}"
                    };

                    var attachmentsUploadResult = await _cloudinary.UploadAsync(attachmentsParams);

                    var attachments = new ClientDocuments
                    {
                        DocumentPath = attachmentsUploadResult.SecureUrl.ToString(),
                        ClientId = directClients.Id,
                        DocumentType = document.DocumentsType
                    };

                    await _context.ClientDocuments.AddAsync(attachments, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                throw new InvalidOperationException($"Client already exist!");
            }

            return Unit.Value;
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