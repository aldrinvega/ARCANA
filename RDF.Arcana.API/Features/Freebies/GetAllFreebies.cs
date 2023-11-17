using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class GetAllFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllFreebies")]
    public async Task<IActionResult> GetAllApprovedFreebiesAsync([FromQuery] GetAllFreebiesQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var freebies = await _mediator.Send(query);

            Response.AddPaginationHeader(
                freebies.CurrentPage,
                freebies.PageSize,
                freebies.TotalCount,
                freebies.TotalPages,
                freebies.HasPreviousPage,
                freebies.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    approvedFreebies = freebies,
                    freebies.CurrentPage,
                    freebies.PageSize,
                    freebies.TotalCount,
                    freebies.TotalPages,
                    freebies.HasPreviousPage,
                    freebies.HasNextPage
                }
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

    public class GetAllFreebiesQuery : UserParams, IRequest<PagedList<GetAllFreebiesResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string FreebieStatus { get; set; }
    }

    public class GetAllFreebiesResult
    {
        public int? ClientId { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public int? TransactionNumber { get; set; }
        public string Status { get; set; }

        public int? FreebieRequestId { get; set; }
        public List<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int? Id { get; set; }
            public string ItemCode { get; set; }
            public int? Quantity { get; set; }
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

    public class Handler : IRequestHandler<GetAllFreebiesQuery, PagedList<GetAllFreebiesResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllFreebiesResult>> Handle(GetAllFreebiesQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> freebies = _context.Clients
                .Include(x => x.OwnersAddress)
                .Include(c => c.Approvals)
                .ThenInclude(a => a.FreebieRequest)
                .ThenInclude(fr => fr.FreebieItems)
                .ThenInclude(fi => fi.Items)
                .Where(c => c.Approvals.Any(a => a.IsApproved == true));


            freebies = request.FreebieStatus switch
            {
                "For freebie request" => freebies.Where(x => x.Approvals.All(a => a.FreebieRequest == null)),
                "Requested" => freebies.Where(x =>
                    x.Approvals.Any(a =>
                        a.FreebieRequest != null && a.FreebieRequest.Any(fr => fr.Status == request.FreebieStatus))),
                "Released" => freebies.Where(x =>
                    x.Approvals.Any(a =>
                        a.FreebieRequest != null && a.FreebieRequest.Any(fr => fr.Status == request.FreebieStatus))),
                _ => freebies
            };

            if (request.Status != null)
            {
                freebies = freebies.Where(x => x.Approvals.Any(x => x.IsActive == request.Status));
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                freebies = freebies.Where(x =>
                    x.Fullname.Contains(request.Search) || x.BusinessName.Contains(request.Search));
            }

            var result = freebies.Select(x => x.ToGetAllFreebiesResult());

            return await PagedList<GetAllFreebiesResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}