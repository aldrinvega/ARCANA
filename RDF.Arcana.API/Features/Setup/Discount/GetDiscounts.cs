using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]

public class GetDiscounts : ControllerBase
{
    private readonly IMediator _mediator;

    public GetDiscounts(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllDiscount")]
    public async Task<IActionResult> Get([FromQuery] GetDiscountQuery query)
    {
        var discount = await _mediator.Send(query);
        
        Response.AddPaginationHeader(
            discount.CurrentPage,
            discount.PageSize,
            discount.TotalPages,
            discount.TotalCount,
            discount.HasPreviousPage,
            discount.HasNextPage
            );

        var result = new
        {
            discount,
            discount.CurrentPage,
            discount.PageSize,
            discount.TotalPages,
            discount.TotalCount,
            discount.HasPreviousPage,
            discount.HasNextPage
        };

        var successResult = Result.Success(result);
        return Ok(successResult);
    }

    public class GetDiscountQuery : UserParams, IRequest<PagedList<GetDiscountQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetDiscountQueryResult
    {
        public int Id { get; set; }
        public string DiscountType { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<GetDiscountQuery, PagedList<GetDiscountQueryResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetDiscountQueryResult>> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
             IQueryable<Domain.Discount> validateDiscount = _context.Discounts
                .Include(user => user.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                validateDiscount = validateDiscount.Where(dc => dc.DiscountType.Contains(request.Search));
            }

            if (request.Status != null)
            {
                validateDiscount = validateDiscount.Where(dc => dc.IsActive == request.Status);
            }

            var result = validateDiscount.Select(dc => new GetDiscountQueryResult
            {
                Id = dc.Id,
                DiscountType = dc.DiscountType,
                CreatedAt = dc.CreatedAt.ToString("MM/dd/yyyy"),
                UpdatedAt = dc.UpdateAt.ToString("MM/dd/yyyy"),
                AddedBy = dc.AddedByUser.Fullname
            });

            return await PagedList<GetDiscountQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}