using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/Clients")]
[ApiController]
public class GetAllClients : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<GetAllClientsQuery> _validator;

    public GetAllClients(IMediator mediator, IValidator<GetAllClientsQuery> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet("GetAllClients")]
    public async Task<IActionResult> GetAllDirectRegistrationClient(
        [FromQuery] GetAllClientsQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                query.AccessBy = userId;

                var roleClaim = identity.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);

                if (roleClaim != null)
                {
                    query.RoleName = roleClaim.Value;
                }
            }

            var regularClient = await _mediator.Send(query);
            Response.AddPaginationHeader(
                regularClient.CurrentPage,
                regularClient.PageSize,
                regularClient.TotalCount,
                regularClient.TotalPages,
                regularClient.HasPreviousPage,
                regularClient.HasNextPage
            );

            var result = new
            {
                regularClient,
                regularClient.CurrentPage,
                regularClient.PageSize,
                regularClient.TotalCount,
                regularClient.TotalPages,
                regularClient.HasPreviousPage,
                regularClient.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllClientsQuery : UserParams, IRequest<PagedList<GetAllClientResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string RegistrationStatus { get; set; }
        public string StoreType { get; set; }
        public string Origin { get; set; }
        public int AccessBy { get; set; }
        public string RoleName { get; set; }
    }

    public sealed record GetAllClientResult
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? RequestId { get; set; }
        public string OwnersName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string TinNumber { get; set; }
        public string BusinessName { get; set; }
        public string Origin { get; set; }
        public BusinessAddressCollection BusinessAddress { get; set; }
        public string StoreType { get; set; }
        public string AuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativePosition { get; set; }
        public int Cluster { get; set; }
        public bool Freezer { get; set; }
        public string TypeOfCustomer { get; set; }
        public bool? DirectDelivery { get; set; }
        public string BookingCoverage { get; set; }
        public IEnumerable<ModeOfPayment> ModeOfPayments { get; set; }
        public ClientTerms Terms { get; set; }
        public FixedDiscounts FixedDiscount { get; set; }
        public bool? VariableDiscount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string RequestedBy { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
        public IEnumerable<UpdateHistory> UpdateHistories { get; set; }
        public IEnumerable<ClientApprovalHistory> ClientApprovalHistories { get; set; }
        public IEnumerable<FreebiesCollection> Freebies { get; set; }
        public IEnumerable<ListingFeeCollection> ListingFees { get; set; }

        public class ModeOfPayment
        {
            public int Id { get; set; }
        }
        public class FixedDiscounts
        {
            public decimal? DiscountPercentage { get; set; }
        }
        public class Attachment
        {
            public int DocumentId { get; set; }
            public string DocumentLink { get; set; }
            public string DocumentType { get; set; }
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

        public class ClientTerms
        {
            public int TermId { get; set; }
            public string Term { get; set; }
            public int? CreditLimit { get; set; }
            public int? TermDays { get; set; }
            public int? TermDaysId { get; set; }
        }
        public class ClientApprovalHistory
        {
            public string Module { get; set; }
            public string Approver { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; }
            public int? Level { get; set; }
            public string Reason { get; set; }
        }
        
        public class UpdateHistory
        {
            public string Module { get; set; }
            public DateTime UpdatedAt { get; set; }
        }

        public class FreebiesCollection
        {
            public int? TransactionNumber { get; set; }
            public string Status { get; set; }
            public string ESignature { get; set; }

            public int? FreebieRequestId { get; set; }
            public IEnumerable<Items> Freebies { get; set; }
        }
            
        public class Items
        {
            public int? Id { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public int? Quantity { get; set; }
        }

        public class ListingFeeCollection
        {
            public int Id { get; set; }
            public int RequestId { get; set; }
            public decimal Total { get; set; }
            public string Status { get; set; }
            public string ApprovalDate { get; set; }
            public IEnumerable<ListingItems> ListingItems { get; set; }
        }

        public class ListingItems
        {
            public int? Id { get; set; }
            public string ItemCode { get; set; }
            public int Sku { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public decimal? UnitCost { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllClientsQuery, PagedList<GetAllClientResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllClientResult>> Handle(GetAllClientsQuery request,
            CancellationToken cancellationToken)
        {
            var regularClients = _context.Clients.AsNoTracking();
                /*.AsSplitQuery()
                .Include(mop => mop.ClientModeOfPayment)
                .Include(abu => abu.AddedByUser)
                .Include(rq => rq.Request)
                .ThenInclude(user => user.Requestor)
                .Include(rq => rq.Request)
                .ThenInclude(ah => ah.UpdateRequestTrails)
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.Approvals)
                .ThenInclude(cap => cap.Approver)
                .Include(st => st.StoreType)
                .Include(fd => fd.FixedDiscounts)
                .Include(to => to.Term)
                .ThenInclude(tt => tt.Terms)
                .Include(to => to.Term)
                .ThenInclude(td => td.TermDays)
                .Include(ba => ba.BusinessAddress)
                .Include(oa => oa.OwnersAddress)
                .Include(bc => bc.BookingCoverages)
                .Include(fr => fr.FreebiesRequests)
                .ThenInclude(fi => fi.FreebieItems)
                .ThenInclude(item => item.Items)
                .ThenInclude(uom => uom.Uom)
                .Include(lf => lf.ListingFees)
                .ThenInclude(li => li.ListingFeeItems)
                .ThenInclude(item => item.Item)
                .ThenInclude(uom => uom.Uom)
                .Include(cd => cd.ClientDocuments)
                .AsSingleQuery();*/
                
            var user = await _context.Users
                    .Include(cluster => cluster.CdoCluster)
                    .FirstOrDefaultAsync(user => user.Id == request.AccessBy, cancellationToken);

            if (!string.IsNullOrEmpty(request.Search))
            {
                regularClients = regularClients.Where(x =>
                    x.BusinessName.Contains(request.Search) ||
                    x.StoreType.StoreTypeName.Contains(request.Search) ||
                    x.Fullname.Contains(request.Search)
                );
            }
            // To Separate Under Review & Approved Request between CDO and Approver including Admin
            // (Request and Approval)
            //To get the Approved Request, Approval table need to access and the role need to be Approver
            if (request.RoleName == Roles.Approver && (!string.IsNullOrWhiteSpace(request.RegistrationStatus) &&
                                                       request.RegistrationStatus.ToLower() !=
                                                       Status.UnderReview.ToLower()))
            {
                regularClients = regularClients.Where(clients => clients.Request.Approvals.Any(x =>
                    x.Status == request.RegistrationStatus && x.ApproverId == request.AccessBy &&
                    x.IsActive == true));
            }
                
            else if (request.RoleName == Roles.Approver)
            {
                regularClients = regularClients.Where(clients =>
                    clients.Request.Status == request.RegistrationStatus &&
                    clients.Request.CurrentApproverId == request.AccessBy);
            }
            
            else if (request.RoleName is Roles.Admin or Roles.Cdo)
            {
                var userClusters = user?.CdoCluster?.Select(cluster => cluster.ClusterId);

                regularClients = regularClients
                    .Where(x => userClusters.Contains(x.ClusterId) && x.RegistrationStatus == request.RegistrationStatus);
            }

            //Get all the under review request for the Approver
            //It will access the request table where status is Under Review
            //And CurrentApproverId is the Logged In user (Approver)

            if (request.RoleName is Roles.Approver && request.RegistrationStatus == Status.UnderReview)
            {
                regularClients = regularClients.Where(x => x.Request.CurrentApproverId == request.AccessBy);
            }
            
            //Filter based on origin (Prospect or Direct)
            if (request.Origin != null)
            {
                regularClients = regularClients.Where(x => x.Origin == request.Origin);
            }
            
            //Filter based on StoreType or BusinessType
            if (!string.IsNullOrEmpty(request.StoreType))
            {
                regularClients = regularClients.Where(x => x.StoreType.StoreTypeName == request.StoreType);
            }
            
            //Filter based on Status
            if (request.Status != null)
            {
                regularClients = regularClients.Where(x => x.IsActive == request.Status);
            }
            
            //Get the result

            var result = regularClients.Select(client => new GetAllClientResult
            {
                Id = client.Id,
                CreatedAt = client.CreatedAt,
                RequestId = client.RequestId,
                Origin = client.Origin,
                OwnersName = client.Fullname,
                OwnersAddress = client.OwnersAddress != null
                    ? new GetAllClientResult.OwnersAddressCollection
                    {
                        HouseNumber = client.OwnersAddress.HouseNumber,
                        StreetName = client.OwnersAddress.StreetName,
                        BarangayName = client.OwnersAddress.Barangay,
                        City = client.OwnersAddress.City,
                        Province = client.OwnersAddress.Province
                    }
                    : null,
                PhoneNumber = client.PhoneNumber,
                EmailAddress = client.EmailAddress,
                DateOfBirth = client.DateOfBirthDB,
                TinNumber = client.TinNumber,
                BusinessName = client.BusinessName,
                BusinessAddress = client.BusinessAddress != null
                    ? new GetAllClientResult.BusinessAddressCollection
                    {
                        HouseNumber = client.BusinessAddress.HouseNumber,
                        StreetName = client.BusinessAddress.StreetName,
                        BarangayName = client.BusinessAddress.Barangay,
                        City = client.BusinessAddress.City,
                        Province = client.BusinessAddress.Province
                    }
                    : null,
                ModeOfPayments = client.ClientModeOfPayment.Select(mop => new GetAllClientResult.ModeOfPayment
                {
                    Id = mop.ModeOfPaymentId
                }),
                StoreType = client.StoreType.StoreTypeName,
                AuthorizedRepresentative = client.RepresentativeName,
                AuthorizedRepresentativePosition = client.RepresentativePosition,
                Cluster = client.ClusterId,
                Freezer = client.Freezer,
                TypeOfCustomer = client.CustomerType,
                DirectDelivery = client.DirectDelivery,
                BookingCoverage = client.BookingCoverages.BookingCoverage,
                Terms = client.Term != null
                    ? new GetAllClientResult.ClientTerms
                    {
                        TermId = client.Term.TermsId,
                        Term = client.Term.Terms.TermType,
                        CreditLimit = client.Term.CreditLimit,
                        TermDaysId = client.Term.TermDaysId,
                        TermDays = client.Term.TermDays.Days
                    }
                    : null,
                FixedDiscount = client.FixedDiscounts != null
                    ? new GetAllClientResult.FixedDiscounts
                    {
                        
                        DiscountPercentage = client.FixedDiscounts.DiscountPercentage
                    }
                    : null,
                VariableDiscount = client.VariableDiscount,
                Longitude = client.Longitude,
                Latitude = client.Latitude,
                RequestedBy = client.AddedByUser.Fullname,
                Attachments = client.ClientDocuments.Select(cd =>
                    new GetAllClientResult.Attachment
                    {
                        DocumentId = cd.Id,
                        DocumentLink = cd.DocumentPath,
                        DocumentType = cd.DocumentType
                    }),
                ClientApprovalHistories = client.Request.Approvals == null ? null :
                    client.Request.Approvals.OrderByDescending(a => a.CreatedAt)
                    .Select( a => new GetAllClientResult.ClientApprovalHistory
                    {
                        Module = a.Request.Module,
                        Approver = a.Approver.Fullname,
                        CreatedAt = a.CreatedAt,
                        Status = a.Status,
                        Level = a.Approver.Approver.FirstOrDefault().Level,
                        Reason = a.Reason
                        
                    }),
                UpdateHistories = client.Request.UpdateRequestTrails == null ? null :
                    client.Request.UpdateRequestTrails.Select(uh => new GetAllClientResult.UpdateHistory
                    {
                        Module = uh.ModuleName,
                        UpdatedAt = uh.UpdatedAt
                    }),
                Freebies = client.FreebiesRequests
                    .Where(fr => fr.Status == Status.Approved || fr.Status == Status.Released)
                    .Select(x => new GetAllClientResult.FreebiesCollection
                {
                    TransactionNumber = x.Id,
                    FreebieRequestId = x.Id,
                    Status = x.Status,
                    ESignature = x.ESignaturePath,
                    Freebies = x.FreebieItems.Select(x => new GetAllClientResult.Items
                    {
                        Id = x.Id,
                        ItemCode = x.Items.ItemCode,
                        ItemDescription = x.Items.ItemDescription,
                            Uom = x.Items.Uom.UomCode,
                        Quantity = x.Quantity
                    })
                }),
                ListingFees = client.ListingFees.Select( lf => new GetAllClientResult.ListingFeeCollection
                {
                    Id = lf.Id,
                    RequestId = lf.RequestId,
                    Total = lf.Total,
                    Status = lf.Status,
                    ApprovalDate = lf.ApprovalDate.ToString("MM/dd/yyyy HH:mm:ss"),
                    ListingItems = lf.ListingFeeItems.Select(lfi => new GetAllClientResult.ListingItems
                    {
                        Id = lfi.Id,
                        ItemCode = lfi.Item.ItemCode,
                        ItemDescription = lfi.Item.ItemDescription,
                        Sku = lfi.Sku,
                        UnitCost = lfi.UnitCost,
                        Uom = lfi.Item.Uom.UomCode
                    })
                })
            });

            result = result.OrderBy(r => r.Id);
            //Return the result
            return await PagedList<GetAllClientResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}