using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]

public class GetRequestedFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public GetRequestedFreebies(IMediator mediator)   
    {
        _mediator = mediator;
    }

    public class GetRequestedFreebiesQuery : UserParams, IRequest<PagedList<GetRequestedFreebiesQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetRequestedFreebiesQueryResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public string OwnersAddress { get; set; }
        public string TransactionNumber { get; set; }
        
        public List<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int Id { get; set; }
            public string ItemCode { get; set; }
            public int Quantity { get; set; }
        }
        public string DateCreated { get; set; }
    }

    public class Handler : IRequestHandler<GetRequestedFreebiesQuery, PagedList<GetRequestedFreebiesQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetRequestedFreebiesQueryResult>> Handle(GetRequestedFreebiesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<FreebieRequest> freebies = _context.FreebieRequests
                .Where(x => x.StatusId == 1)
                .Include(x => x.Freebies)
                .ThenInclude(x => x.Item)
                .Include(x => x.ApprovedClient)
                .ThenInclude(x => x.Client);

            if (!string.IsNullOrEmpty(request.Search))
            {
                freebies = freebies.Where(x => x.ApprovedClient.Client.OwnersName.Contains(request.Search));
            }

            if (request.Status != null)
            {
                freebies = freebies.Where(x => x.IsActive == request.Status);
            }

            var result = freebies.Select(x => x.ToGetRequestedFreebiesQueryResult());

            return await PagedList<GetRequestedFreebiesQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
            
        }
    }

    [HttpGet("GetAllFreebieRequests")]
    public async Task<IActionResult> GetAllFreebieRequests([FromQuery]GetRequestedFreebiesQuery queryResult)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var freebieRequest = await _mediator.Send(queryResult);
            
            Response.AddPaginationHeader(
                freebieRequest.CurrentPage,
                freebieRequest.PageSize,
                freebieRequest.TotalCount,
                freebieRequest.TotalPages,
                freebieRequest.HasPreviousPage,
                freebieRequest.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    freebieRequest,
                    freebieRequest.PageSize,
                    freebieRequest.TotalCount,
                    freebieRequest.TotalPages,
                    freebieRequest.HasPreviousPage,
                    freebieRequest.HasNextPage
                },
                Success = true
            };
            
            result.Messages.Add("Successfully fetch data");

            return Ok(result);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;

            return Conflict(response);
        }
    }
}