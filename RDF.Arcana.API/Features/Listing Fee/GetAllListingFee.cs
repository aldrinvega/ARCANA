using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
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

            var successResult = Result<object>.Success(result, "Data fetch successfully");

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
        public string ListingFeeStatus { get; set; }
    }

    public class ClientsWithListingFee
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string BusinessName { get; set; }
        public int ListingFeeId { get; set; }
        public int ApprovalId { get; set; }
        public string Status { get; set; }
        public string RequestedBy { get; set; }
        public decimal Total { get; set; }
        public string CreateAt { get; set; }
        public string CancellationReason { get; set; }
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

    public class Handler : IRequestHandler<GetAllListingFeeQuery, PagedList<ClientsWithListingFee>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<ClientsWithListingFee>> Handle(GetAllListingFeeQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<ListingFee> listingFees = _context.ListingFees
                .Include(x => x.Approvals)
                .Include(x => x.Client)
                .Include(x => x.RequestedByUser)
                .Include(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item)
                .ThenInclude(x => x.Uom);

            if (!string.IsNullOrEmpty(request.Search))
            {
                listingFees = listingFees.Where(x =>
                    x.Client.BusinessName.Contains(request.Search) || x.Client.Fullname.Contains(request.Search));
            }

            if (!string.IsNullOrEmpty(request.ListingFeeStatus))
            {
                listingFees = listingFees.Where(x => x.Status == request.ListingFeeStatus);
            }

            if (request.Status != null)
            {
                listingFees = listingFees.Where(x => x.IsActive == request.Status);
            }

            var result = listingFees.Select(x => x.ToGetAllListingFeeResult());

            return PagedList<ClientsWithListingFee>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}