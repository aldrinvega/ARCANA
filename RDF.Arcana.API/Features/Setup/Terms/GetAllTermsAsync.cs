using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Terms;

[Route("api/Terms")]
[ApiController]
public class GetAllTermsAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllTermsAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllTerms")]
    public async Task<IActionResult> GetAllTerms([FromQuery] GetAllTermsAsyncQuery query)
    {
        try
        {
            var terms = await _mediator.Send(query);

            Response.AddPaginationHeader(
                terms.CurrentPage,
                terms.PageSize,
                terms.TotalCount,
                terms.TotalPages,
                terms.HasPreviousPage,
                terms.HasNextPage
            );

            var results = new
            {
                terms,
                terms.CurrentPage,
                terms.PageSize,
                terms.TotalCount,
                terms.TotalPages,
                terms.HasPreviousPage,
                terms.HasNextPage
            };

            var successResult = Result.Success(results);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllTermsAsyncQuery : UserParams, IRequest<PagedList<GetAllTermsAsyncResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetAllTermsAsyncResult
    {
        public int Id { get; set; }
        public string TermType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<GetAllTermsAsyncQuery, PagedList<GetAllTermsAsyncResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllTermsAsyncResult>> Handle(GetAllTermsAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Terms> terms = _context.Terms
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                terms = terms.Where(x => x.TermType.Contains(request.Search));
            }

            if (request.Status != null)
            {
                terms = terms.Where(x => x.IsActive == request.Status);
            }

            var result = terms.Select(x => new GetAllTermsAsyncResult
            {
                Id = x.Id,
                TermType = x.TermType,
                CreatedAt = x.CreatedAt,
                IsActive = x.IsActive,
                AddedBy = x.AddedByUser.Fullname
            });

            return await PagedList<GetAllTermsAsyncResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}