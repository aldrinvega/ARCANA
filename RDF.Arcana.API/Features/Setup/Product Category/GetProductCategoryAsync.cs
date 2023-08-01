using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

[Route("api/ProductCategory")]
[ApiController]

public class GetProductCategoryAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetProductCategoryAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetProductCategoryAsyncQuery : UserParams, IRequest<PagedList<GetProductCategoryAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetProductCategoryAsyncResult
    {
        public int Id { get; set; }
        public string ProductCategoryName { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public virtual IEnumerable<string> ProductSubCategory { get; set; }
    }
    
    public class Handler : IRequestHandler<GetProductCategoryAsyncQuery, PagedList<GetProductCategoryAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetProductCategoryAsyncResult>> Handle(GetProductCategoryAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductCategory> productCategories = _context.ProductCategories
                .Include(x => x.ProductSubCategory)
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                productCategories = productCategories.Where(x => x.ProductCategoryName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                productCategories = productCategories.Where(x => x.IsActive == request.Status);
            }

            var result = productCategories.Select(x => x.ToGetProductCategoryAsyncResult());

            return await PagedList<GetProductCategoryAsyncResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
    
    [HttpGet("GetProductCategory")]
    public async Task<IActionResult> GetProductCategory([FromQuery]GetProductCategoryAsync.GetProductCategoryAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var result = await _mediator.Send(query);
            Response.AddPaginationHeader(
                result.CurrentPage,
                result.PageSize,
                result.TotalCount,
                result.TotalPages,
                result.HasPreviousPage,
                result.HasNextPage
            );

            var productCategories = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    result,
                    result.CurrentPage,
                    result.PageSize,
                    result.TotalCount,
                    result.TotalPages,
                    result.HasPreviousPage,
                    result.HasNextPage
                }
            };
            productCategories.Messages.Add("Successfully fetch data");
            return Ok(productCategories);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}