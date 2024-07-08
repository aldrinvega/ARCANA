using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

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
        public decimal Total { get; set; }
        public string CreatedAt { get; set; }
        public string Requestor { get; set; }
        public string RequestorMobileNumber { get; set; }
        public string CancellationReason { get; set; }
        public string CurrentApprover { get; set; }
        public string CurrentApproverNumber { get; set; }
        public string NextApprover { get; set; }
        public string NextApproverMobileNumber { get; set; }
        public decimal RemainingBalance { get; set; }
        public IEnumerable<ListingItem> ListingItems { get; set; }
        public IEnumerable<ListingFeeApprovalHistory> ListingFeeApprovalHistories { get; set; }
        //public IEnumerable<RequestApproversForListingFee> Approvers { get; set; }

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
    public class ListingFeeTotal
    {
        public int ClientId { get; set; }
        public decimal Total { get; set; }
    }

    public class ListingFeePayment
    {
        public int ClientId { get; set; }
        public decimal AmountSum { get; set; }
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
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.CurrentApprover)
                .Include(rq => rq.Request)
                .ThenInclude(ap => ap.NextApprover)
                .Include(x => x.RequestedByUser)
                .AsSplitQuery()
                .Include(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.Uom)
                .AsSingleQuery();

            var userClusters = await _context.CdoClusters.FirstOrDefaultAsync(x => x.UserId == request.AccessBy, cancellationToken);

            var totalListingFee = await _context.ListingFees
                .Where(lf => lf.IsActive)
                .GroupBy(lf => lf.ClientId)
                .Select(g => new ListingFeeTotal { ClientId = g.Key, Total = g.Sum(x => x.Total) })
                .ToListAsync(cancellationToken);

            var userListingFeePayments = await _context.Transactions
                .Where(pt => pt.PaymentTransactions.Any(p => p.PaymentMethod == PaymentMethods.ListingFee))
                .GroupBy(x => x.ClientId)
                .Select(x => new ListingFeePayment { ClientId = x.Key, AmountSum = x.SelectMany(t => t.PaymentTransactions)
                    .Where(p => p.PaymentMethod == PaymentMethods.ListingFee)
                    .Sum(p => p.TotalAmountReceived) })
                .ToListAsync(cancellationToken);

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
                        x.Status == request.ListingFeeStatus && x.ApproverId == request.AccessBy && x.IsActive) && lf.Client.RegistrationStatus == Status.Approved);
                    break;

                case var roleName when roleName.Contains(Roles.Approver):
                    listingFees = listingFees.Where(lf =>
                        lf.Request.Status == request.ListingFeeStatus && lf.Request.CurrentApproverId == request.AccessBy && lf.Client.RegistrationStatus == Status.Approved);
                    break;

                case Roles.Cdo:

                    if (userClusters is null)
                    {
                        listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus && x.Client.ClusterId == userClusters.ClusterId && x.Client.RegistrationStatus == Status.Approved);
                        break;
                    }

                    listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus && x.Client.RegistrationStatus == Status.Approved);
                    break;

                case Roles.Admin:
                    listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus && x.Client.RegistrationStatus == Status.Approved);
                    break;
            }

            if (request.RoleName.Contains(Roles.Approver) && request.ListingFeeStatus == Status.Approved)
            {
                if (request.AccessBy == 2)
                {
                    listingFees = listingFees.Where(x => (x.Request.CurrentApproverId - 1) == request.AccessBy && x.Client.RegistrationStatus == Status.Approved);
                }
                else
                {
                    listingFees = listingFees.Where(x => x.Request.CurrentApproverId == request.AccessBy && x.Client.RegistrationStatus == Status.Approved);
                }
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
                Total = listingFee.Total,
                RemainingBalance = CalculateRemainingBalance(listingFee, totalListingFee, userListingFeePayments),
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
                    .Select(a => new ClientsWithListingFee.ListingFeeApprovalHistory
                    {
                        Module = a.Request.Module,
                        Approver = a.Approver.Fullname,
                        Level = a.Approver.Approver.FirstOrDefault().Level,
                        Reason = a.Request.Approvals.FirstOrDefault().Reason,
                        CreatedAt = a.CreatedAt,
                        Status = a.Status,
                    }),
                Requestor = listingFee.Request.Requestor.Fullname,
                RequestorMobileNumber = listingFee.Request.Requestor.MobileNumber,
                CurrentApprover = listingFee.Request.CurrentApprover.Fullname,
                CurrentApproverNumber = listingFee.Request.CurrentApprover.MobileNumber,
                NextApprover = listingFee.Request.NextApprover.Fullname,
                NextApproverMobileNumber = listingFee.Request.NextApprover.MobileNumber

            }).OrderBy(x => x.ClientId);

            return await PagedList<ClientsWithListingFee>.CreateAsync(result, request.PageNumber, request.PageSize);
        }

        private static decimal CalculateRemainingBalance(ListingFee listingFee, List<ListingFeeTotal> totalListingFee, List<ListingFeePayment> userListingFeePayments)
        {
            var totalListingFeeForClient = totalListingFee.FirstOrDefault(lf => lf.ClientId == listingFee.ClientId);
            var userListingFeePaymentsForClient = userListingFeePayments.FirstOrDefault(lp => lp.ClientId == listingFee.ClientId);

            var answer = totalListingFeeForClient?.Total - userListingFeePaymentsForClient?.AmountSum;

            return (decimal)((totalListingFeeForClient?.Total - userListingFeePaymentsForClient?.AmountSum) == null ? 
                totalListingFeeForClient?.Total : 
                totalListingFeeForClient?.Total - userListingFeePaymentsForClient?.AmountSum);
        }


    }
}