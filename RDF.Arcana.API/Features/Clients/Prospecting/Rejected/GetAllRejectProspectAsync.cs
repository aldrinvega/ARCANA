using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Domain.New_Doamin;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Rejected;

[Route("api/Prospecting")]
[ApiController]

public class GetAllRejectProspectAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRejectProspectAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetAllRejectProspectQuery : UserParams, IRequest<PagedList<GetAllRejectProspectResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetAllRejectProspectResult
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
        public string Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllRejectProspectQuery, PagedList<GetAllRejectProspectResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRejectProspectResult>> Handle(GetAllRejectProspectQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> rejectProspect = _context.Approvals.Where(
                    x => x.Client.RegistrationStatus == "Rejected"
                    && x.IsApproved == false)
                .Include(x => x.Client);

            if (!string.IsNullOrEmpty(request.Search))
            {
                rejectProspect = rejectProspect.Where(x => x.Client.Fullname == request.Search && x.Client.CustomerType == "Prospect");
            }

            if (request.Status != null)
            {
                rejectProspect = rejectProspect.Where(x => x.IsActive == request.Status && x.Client.CustomerType == "Prospect");
            }

            var result = rejectProspect.Select(x => x.ToGetGetAllRejectProspectResult());

            return await PagedList<GetAllRejectProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }

    [HttpGet("GetAllRejectedProspect")]
    public async Task<IActionResult> GetAllRejectedProspect([FromQuery]GetAllRejectProspectQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
           var rejectedProspect =  await _mediator.Send(query);
            
            Response.AddPaginationHeader(
                rejectedProspect.CurrentPage,
                rejectedProspect.PageSize,
                rejectedProspect.TotalCount,
                rejectedProspect.TotalPages,
                rejectedProspect.HasPreviousPage,
                rejectedProspect.HasNextPage
                );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    requestedProspect = rejectedProspect,
                    rejectedProspect.CurrentPage,
                    rejectedProspect.PageSize,
                    rejectedProspect.TotalCount,
                    rejectedProspect.TotalPages,
                    rejectedProspect.HasPreviousPage,
                    rejectedProspect.HasNextPage
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