using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.Prospecting.Approved;

[Route("api/Prospecting")]
[ApiController]
public class GetAllApprovedProspectAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllApprovedProspectAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllApprovedProspect")]
    public async Task<IActionResult> GetAllRequestedProspect([FromQuery] GetAllApprovedProspectQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var approvedProspect = await _mediator.Send(query);

            Response.AddPaginationHeader(
                approvedProspect.CurrentPage,
                approvedProspect.PageSize,
                approvedProspect.TotalCount,
                approvedProspect.TotalPages,
                approvedProspect.HasPreviousPage,
                approvedProspect.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    requestedProspect = approvedProspect,
                    approvedProspect.CurrentPage,
                    approvedProspect.PageSize,
                    approvedProspect.TotalCount,
                    approvedProspect.TotalPages,
                    approvedProspect.HasPreviousPage,
                    approvedProspect.HasNextPage
                }
            };

            result.Messages.Add("Successfully Fetch Data");
            return Ok(result);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;

            return Ok(response);
        }
    }

    public class GetAllApprovedProspectQuery : UserParams, IRequest<PagedList<GetAllApprovedProspectResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string StoreType { get; set; }

        public bool? WithFreebies { get; set; }
        /*public int AddedBy { get; set; }*/
    }

    public class GetAllApprovedProspectResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddedBy { get; set; }
        public string Origin { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public string StoreType { get; set; }
        public bool IsActive { get; set; }
        public string RegistrationStatus { get; set; }
        public IEnumerable<Freebie> Freebies { get; set; }

        public class Freebie
        {
            public string Status { get; set; }
            public ICollection<FreebieItem> FreebieItems { get; set; }
        }

        public class FreebieItem
        {
            public int? Id { get; set; }
            public string ItemCode { get; set; }
            public int? Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllApprovedProspectQuery, PagedList<GetAllApprovedProspectResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllApprovedProspectResult>> Handle(GetAllApprovedProspectQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> approvedProspect = _context.Clients
                .Include(x => x.Approvals)
                .ThenInclude(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .Include(x => x.StoreType)
                .Where(x => x.RegistrationStatus == "Approved");

            /*if (request.WithFreebies != null)
            {
                approvedProspect = request.WithFreebies.Value
                    ? approvedProspect.Where(x => x.Approvals.Any(a => a.FreebieRequest.Any()))
                    : approvedProspect.Where(x => !x.Approvals.Any(a => a.FreebieRequest.Any()));
            }*/

            if (!string.IsNullOrEmpty(request.StoreType))
            {
                approvedProspect =
                    approvedProspect.Where(x => x.StoreType.StoreTypeName.Contains(request.StoreType));
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                approvedProspect = approvedProspect.Where(x =>
                    x.Fullname == request.Search && x.CustomerType == "Prospect");
            }

            if (request.Status != null)

            {
                approvedProspect = approvedProspect.Where(x =>
                    x.IsActive == request.Status && x.CustomerType == "Prospect");
            }

            var result = approvedProspect.Select(x => x.ToGetGetAllApprovedProspectResult());

            return await PagedList<GetAllApprovedProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}