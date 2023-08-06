using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Domain.New_Doamin;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]

public class GetAllRequestedProspectAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRequestedProspectAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetAllRequestedProspectQuery : UserParams, IRequest<PagedList<GetAllRequestedProspectResult>>
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetAllRequestedProspectResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public int AddedBy { get; set; }
        public string CustomerType { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllRequestedProspectQuery, PagedList<GetAllRequestedProspectResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRequestedProspectResult>> Handle(GetAllRequestedProspectQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> requestedProspect = _context.Approvals.Where(x => x.ApprovalType == "Approver Approval" && x.IsApproved == false)
                .Include(x => x.Client);

            if (!string.IsNullOrEmpty(request.Search))
            {
                requestedProspect = requestedProspect.Where(x => x.Client.Fullname.Contains(request.Search) && x.Client.CustomerType == "Prospect");
            }

            if (request.IsActive != null)
            {
                requestedProspect = requestedProspect.Where(x => x.IsActive == request.IsActive && x.Client.CustomerType == "Prospect");
            }

            var result = requestedProspect.Select(x => x.ToGetGetAllProspectResult());

            return await PagedList<GetAllRequestedProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }

    [HttpGet("GetAllRequestedProspect")]
    public async Task<IActionResult> GetAllRequestedProspect([FromQuery]GetAllRequestedProspectQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
           var requestedProspect =  await _mediator.Send(query);
            
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
}