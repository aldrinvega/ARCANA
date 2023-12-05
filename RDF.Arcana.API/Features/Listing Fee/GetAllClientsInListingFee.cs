using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Regular;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee")]
[ApiController]
public class GetAllClientsInListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllClientsInListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllClientsInListingFee")]
    public async Task<IActionResult> GetAllClientsInListingFees(
        [FromQuery] GetAllClientsInListingFeeQuery query)
    {
        try
        {
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
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllClientsInListingFeeQuery : UserParams, IRequest<PagedList<GetAllClientsInListingFeeResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public bool? IncludeRejected { get; set; }
        public string StoreType { get; set; }
        public string Origin { get; set; }
    }

    public class GetAllClientsInListingFeeResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string BusinessName { get; set; }
        public IEnumerable<ListingFee> ListingFees { get; set; }

        public class ListingFee
        {
            public int Id { get; set; }
            public int RequestId { get; set; }
            public string Status { get; set; }
            public IEnumerable<ListingItem> ListingItems { get; set; }
        }
        public class ListingItem
        {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public int Sku { get; set; }
            public decimal UnitCost { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllClientsInListingFeeQuery, PagedList<GetAllClientsInListingFeeResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllClientsInListingFeeResult>> Handle(GetAllClientsInListingFeeQuery request,
            CancellationToken cancellationToken)
        {
            var clientsListingFee = _context.Clients
                .Include(mop => mop.ClientModeOfPayment)
                .Include(abu => abu.AddedByUser)
                .Include(rq => rq.Request)
                .ThenInclude(user => user.Requestor)
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
                .AsNoTracking();

            if (!string.IsNullOrEmpty(request.Search))
            {
                clientsListingFee = clientsListingFee.Where(x =>
                    x.BusinessName.Contains(request.Search) ||
                    x.StoreType.StoreTypeName.Contains(request.Search) ||
                    x.Fullname.Contains(request.Search)
                );
            }

            if (request.Origin != null)
            {
                clientsListingFee = clientsListingFee.Where(x => x.Origin == request.Origin);
            }

            if (!string.IsNullOrEmpty(request.StoreType))
            {
                clientsListingFee = clientsListingFee.Where(x => x.StoreType.StoreTypeName == request.StoreType);
            }

            if (request.Status != null)
            {
                clientsListingFee = clientsListingFee.Where(x => x.IsActive == request.Status);
            }

            if (request.IncludeRejected == false)
            {
                clientsListingFee = clientsListingFee.Where(x =>
                    x.RegistrationStatus == Status.ForFreebieApproval ||
                    x.RegistrationStatus == Status.UnderReview);
            }

            var result = clientsListingFee.Select(x => x.ToGetAllClientsInListingFeeResult());

            return await PagedList<GetAllClientsInListingFeeResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}