using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Discount;

[Route("api/Discount")]
[ApiController]

public class GetDiscountsAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetDiscountsAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetDiscountAsyncQuery : UserParams, IRequest<PagedList<GetDiscountAsyncQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetDiscountAsyncQueryResult
    {
        public int Id { get; set; }
        public decimal LowerBound { get; set; }
        public decimal UpperBound { get; set; }
        public decimal CommissionRateLower { get; set; }
        public decimal CommissionRateUpper { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetDiscountAsyncQuery, PagedList<GetDiscountAsyncQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetDiscountAsyncQueryResult>> Handle(GetDiscountAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Discount> discounts = _context.Discounts
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                discounts = discounts.Where(x => x.LowerBound.Equals(request.Search));
            }

            if (request.Status != null )
            {
                discounts = discounts.Where(x => x.IsActive == request.Status);
            }

            var result = discounts.Select(x => x.ToGetDiscountAsyncQueryResult());
            return await PagedList<GetDiscountAsyncQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
    
    [HttpGet("GetDiscount")]
    public async Task<IActionResult> GetAllDiscount([FromQuery]GetDiscountsAsync.GetDiscountAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var discount =  await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                discount.CurrentPage,
                discount.PageSize,
                discount.TotalCount,
                discount.TotalPages,
                discount.HasPreviousPage,
                discount.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    discount,
                    discount.CurrentPage,
                    discount.PageSize,
                    discount.TotalCount,
                    discount.TotalPages,
                    discount.HasPreviousPage,
                    discount.HasNextPage
                }
            };
            result.Messages.Add("Successfully fetch data");
            return Ok(result);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}