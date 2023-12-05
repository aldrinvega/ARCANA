using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Store_Type;

[Route("api/StoreType")]
[ApiController]

public class GetStoreTypesAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetStoreTypesAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetStoreTypesQuery : UserParams, IRequest<PagedList<GetStoreTypesQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetStoreTypesQueryResult
    {
        public int Id { get; set; }
        public string StoreTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetStoreTypesQuery, PagedList<GetStoreTypesQueryResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetStoreTypesQueryResult>> Handle(GetStoreTypesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<StoreType> storeTypes = _context.StoreTypes
                .Include(x => x.AddedByUser)
                .Include(x => x.ModifiedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                storeTypes = storeTypes.Where(x => x.StoreTypeName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                storeTypes = storeTypes.Where(x => x.IsActive == request.Status);
            }

            var result = storeTypes.Select(x => new GetStoreTypesQueryResult
            {
                Id = x.Id,
                StoreTypeName = x.StoreTypeName,
                CreatedAt = x.CreateAt,
                AddedBy = x.AddedByUser.Fullname,
                IsActive = x.IsActive
            });

            return await PagedList<GetStoreTypesQueryResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }

    [HttpGet("GetAllStoreTypes")]
    public async Task<IActionResult> GetAllStoreTypes([FromQuery]GetStoreTypesQuery query)
    {
        try
        {
            var storeTypes = await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                storeTypes.CurrentPage,
                storeTypes.PageSize,
                storeTypes.TotalCount,
                storeTypes.TotalPages,
                storeTypes.HasPreviousPage,
                storeTypes.HasNextPage
                );

            var result = new
            {
                storeTypes,
                storeTypes.CurrentPage,
                storeTypes.PageSize,
                storeTypes.TotalCount,
                storeTypes.TotalPages,
                storeTypes.HasPreviousPage,
                storeTypes.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}