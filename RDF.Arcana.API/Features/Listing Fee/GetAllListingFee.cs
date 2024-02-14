using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee")]
public class GetAllListingFee : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<GetAllListingFeeQuery> _validator;

    public GetAllListingFee(IMediator mediator, IValidator<GetAllListingFeeQuery> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet("GetAllListingFee")]
    public async Task<IActionResult> GetAllListingFees([FromQuery] GetAllListingFeeQuery query)
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
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            var listingFees = await _mediator.Send(query);

            Response.AddPaginationHeader(
                listingFees.CurrentPage,
                listingFees.PageSize,
                listingFees.TotalCount,
                listingFees.TotalPages,
                listingFees.HasPreviousPage,
                listingFees.HasNextPage
            );

            var result = new
            {
                listingFees,
                listingFees.CurrentPage,
                listingFees.PageSize,
                listingFees.TotalCount,
                listingFees.TotalPages,
                listingFees.HasPreviousPage,
                listingFees.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllListingFeeQuery : UserParams, IRequest<PagedList<ClientsWithListingFee>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string RoleName { get; set; }
        public int AccessBy { get; set; }
        public string ListingFeeStatus { get; set; }
    }

    public class ClientsWithListingFee
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string RegistrationStatus { get; set; }
        public string BusinessName { get; set; }
        public int ListingFeeId { get; set; }
        public int RequestId { get; set; }
        public string Status { get; set; }
        public string RequestedBy { get; set; }
        public decimal Total { get; set; }
        public string CreatedAt { get; set; }
        public string CancellationReason { get; set; }
        public IEnumerable<ListingItem> ListingItems { get; set; }
        public IEnumerable<ListingFeeApprovalHistory> ListingFeeApprovalHistories { get; set; }
        public IEnumerable<RequestApproversForListingFee> Approvers { get; set; }

        public class ListingItem
        {
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
            public int Quantity { get; set; }
        }
        public class ListingFeeApprovalHistory
        {
            public string Module { get; set; }
            public string Approver { get; set; }
            public int Level { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Status { get; set; }
            public string Reason { get; set; }
        }
        
        public class RequestApproversForListingFee
        {
            public string Name { get; set; }
            public int Level { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllListingFeeQuery, PagedList<ClientsWithListingFee>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<ClientsWithListingFee>> Handle(GetAllListingFeeQuery request,
            CancellationToken cancellationToken)
        {
            var listingFees = _context.ListingFees
                .Include(x => x.Client)
                .AsSplitQuery()
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.Approvals)
                .Include(x => x.RequestedByUser)
                .AsSplitQuery()
                .Include(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.Uom)
                .AsSingleQuery();

            var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AccessBy, cancellationToken);

            if (!string.IsNullOrEmpty(request.Search))

            {
                listingFees = listingFees.Where(x =>
                    x.Client.BusinessName.Contains(request.Search) || x.Client.Fullname.Contains(request.Search));
            }

            switch (request.RoleName)
            {
                case var roleName when roleName.Contains(Roles.Approver) &&
                      !string.IsNullOrWhiteSpace(request.ListingFeeStatus) &&
                      request.ListingFeeStatus.ToLower() != Status.UnderReview.ToLower():
                    listingFees = listingFees.Where(lf => lf.Request.Approvals.Any(x =>
                        x.Status == request.ListingFeeStatus && x.ApproverId == request.AccessBy && x.IsActive));
                    break;

                case var roleName when roleName.Contains(Roles.Approver):
                    listingFees = listingFees.Where(lf =>
                        lf.Request.Status == request.ListingFeeStatus && lf.Request.CurrentApproverId == request.AccessBy);
                    break;

                case Roles.Cdo:

                    if (userClusters is null)
                    {
                        listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus && x.Client.ClusterId == userClusters.ClusterId);
                        break;
                    }

                    listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus);
                    break;

                case Roles.Admin:
                    listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus);
                    break;

                default:
                    // No additional filtering for other roles
                    break;
            }
           

            if (request.RoleName.Contains(Roles.Approver) && request.ListingFeeStatus == Status.UnderReview)
            {
                listingFees = listingFees.Where(x => x.Request.CurrentApproverId == request.AccessBy);
            }

            if (request.Status != null)
            {
                listingFees = listingFees.Where(x => x.IsActive == request.Status);
            }

            var result = listingFees.Select(listingFee => new ClientsWithListingFee
            {
                ClientId = listingFee.ClientId,
                ClientName = listingFee.Client.Fullname,
                RegistrationStatus = listingFee.Client.RegistrationStatus,
                BusinessName = listingFee.Client.BusinessName,
                CreatedAt = listingFee.CratedAt.ToString("MM/dd/yyyy HH:mm:ss"),
                ListingFeeId = listingFee.Id,
                RequestId = listingFee.RequestId,
                Status = listingFee.Status,
                RequestedBy = listingFee.RequestedByUser.Fullname,
                Total = listingFee.Total,
                ListingItems = listingFee.ListingFeeItems.Select(li =>
                    new ClientsWithListingFee.ListingItem
                    {
                        ItemId = li.ItemId,
                        ItemCode = li.Item.ItemCode,
                        ItemDescription = li.Item.ItemDescription,
                        Uom = li.Item.Uom.UomCode,
                        Sku = li.Sku,
                        UnitCost = li.UnitCost
                    }).ToList(),
                ListingFeeApprovalHistories = listingFee.Request.Approvals.OrderByDescending(a => a.CreatedAt)
                    .Select( a => new ClientsWithListingFee.ListingFeeApprovalHistory
                    {
                        Module = a.Request.Module,
                        Approver = a.Approver.Fullname,
                        Level = a.Approver.Approver.FirstOrDefault().Level,
                        Reason = a.Request.Approvals.FirstOrDefault().Reason,
                        CreatedAt = a.CreatedAt,
                        Status = a.Status,
                    }),
                Approvers = listingFee.Request.RequestApprovers.Select(x => new ClientsWithListingFee.RequestApproversForListingFee
                {
                    Name = x.Approver.Fullname,
                    Level = x.Level
                })
            }).OrderBy(x => x.ClientId);

            return await PagedList<ClientsWithListingFee>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}