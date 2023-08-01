using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
 using RDF.Arcana.API.Data;
 using RDF.Arcana.API.Domain;
 
 namespace RDF.Arcana.API.Features.Setup.Meat_Type;
 
 [Route("api/[controller]")]
 [ApiController]

public class GetMeatTypesAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetMeatTypesAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetMeatTypeQuery : UserParams, IRequest<PagedList<GetMeatTypeQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetMeatTypeQueryResult
    {
        public int Id { get; set; }
        public string MeatTypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetMeatTypeQuery, PagedList<GetMeatTypeQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetMeatTypeQueryResult>> Handle(GetMeatTypeQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<MeatType> query = _context.MeatTypes
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(m => m.MeatTypeName.Contains(request.Search));
            }

            if (request.Status.HasValue)
            {
                query = query.Where(m => m.IsActive == request.Status);
            }

            var meatTypes = query.Select(m => new GetMeatTypeQueryResult
            {
                Id = m.Id,
                MeatTypeName = m.MeatTypeName,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt,
                ModifiedBy = m.ModifiedBy,
                AddedBy = m.AddedByUser.Fullname,
                IsActive = m.IsActive
            });

            return await PagedList<GetMeatTypeQueryResult>.CreateAsync(meatTypes, request.PageNumber, request.PageSize);
        }
    }
    
    [HttpGet("GetMeatType")]
    public async Task<IActionResult> GetMeatTypes([FromQuery] GetMeatTypesAsync.GetMeatTypeQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var meatTypes = await _mediator.Send(query);
            Response.AddPaginationHeader(
                meatTypes.CurrentPage,
                meatTypes.PageSize,
                meatTypes.TotalCount,
                meatTypes.TotalPages,
                meatTypes.HasPreviousPage,
                meatTypes.HasNextPage
            );

            response.Status = StatusCodes.Status200OK;
            response.Success = true;
            response.Data = new
            {
                meatTypes,
                meatTypes.CurrentPage,
                meatTypes.PageSize,
                meatTypes.TotalCount,
                meatTypes.TotalPages,
                meatTypes.HasPreviousPage,
                meatTypes.HasNextPage
            };

            response.Messages.Add("Successfully fetch data");

            return Ok(response);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);

            return Conflict(response);
        }
    }
}