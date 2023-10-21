using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Client.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]
public class GetAllRequestedProspectAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRequestedProspectAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllRequestedProspect")]
    public async Task<IActionResult> GetAllRequestedProspect([FromQuery] GetAllRequestedProspectQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var requestedProspect = await _mediator.Send(query);

            Response.AddPaginationHeader(
                requestedProspect.CurrentPage,
                requestedProspect.PageSize,
                requestedProspect.TotalCount,
                requestedProspect.TotalPages,
                requestedProspect.HasPreviousPage,
                requestedProspect.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    requestedProspect,
                    requestedProspect.CurrentPage,
                    requestedProspect.PageSize,
                    requestedProspect.TotalCount,
                    requestedProspect.TotalPages,
                    requestedProspect.HasPreviousPage,
                    requestedProspect.HasNextPage
                }
            };

            result.Messages.Add("Successfully Fetch Data");

            return Ok(result);
        }
        catch (System.Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;

            return Ok(response);
        }
    }

    public class GetAllRequestedProspectQuery : UserParams, IRequest<PagedList<GetAllRequestedProspectResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetAllRequestedProspectResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public int AddedBy { get; set; }
        public string CustomerType { get; set; }
        public string BusinessName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string StoreType { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string Reason { get; set; }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllRequestedProspectQuery, PagedList<GetAllRequestedProspectResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRequestedProspectResult>> Handle(GetAllRequestedProspectQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Approvals> requestedProspect = _context.Approvals.Where(x =>
                    x.Client.RegistrationStatus == "Approved"
                    && x.IsApproved == true
                )
                .Include(x => x.Client)
                .ThenInclude(x => x.OwnersAddress)
                .Include(x => x.Client)
                .ThenInclude(x => x.StoreType);

            if (!string.IsNullOrEmpty(request.Search))
            {
                requestedProspect = requestedProspect.Where(x =>
                    x.Client.Fullname.Contains(request.Search) && x.Client.CustomerType == "Prospect");
            }

            if (request.Status != null)
            {
                requestedProspect = requestedProspect.Where(x =>
                    x.IsActive == request.Status && x.Client.CustomerType == "Prospect");
            }

            var result = requestedProspect.Select(x => x.ToGetAllRequestedProspectResult());

            return await PagedList<GetAllRequestedProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}