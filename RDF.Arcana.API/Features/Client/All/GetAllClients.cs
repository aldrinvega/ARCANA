using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using Result = RDF.Arcana.API.Common.Result;

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

    public class  GetAllClientsQuery : UserParams, IRequest<PagedList<GetAllClientResult>>
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
        public string RegistrationStatus { get; set; }
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
        public int? ClusterId { get; set; }
        public string ClusterName { get; set; }
        public int? PriceModeId { get; set; }
        public string PriceModeName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Requestor { get; set; }
        public string RequestorMobileNumber { get; set; }
        public string RequestorEmailAddress { get; set; }
        public string Terms { get; set; }
        public string CurrentApprover { get; set; }
        public string CurrentApproverPhoneNumber { get; set; }
        public string NextApprover { get; set; }
        public string NextApproverPhoneNumber { get; set; }

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
            var regularClients = _context.Clients
                .AsSplitQuery()
                .Include(t => t.Term)
                .ThenInclude(t => t.Terms)
                .AsSplitQuery()
                .Include(abu => abu.AddedByUser)
                .AsSplitQuery()
                .Include(pm => pm.PriceMode)
                .AsSplitQuery()
                .Include(st => st.StoreType)
                .AsSplitQuery()
                .Include(fd => fd.FixedDiscounts)
                .AsSplitQuery()
                .Include(ba => ba.BusinessAddress)
                .AsSplitQuery()
                .Include(oa => oa.OwnersAddress)
                .AsSplitQuery()
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.CurrentApprover)
                .AsSplitQuery()
                .AsSingleQuery();

            if (!string.IsNullOrEmpty(request.Search))
            {
                regularClients = regularClients.Where(x =>
                    x.BusinessName.Contains(request.Search) ||
                    x.StoreType.StoreTypeName.Contains(request.Search) ||
                    x.Fullname.Contains(request.Search)
                );
            }

            switch (request.RoleName)
            {
                // To Separate Under Review & Approved Request between CDO and Approver including Admin
                // (Request and Approval)
                //To get the Approved Request, Approval table need to access and the role need to be Approver
                case var roleName when roleName.Contains(Roles.Approver) &&
                      (!string.IsNullOrWhiteSpace(request.RegistrationStatus) &&
                      !string.Equals(request.RegistrationStatus, Status.UnderReview, StringComparison.CurrentCultureIgnoreCase) &&
                      !string.Equals(request.RegistrationStatus, Status.Voided, StringComparison.CurrentCultureIgnoreCase)):
                    regularClients = regularClients.Where(clients => clients.Request.Approvals.Any(x =>
                        x.Status == request.RegistrationStatus && x.ApproverId == request.AccessBy &&
                        x.IsActive == true));
                    break;
                case var roleName when roleName.Contains(Roles.Approver):
                    regularClients = regularClients.Where(clients =>
                        clients.Request.Status == request.RegistrationStatus &&
                        clients.Request.CurrentApproverId == request.AccessBy);
                    break;
                case Roles.Cdo:
                {
                    
                    var userClusters = await _context.CdoClusters
                        .FirstOrDefaultAsync(x => 
                            x.UserId == request.AccessBy, 
                            cancellationToken);

                        if (request.RegistrationStatus is Status.ForReleasing or Status.Released or Status.Voided)
                        {

                            regularClients = regularClients
                                .Where(x => x.AddedBy == request.AccessBy &&
                                            x.RegistrationStatus == request.RegistrationStatus &&
                                            x.RequestId != null);

                            //Get the result

                            var voidedResults = regularClients.Select(client => new GetAllClientResult
                            {
                                Id = client.Id,
                                CreatedAt = client.CreatedAt,
                                RequestId = client.RequestId,
                                Origin = client.Origin,
                                OwnersName = client.Fullname,
                                RegistrationStatus = client.RegistrationStatus,
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
                                StoreType = client.StoreType.StoreTypeName,
                                AuthorizedRepresentative = client.RepresentativeName,
                                AuthorizedRepresentativePosition = client.RepresentativePosition,
                                ClusterId = client.ClusterId,
                                ClusterName = client.Cluster.ClusterType,
                                PriceModeId = client.PriceModeId,
                                PriceModeName = client.PriceMode.PriceModeDescription ?? null,
                                Longitude = client.Longitude,
                                Latitude = client.Latitude,
                                Requestor = client.AddedByUser.Fullname,
                                RequestorMobileNumber = client.AddedByUser.MobileNumber,
                                Terms = client.Term.Terms.TermType,
                                CurrentApprover = client.Request.CurrentApprover.Fullname,
                                CurrentApproverPhoneNumber = client.Request.CurrentApprover.MobileNumber
                            });

                            voidedResults = voidedResults.OrderBy(r => r.Id);

                            //Return the result
                            return await PagedList<GetAllClientResult>.CreateAsync(voidedResults, request.PageNumber, request.PageSize);

                        }
                        
                        if (userClusters is null)
                        {
                            regularClients = regularClients
                            .Where(x => x.RegistrationStatus == request.RegistrationStatus && x.RequestId != null);
                            break;
                        }

                    regularClients = regularClients.Where(x => 
                        x.RegistrationStatus == request.RegistrationStatus && 
                        x.ClusterId == userClusters.ClusterId);
                    break;
                }

                case Roles.Admin:
                {
                    regularClients = regularClients
                        .Where(x =>  x.RegistrationStatus == request.RegistrationStatus &&
                                            x.RequestId != null);
                    break;
                }
            }

            //Get all the under review request for the Approver
            //It will access the request table where status is Under Review
            //And CurrentApproverId is the Logged In user (Approver)
            if (request.RoleName.Contains(Roles.Approver) && request.RegistrationStatus == Status.UnderReview)
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

            //For GAS and Audit
            if (request.RoleName.ToLower() == Roles.GAS || request.RoleName.ToLower() == Roles.Audit)
            {
                regularClients = regularClients.Where(c => c.RegistrationStatus == Status.Approved 
                            && c.IsActive == true);        
            }

            //Get the result
                var personalInformation = regularClients.Select(client => new GetAllClientResult
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
                    StoreType = client.StoreType.StoreTypeName,
                    AuthorizedRepresentative = client.RepresentativeName,
                    AuthorizedRepresentativePosition = client.RepresentativePosition,
                    ClusterId = client.ClusterId,
                    ClusterName = client.Cluster.ClusterType,
                    PriceModeId = client.PriceModeId,
                    PriceModeName = client.PriceMode.PriceModeDescription ?? null,
                    RegistrationStatus = client.RegistrationStatus,
                    Longitude = client.Longitude,
                    Latitude = client.Latitude,
                    Requestor = client.AddedByUser.Fullname,
                    RequestorMobileNumber = client.AddedByUser.MobileNumber,
                    // RequestorEmailAddress = client.AddedByUser.
                    Terms = client.Term.Terms.TermType,
                    CurrentApprover = client.Request.CurrentApprover.Fullname,
                    CurrentApproverPhoneNumber = client.Request.CurrentApprover.MobileNumber,
                    NextApprover = client.Request.NextApprover.Fullname,
                    NextApproverPhoneNumber = client.Request.NextApprover.MobileNumber
                });

                personalInformation = personalInformation.OrderBy(r => r.Id);

                //Return the result
                return await PagedList<GetAllClientResult>.CreateAsync(personalInformation, request.PageNumber, request.PageSize);
        }
    }
}