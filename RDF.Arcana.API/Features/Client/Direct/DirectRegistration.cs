using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Direct;
using RDF.Arcana.API.Features.Client.Direct.Exceptions;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Freebies;
using RDF.Arcana.API.Features.Requests_Approval;
using RDF.Arcana.API.Features.Setup.Items;
using RDF.Arcana.API.Features.Setup.Mode_Of_Payment;

namespace RDF.Arcana.API.Features.Client.Direct
{
    public class DirectRegistrationCommand : IRequest<Result>
    { 
        public string OwnersName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; } 
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; } 
        public string TinNumber { get; set; }
        public string BusinessName { get; set; }
        public int StoreTypeId { get; set; }
        public int AddedBy { get; set; }
        public BusinessAddressCollection BusinessAddress { get; set; }
        public string AuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativePosition { get; set; }
        public int ClusterId { get; set; }
        public bool Freezer { get; set; }
        public string TypeOfCustomer { get; set; }
        public bool DirectDelivery { get; set; }
        public int BookingCoverageId { get; set; }
        public ICollection<ClientModeOfPayments> ModeOfPayments { get; set; }
        public int TermsId { get; set; }
        public int? CreditLimit { get; set; }
        public int? TermDaysId { get; set; }
        public FixedDiscounts FixedDiscount { get; set; }
        public bool VariableDiscount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IFormFile PhotoProof { get; set; }
        public IFormFile ESignature { get; set; }
        public List<UpdateFreebie> Freebies { get; set; }

        public class FixedDiscounts
        {
            public decimal? DiscountPercentage { get; set; }
        }
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
        
        public class ClientModeOfPayments
        {
            public int ModeOfPaymentId { get; set; }
        }
    }

    public class DirectRegisterResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string EmailAddress { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public Freebie Freebies { get; set; }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class Freebie
        {
            public int FreebieRequestId { get; set; }
            public string Status { get; set; }
            public int TransactionNumber { get; set; }
            public ICollection<FreebieItem> FreebieItems { get; set; }
        }

        public class FreebieItem
        {
            public int? Id { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string UOM { get; set; }
            public int? Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<DirectRegistrationCommand, Result>
    {

        private readonly ArcanaDbContext _context;
        private readonly Cloudinary _cloudinary;

        public Handler(ArcanaDbContext context, IOptions<CloudinaryOptions> options)
        {
            var account = new Account(
                options.Value.Cloudname,
                options.Value.ApiKey,
                options.Value.ApiSecret
            );
            _context = context;
            _cloudinary = new Cloudinary(account);
        }

        public async Task<Result> Handle(DirectRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            
            var existingClient = await _context.Clients.FirstOrDefaultAsync(
                x => x.Fullname == request.OwnersName &&
                     x.StoreType.Id == request.StoreTypeId &&
                     x.BusinessName == request.BusinessName,
                cancellationToken
            );

            var validateStoreType = await _context.StoreTypes.FirstOrDefaultAsync(x => x.Id == request.StoreTypeId, cancellationToken: cancellationToken);
            
            if (validateStoreType == null)
                throw new StoreTypeNotFoundException(request.StoreTypeId);

            var validateBookingCoverages =
                await _context.BookingCoverages.FirstOrDefaultAsync(x => x.Id == request.BookingCoverageId, cancellationToken: cancellationToken);
            
            if (validateBookingCoverages == null)
                throw new BookingCoverageNotFoundException(request.BookingCoverageId);

            var validateTermsAndCondition =
                await _context.Terms.FirstOrDefaultAsync(x => x.Id == request.TermsId, cancellationToken: cancellationToken);
            
            if (validateTermsAndCondition == null)
                throw new TermsNotFoundException(request.TermsId);

            if (existingClient != null)
                return ClientErrors.AlreadyExist(
                        existingClient.Fullname
                        );
            {
                var clientFreebies = new List<DirectRegisterResult.FreebieItem>();
                var freebiesResult = new DirectRegisterResult.Freebie(); 
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
                    ClusterId = request.ClusterId,
                    Freezer = request.Freezer,
                    CustomerType = request.TypeOfCustomer,
                    DirectDelivery = request.DirectDelivery,
                    BookingCoverageId = request.BookingCoverageId,
                    RegistrationStatus = Status.UnderReview,
                    Origin = Origin.Direct,
                    IsActive = true,
                    AddedBy = request.AddedBy,
                    Longitude = request.Longitude,
                    Latitude = request.Latitude
                };
                _context.Clients.Add(directClients);

                foreach (var modeOfPayment in request.ModeOfPayments)
                {
                    var validateModeOfPayment =
                        await _context.ModeOfPayments.FirstOrDefaultAsync(x => x.Id == modeOfPayment.ModeOfPaymentId, cancellationToken: cancellationToken);

                    if (validateModeOfPayment is null)
                    {
                        return ModeOfPaymentErrors.NotFound();
                    }
                    
                    var newPaymentMethod = new ClientModeOfPayment
                    {
                        ClientId = directClients.Id,
                        ModeOfPaymentId = modeOfPayment.ModeOfPaymentId,
                    };
                    _context.ClientModeOfPayments.Add(newPaymentMethod);
                }

                if (request.FixedDiscount.DiscountPercentage != null)
                {
                    var fixedDiscount = new FixedDiscounts
                    {
                        /*ClientId = directClients.Id,*/
                        DiscountPercentage = request.FixedDiscount.DiscountPercentage / 100
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
                    ApprovalType = Status.DirectRegistrationApproval,
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
                        return FreebieErrors.Exceed5Items();
                    }

                    if (request.Freebies.Select(x => x.ItemId).Distinct().Count() != request.Freebies.Count)
                    {
                        return FreebieErrors.CannotBeRepeated();
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
                            return ItemErrors.NotFound(item.ItemId);
                        }

                        if (existingRequest != null)
                        {
                            return FreebieErrors.AlreadyRequested(existingRequest.Items.ItemDescription);
                        }
                    }
                    
                    //Create approval for Freebies
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
                    
                    //Add new FreebieRequest
                    var freebieRequest = new FreebieRequest
                    {
                        ClientId = directClients.Id,
                        ApprovalsId = newApproval.Id,
                        Status = Status.ForReleasing,
                        IsDelivered = false,
                        RequestedBy = request.AddedBy
                    };
                    _context.FreebieRequests.Add(freebieRequest);

                    //Add the freebie items
                    foreach (var freebieItem in request.Freebies.Select(freebie => new FreebieItems
                             {
                                 RequestId = freebieRequest.Id,
                                 ItemId = freebie.ItemId,
                                 Quantity = 1
                             }))
                    {
                        await _context.FreebieItems.AddAsync(freebieItem, cancellationToken);
                        
                        //Get the item details inserted by Item Id
                        var itemDetails = await _context.Items
                            .Include(x => x.Uom)
                            .Where(i => i.Id == freebieItem.ItemId)
                            .Select(i => new { i.ItemCode, i.ItemDescription, i.Uom.UomCode })
                            .FirstOrDefaultAsync(cancellationToken);
                        
                        //Add the item details to be return
                        clientFreebies.Add(new DirectRegisterResult.FreebieItem
                        {
                          Id = freebieItem.Id,
                          ItemId = freebieItem.ItemId,
                          ItemCode = itemDetails.ItemCode,
                          ItemDescription = itemDetails.ItemDescription,
                          UOM = itemDetails.UomCode,
                          Quantity = freebieItem.Quantity
                        });
                    }

                    //Result for the added freebies
                    freebiesResult.FreebieRequestId = freebieRequest.Id;
                    freebiesResult.Status = freebieRequest.Status;
                    freebiesResult.TransactionNumber = freebieRequest.Id;
                    freebiesResult.FreebieItems = clientFreebies;
                    
                    //Released the freebies
                    var uploadTasks = new List<Task>();

                    if (request.PhotoProof is not null && request.ESignature is not null)
                    {
                        if (request.PhotoProof.Length > 0 || request.ESignature.Length > 0)
                        {
                            uploadTasks.Add(Task.Run(async () =>
                            {
                                await using var stream = request.PhotoProof.OpenReadStream();
                                await using var esignatureStream = request.ESignature.OpenReadStream();

                                var photoProofParams = new ImageUploadParams
                                {
                                    File = new FileDescription(request.PhotoProof.FileName, stream),
                                    PublicId = $"{WebUtility.UrlEncode(directClients.BusinessName)}/{request.PhotoProof.FileName}"
                                };

                                var eSignaturePhotoParams = new ImageUploadParams
                                {
                                    File = new FileDescription(request.ESignature.FileName, esignatureStream),
                                    PublicId = $"{WebUtility.UrlEncode(directClients.BusinessName)}/{request.ESignature.FileName}"
                                };


                                var photoProofUploadResult = await _cloudinary.UploadAsync(photoProofParams);
                                var eSignatureUploadResult = await _cloudinary.UploadAsync(eSignaturePhotoParams);

                                if (photoProofUploadResult.Error != null)
                                {
                                    throw new Exception(photoProofUploadResult.Error.Message);
                                }

                                if (eSignatureUploadResult.Error != null)
                                {
                                    throw new Exception(eSignatureUploadResult.Error.Message);
                                }
                                
                                freebieRequest.Status = Status.Released;
                                freebieRequest.IsDelivered = true;
                                freebieRequest.PhotoProofPath = photoProofUploadResult.SecureUrl.ToString();
                                freebieRequest.ESignaturePath = eSignatureUploadResult.SecureUrl.ToString();
                            }, cancellationToken));
                        }
                    }
                    await Task.WhenAll(uploadTasks);
                }
                
                var approvers = await _context.Approvers
                    .Where(x => x.ModuleName == Modules.RegistrationApproval)
                    .OrderBy(x => x.Level)
                    .ToListAsync(cancellationToken);
                
                if (!approvers.Any())
                {
                    return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
                }

                var newRequest = new Request(
                    Modules.RegistrationApproval,
                    request.AddedBy,
                    approvers.First().UserId,
                    Status.UnderReview
                );
                
                await _context.Requests.AddAsync(newRequest, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
                         {
                             ApproverId = approver.UserId,
                             RequestId = newRequest.Id,
                             Level = approver.Level,
                         }))
                {
                    _context.RequestApprovers.Add(newRequestApprover);
                }
                
                directClients.RequestId = newRequest.Id;

                var result = new DirectRegisterResult
                {
                    Id = directClients.Id,
                    OwnersName = directClients.Fullname,
                    EmailAddress = directClients.EmailAddress,
                    OwnersAddress = new DirectRegisterResult.OwnersAddressCollection
                    {
                        HouseNumber = directClients.OwnersAddress.HouseNumber,
                        StreetName = directClients.OwnersAddress.StreetName,
                        BarangayName = directClients.OwnersAddress.Barangay,
                        City = directClients.OwnersAddress.City,
                        Province = directClients.OwnersAddress.Province
                    },
                    PhoneNumber = directClients.PhoneNumber,
                    BusinessName = directClients.BusinessName,
                    Freebies = freebiesResult
                };

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(result);
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