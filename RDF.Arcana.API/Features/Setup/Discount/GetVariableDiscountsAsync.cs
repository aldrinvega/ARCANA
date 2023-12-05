using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]
public class GetVariableDiscountsAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetVariableDiscountsAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetVariableDiscount")]
    public async Task<IActionResult> GetAllDiscount([FromQuery] GetDiscountAsyncQuery query)
    {
        try
        {
            var discount = await _mediator.Send(query);

            Response.AddPaginationHeader(
                discount.CurrentPage,
                discount.PageSize,
                discount.TotalCount,
                discount.TotalPages,
                discount.HasPreviousPage,
                discount.HasNextPage
            );

            var result = new
            {
                discount,
                discount.CurrentPage,
                discount.PageSize,
                discount.TotalCount,
                discount.TotalPages,
                discount.HasPreviousPage,
                discount.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetDiscountAsyncQuery : UserParams, IRequest<PagedList<GetDiscountAsyncQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetDiscountAsyncQueryResult
    {
        public int Id { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal MinimumPercentage { get; set; }
        public decimal MaximumPercentage { get; set; }
        public int TotalActives { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetDiscountAsyncQuery, PagedList<GetDiscountAsyncQueryResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetDiscountAsyncQueryResult>> Handle(GetDiscountAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.VariableDiscounts> discounts = _context.VariableDiscounts;

            if (!string.IsNullOrEmpty(request.Search))
            {
                discounts = discounts.Where(x => x.MinimumAmount.Equals(request.Search) ||
                                                 x.MaximumAmount.Equals(request.Search) ||
                                                 x.MinimumPercentage.Equals(request.Search) ||
                                                 x.MaximumPercentage.Equals(request.Search));
            }

            if (request.Status != null)
            {
                discounts = discounts.Where(x => x.IsActive == request.Status);
            }

            var result = discounts.Select(x => x.ToGetDiscountAsyncQueryResult());
            return await PagedList<GetDiscountAsyncQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}