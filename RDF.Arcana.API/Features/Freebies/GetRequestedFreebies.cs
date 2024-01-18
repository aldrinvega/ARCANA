/*using Microsoft.AspNetCore.Mvc;
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

    [HttpGet("GetAllFreebieRequests")]
    public async Task<IActionResult> GetAllFreebieRequests([FromQuery] GetRequestedFreebiesQuery queryResult)
    {
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

            var result = new
                {
                    freebieRequest,
                    freebieRequest.PageSize,
                    freebieRequest.TotalCount,
                    freebieRequest.TotalPages,
                    freebieRequest.HasPreviousPage,
                    freebieRequest.HasNextPage
                };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetRequestedFreebiesQuery : UserParams, IRequest<PagedList<GetRequestedFreebiesQueryResultCollection>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public int RequestedBy { get; set; }
    }

    public class GetRequestedFreebiesQueryResultCollection
    {
        public ICollection<GetRequestedFreebiesQueryResult> GetRequestedFreebiesQueryResults { get; set; }
    }

    public class GetRequestedFreebiesQueryResult
    {
        public int FreebieRequestId { get; set; }
        public int ClientId { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public int? TransactionNumber { get; set; }
        public string Status { get; set; }

        public List<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int Id { get; set; }
            public int RequestId { get; set; }
            public string ItemCode { get; set; }
            public int Quantity { get; set; }
        }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetRequestedFreebiesQuery,
        PagedList<GetRequestedFreebiesQueryResultCollection>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetRequestedFreebiesQueryResultCollection>> Handle(
            GetRequestedFreebiesQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Approvals> freebies = _context.Approvals
                .Where(x => x.ApprovalType == "For Freebie Approval")
                .Include(x => x.Client)
                .ThenInclude(x => x.OwnersAddress)
                .Include(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .Where(x => x.FreebieRequest.Any(x => x.Status == "Requested"))
                .Where(x => x.RequestedBy == request.RequestedBy);

            if (!string.IsNullOrEmpty(request.Search))
            {
                freebies = freebies.Where(x => x.Client.Fullname.Contains(request.Search));
            }

            if (request.Status != null)
            {
                freebies = freebies.Where(x => x.IsActive == request.Status);
            }

            var result = freebies.Select(x => x.ToGetRequestedFreebiesQueryResult());

            return await PagedList<GetRequestedFreebiesQueryResultCollection>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}*/