using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Location;

[Route("api/Location")]
[ApiController]

public class GetAllLocationAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllLocationAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetAllLocationAsyncQuery : UserParams, IRequest<PagedList<GetAllLocationAsyncResult>>
    {
        public bool? Status { get; set; }
        public string Search { get; set; }
    }

    public class GetAllLocationAsyncResult
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public string AddedBy { get; set; }
        public IEnumerable<string> Users { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllLocationAsyncQuery, PagedList<GetAllLocationAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllLocationAsyncResult>> Handle(GetAllLocationAsyncQuery request, CancellationToken cancellationToken)
        {
           IQueryable<Domain.Location> locations = _context.Locations
               .Include(x => x.AddedByUser);
            
            if (!string.IsNullOrEmpty(request.Search))
            {
                locations = locations.Where(x => x.LocationName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                locations = locations.Where(x => x.IsActive == request.Status);
            }

            var result = locations.Select(l => l.ToGetAllLocationResult());

            return await PagedList<GetAllLocationAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
            
        }
    }
    
    [HttpGet("GetAllLocations")]
    public async Task<IActionResult> GetAll([FromQuery]GetAllLocationAsyncQuery query)
    {
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

            var results = new QueryOrCommandResult<object>()
            {
                Success = true,
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
            results.Messages.Add("Successfully Data Fetch");
            return Ok(results);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}