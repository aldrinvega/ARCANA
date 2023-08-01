using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.UOM;

[Route("api/[controller]")]
[ApiController]

public class GetUomAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetUomAsync(IMediator mediator)
    {
        _mediator = mediator;
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
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetUomQueryResult>> Handle(GetUomAsyncQuery request, CancellationToken cancellationToken)
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
    
    [HttpGet("GetUom")]
    public async Task<IActionResult> GetUom([FromQuery] GetUomAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();

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

            var result = new QueryOrCommandResult<object>
            {
                Status = StatusCodes.Status200OK,
                Success = true,
                Data = new
                {
                    uom,
                    uom.CurrentPage,
                    uom.PageSize,
                    uom.TotalCount,
                    uom.TotalPages,
                    uom.HasPreviousPage,
                    uom.HasNextPage
                }
            };
            result.Messages.Add("Successfully fetch data");
            return Ok(result);
        }
        catch (Exception e)
        {
            response.Status = StatusCodes.Status409Conflict;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}