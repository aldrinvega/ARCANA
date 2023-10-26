using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Freebies;

[Route("api/Freebies")]
[ApiController]
public class GetAllApprovedFreebies : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllApprovedFreebies(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllApprovedFreebies")]
    public async Task<IActionResult> GetAllApprovedFreebiesAsync([FromQuery] GetAllApprovedFreebiesQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var approvedFreebies = await _mediator.Send(query);

            Response.AddPaginationHeader(
                approvedFreebies.CurrentPage,
                approvedFreebies.PageSize,
                approvedFreebies.TotalCount,
                approvedFreebies.TotalPages,
                approvedFreebies.HasPreviousPage,
                approvedFreebies.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    approvedFreebies,
                    approvedFreebies.CurrentPage,
                    approvedFreebies.PageSize,
                    approvedFreebies.TotalCount,
                    approvedFreebies.TotalPages,
                    approvedFreebies.HasPreviousPage,
                    approvedFreebies.HasNextPage
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

    public class GetAllApprovedFreebiesQuery : UserParams,
        IRequest<PagedList<GetAllApprovedFreebiesQueryResultCollection>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class GetAllApprovedFreebiesQueryResultCollection
    {
        public ICollection<GetAllApprovedFreebiesQueryResult> GetAllApprovedFreebiesQueryResults { get; set; }
    }

    public class GetAllApprovedFreebiesQueryResult
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


        // public string DateCreated { get; set; }
    }

    public class Handler : IRequestHandler<GetAllApprovedFreebiesQuery,
        PagedList<GetAllApprovedFreebiesQueryResultCollection>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllApprovedFreebiesQueryResultCollection>> Handle(
            GetAllApprovedFreebiesQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Approvals> approvedFreebies = _context.Approvals
                .Where(x => x.ApprovalType == "For Freebie Approval")
                .Include(x => x.Client)
                .ThenInclude(x => x.OwnersAddress)
                .Include(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .Where(x => x.IsApproved == true)
                .Where(x => x.FreebieRequest.Any(x => x.IsDelivered == false))
                .Where(x => x.FreebieRequest.Any(x => x.Status == "Approved"));

            if (!string.IsNullOrEmpty(request.Search))
            {
                approvedFreebies = approvedFreebies.Where(x => x.Client.Fullname.Contains(request.Search));
            }

            if (!request.Status != null)
            {
                approvedFreebies = approvedFreebies.Where(x => x.IsActive == request.Status);
            }

            var result = approvedFreebies.Select(x => x.ToGetApprovedFreebiesQueryResult());

            return await PagedList<GetAllApprovedFreebiesQueryResultCollection>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}