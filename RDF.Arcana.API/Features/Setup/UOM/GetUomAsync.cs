using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/Uom")]
[ApiController]
public class GetUomAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUomAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetUom")]
    public async Task<IActionResult> GetUom([FromQuery] GetUomAsyncQuery query)
    {

        try
        {
            var uom = await _mediator.Send(query);
            Response.AddPaginationHeader(
                uom.CurrentPage,
                uom.PageSize,
                uom.TotalCount,
                uom.TotalPages,
                uom.HasPreviousPage,
                uom.HasNextPage
            );

            var result = new
            {
                uom,
                uom.CurrentPage,
                uom.PageSize,
                uom.TotalCount,
                uom.TotalPages,
                uom.HasPreviousPage,
                uom.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class GetUomAsyncQuery : UserParams, IRequest<PagedList<GetUomQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetUomQueryResult
    {
        public int Id { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string AddedBy { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }

    public class Handler : IRequestHandler<GetUomAsyncQuery, PagedList<GetUomQueryResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUomQueryResult>> Handle(GetUomAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Uom> uoms = _context.Uoms
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                uoms = uoms.Where(x => x.UomCode.Contains(request.Search));
            }

            if (request.Status != null)
            {
                uoms = uoms.Where(x => x.IsActive == request.Status);
            }

            var result = uoms.Select(x => x.ToGetUomQueryResult());

            return await PagedList<GetUomQueryResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}