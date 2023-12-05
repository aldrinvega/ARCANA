using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

[Route("api/TermDays")]
[ApiController]

public class GetTermDaysAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetTermDaysAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetTermDaysAsyncQuery : UserParams, IRequest<PagedList<GetTermDaysAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetTermDaysAsyncResult
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AddedBy { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetTermDaysAsyncQuery, PagedList<GetTermDaysAsyncResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetTermDaysAsyncResult>> Handle(GetTermDaysAsyncQuery request, CancellationToken cancellationToken)
        {
            IQueryable<TermDays> termDays = _context.TermDays
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                if (int.TryParse(request.Search, out var days))
                {
                    termDays = termDays.Where(x => x.Days == days);
                }
                else
                {
                    throw new Exception("Search parameter should be a number representing days.");
                }
            }

            if (request.Status != null)
            {
                termDays = termDays.Where(x => x.IsActive == request.Status);
            }

            var result = termDays.Select(x => x.ToGetTermDaysAsyncResult());

            return await PagedList<GetTermDaysAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);

        }
    }
    
    [HttpGet("GetTermDays")]
    public async Task<IActionResult> GetTermDays([FromQuery]GetTermDaysAsyncQuery query)
    {
        try
        {
            var termDays = await _mediator.Send(query);
            Response.AddPaginationHeader(
                termDays.CurrentPage,
                termDays.PageSize,
                termDays.TotalCount,
                termDays.TotalPages,
                termDays.HasPreviousPage,
                termDays.HasNextPage
            );
            var result = new
            {
                termDays,
                termDays.CurrentPage,
                termDays.PageSize,
                termDays.TotalCount,
                termDays.TotalPages,
                termDays.HasPreviousPage,
                termDays.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }
}