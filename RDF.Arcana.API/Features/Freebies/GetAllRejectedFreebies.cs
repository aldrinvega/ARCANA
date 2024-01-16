/*using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class GetAllRejectedFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRejectedFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllRejectedFreebies")]
    public async Task<IActionResult> GetAllRejectedFreebiesAsync(
        [FromQuery] GetAllRejectedFreebiesQuery query)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                query.RequestedBy = userId;
            }

            var rejectedFreebies = await _mediator.Send(query);

            Response.AddPaginationHeader(
                rejectedFreebies.CurrentPage,
                rejectedFreebies.PageSize,
                rejectedFreebies.TotalCount,
                rejectedFreebies.TotalPages,
                rejectedFreebies.HasPreviousPage,
                rejectedFreebies.HasNextPage
            );

            var result = new
            {
                rejectedFreebies,
                rejectedFreebies.CurrentPage,
                rejectedFreebies.PageSize,
                rejectedFreebies.TotalCount,
                rejectedFreebies.TotalPages,
                rejectedFreebies.HasPreviousPage,
                rejectedFreebies.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class GetAllRejectedFreebiesQuery : UserParams,
        IRequest<PagedList<GetAllRejectedFreebiesQueryResultCollection>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public int RequestedBy { get; set; }
    }

    public class GetAllRejectedFreebiesQueryResultCollection
    {
        public ICollection<GetAllRejectedFreebiesQueryResult> GetAllRejectedFreebiesQueryResults { get; set; }
    }

    public class GetAllRejectedFreebiesQueryResult
    {
        public int FreebieRequestId { get; set; }
        public int ClientId { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public int? TransactionNumber { get; set; }

        public List<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public int Id { get; set; }
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

    public class Handler : IRequestHandler<GetAllRejectedFreebiesQuery,
        PagedList<GetAllRejectedFreebiesQueryResultCollection>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRejectedFreebiesQueryResultCollection>> Handle(
            GetAllRejectedFreebiesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> rejectedFreebies = _context.Approvals
                .Where(x => x.ApprovalType == "For Freebie Approval")
                .Include(x => x.Client)
                .ThenInclude(x => x.OwnersAddress)
                .Include(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .Where(x => x.IsApproved == false)
                .Where(x => x.FreebieRequest.Any(x => x.Status == "Rejected"))
                .Where(x => x.RequestedBy == request.RequestedBy);

            if (!string.IsNullOrEmpty(request.Search))
            {
                rejectedFreebies = rejectedFreebies.Where(x => x.Client.Fullname.Contains(request.Search));
            }

            if (!request.Status != null)
            {
                rejectedFreebies = rejectedFreebies.Where(x => x.IsActive == request.Status);
            }

            var result = rejectedFreebies.Select(x => x.ToGetAllRejectedFreebiesQueryResultCollection());

            return await PagedList<GetAllRejectedFreebiesQueryResultCollection>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}*/