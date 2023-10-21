using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Listing_Fee;

public class GetAllListingFee
{
    public class GetAllListingFeeByCustomerIdQuery : UserParams, IRequest<PagedList<GetAllListingFeeResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public bool? Approved { get; set; }
    }

    public class GetAllListingFeeResult
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string BusinessName { get; set; }
        public string RequestedBy { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<ListingItem> ListingItems { get; set; }

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
    }

    public class Handler : IRequestHandler<GetAllListingFeeByCustomerIdQuery, PagedList<GetAllListingFeeResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public Task<PagedList<GetAllListingFeeResult>> Handle(GetAllListingFeeByCustomerIdQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Approvals> listingFees = _context.Approvals
                .Include(x => x.ListingFee)
                .Include(x => x.Client)
                .Include(x => x.RequestedByUser)
                .Include(x => x.ApprovedBy)
                .Include(x => x.ListingFee)
                .ThenInclude(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item);

            if (!string.IsNullOrEmpty(request.Search))
            {
                listingFees = listingFees.Where(x =>
                    x.ListingFee.Any(x => x.Client.BusinessName.Contains(request.Search)));
            }

            if (request.Status != null)
            {
                listingFees = listingFees.Where(x => x.IsActive == request.Status);
            }

            if (request.Approved != null)
            {
                listingFees = listingFees.Where(x => x.IsApproved == request.Approved);
            }

            var result = listingFees.Select(x => x.ToGetAllListingFeeResult());

            return PagedList<GetAllListingFeeResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}