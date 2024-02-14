using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.Prospecting.Approved;

[Route("api/Prospecting")]
[ApiController]
public class GetAllApprovedProspectAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllApprovedProspectAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllApprovedProspect")]
    public async Task<IActionResult> GetAllRequestedProspect([FromQuery] GetAllApprovedProspectQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity)
            {
                if (IdentityHelper.TryGetUserId(identity, out var userId))
                {
                    query.AddedBy = userId;
                }

                var role = IdentityHelper.GetRole(identity);
                if (!string.IsNullOrEmpty(role))
                {
                    query.Role = role;
                }
            }

            var approvedProspect = await _mediator.Send(query);

            Response.AddPaginationHeader(
                approvedProspect.CurrentPage,
                approvedProspect.PageSize,
                approvedProspect.TotalCount,
                approvedProspect.TotalPages,
                approvedProspect.HasPreviousPage,
                approvedProspect.HasNextPage
            );

            var result = new
            {
                requestedProspect = approvedProspect,
                approvedProspect.CurrentPage,
                approvedProspect.PageSize,
                approvedProspect.TotalCount,
                approvedProspect.TotalPages,
                approvedProspect.HasPreviousPage,
                approvedProspect.HasNextPage

            };

            var successResult = Result.Success(result);
            
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllApprovedProspectQuery : UserParams, IRequest<PagedList<GetAllApprovedProspectResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string StoreType { get; set; }
        public string FreebieStatus { get; set; }
        public string RegistrationStatus { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int AddedBy { get; set; }
        public string Role { get; set; }
    }

    public class GetAllApprovedProspectResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AddedBy { get; set; }
        public string Origin { get; set; }
        public string BusinessName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string StoreType { get; set; }
        public bool IsActive { get; set; }
        public string RegistrationStatus { get; set; }
        public IEnumerable<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int FreebieRequestId { get; set; }
            public string Status { get; set; }
            public int TransactionNumber { get; set; }
            public string ESignature { get; set; }
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

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }
        
    }

    public class Handler : IRequestHandler<GetAllApprovedProspectQuery, PagedList<GetAllApprovedProspectResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllApprovedProspectResult>> Handle(GetAllApprovedProspectQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> approvedProspect = _context.Clients
                .Include(x => x.AddedByUser)
                .Include(x => x.OwnersAddress)
                .Include(x => x.FreebiesRequests)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .ThenInclude(x => x.Uom)
                .Include(x => x.StoreType)
                .Where(x => x.RegistrationStatus != Status.Approved &&
                            x.RegistrationStatus != Status.UnderReview &&
                            x.RegistrationStatus != Status.Rejected);

            if (request.Role is not Roles.Admin)
            {
                approvedProspect = approvedProspect.Where(x => x.AddedBy == request.AddedBy);
            }

            if (!string.IsNullOrWhiteSpace(request.RegistrationStatus) && request.RegistrationStatus == Status.Voided)
            {

                approvedProspect = GetClientByRegistrationStatus(request, approvedProspect);
                
                var voidedResults = approvedProspect.Select(x => x.ToGetGetAllApprovedProspectResult());
                
                return await PagedList<GetAllApprovedProspectResult>.CreateAsync(voidedResults, request.PageNumber,
                    request.PageSize);
            }

            approvedProspect = GetFreebiesByStatus(request, approvedProspect);
            
            approvedProspect = GetClientByRegistrationStatus(request, approvedProspect);

            approvedProspect = GetClientByStoreType(request, approvedProspect);

            approvedProspect = SearchClientByFullname(request, approvedProspect);

            approvedProspect = FilterByStatus(request, approvedProspect);

            approvedProspect = request.SortOrder?.ToLower() == "desc"
                ? approvedProspect.OrderByDescending(GetSortProperty(request))
                : approvedProspect.OrderBy(GetSortProperty(request));

            var result = approvedProspect.Select(x => x.ToGetGetAllApprovedProspectResult());

            return await PagedList<GetAllApprovedProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }

        private static Expression<Func<Domain.Clients, object>> GetSortProperty(GetAllApprovedProspectQuery request) =>
            request.SortColumn?.ToLower() switch
            {
                "name" => prospects => prospects.Fullname,
                "business_name" => prospects => prospects.BusinessName,
                "owners_address" => prospects => prospects.OwnersAddress.City,
                "business_address" => prospects => prospects.BusinessAddress.City,
                _ => prospect => prospect.Id
            };

        private static IQueryable<Domain.Clients> FilterByStatus(GetAllApprovedProspectQuery request,
            IQueryable<Domain.Clients> approvedProspect)
        {
            if (request.Status != null)
            {
                approvedProspect = approvedProspect.Where(x =>
                    x.IsActive == request.Status);
            }

            return approvedProspect;
        }

        private static IQueryable<Domain.Clients> SearchClientByFullname(GetAllApprovedProspectQuery request,
            IQueryable<Domain.Clients> approvedProspect)
        {
            if (string.IsNullOrEmpty(request.Search)) return approvedProspect;

            request.PageNumber = 1;
            approvedProspect = approvedProspect.Where(x =>
                x.Fullname.Contains(request.Search));

            return approvedProspect;
        }

        private static IQueryable<Domain.Clients> GetClientByStoreType(GetAllApprovedProspectQuery request,
            IQueryable<Domain.Clients> approvedProspect)
        {
            if (!string.IsNullOrEmpty(request.StoreType))
            {
                approvedProspect =
                    approvedProspect.Where(x => x.StoreType.StoreTypeName.Contains(request.StoreType));
            }

            return approvedProspect;
        }
        
        private static IQueryable<Domain.Clients> GetClientByRegistrationStatus(GetAllApprovedProspectQuery request,
            IQueryable<Domain.Clients> approvedProspect)
        {
            if (string.IsNullOrEmpty(request.RegistrationStatus)) return approvedProspect;
            approvedProspect = request.RegistrationStatus == Status.Voided ? approvedProspect.Where(x => x.RegistrationStatus == Status.Voided && x.RequestId == null) :
                // Handle other registration statuses
                approvedProspect.Where(x => x.RegistrationStatus == request.RegistrationStatus);

            approvedProspect = approvedProspect.Where(x => x.Request == null);

            return approvedProspect;
        }

        private static IQueryable<Domain.Clients> GetFreebiesByStatus(GetAllApprovedProspectQuery request,
            IQueryable<Domain.Clients> approvedProspect)
        {
            if (request.FreebieStatus != null)
            {
                approvedProspect = approvedProspect
                    .Where(client => client.FreebiesRequests.OrderByDescending(req => req.CreatedAt)
                        .FirstOrDefault().Status == request.FreebieStatus);
            }
            else
            {
                approvedProspect = approvedProspect.Where(client =>
                    !client.FreebiesRequests.Any() ||
                    client.FreebiesRequests.OrderByDescending(req => req.CreatedAt)
                        .FirstOrDefault().Status == Status.Rejected);
            }

            return approvedProspect;
        }
    }
}