using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting;

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
    
    public class GetAllApprovedProspectQuery : UserParams, IRequest<PagedList<GetAllApprovedProspectResult>>
    {
        public string Search { get; set; }
        public string RegistrationStatus { get; set; }
        public bool? Status { get; set; }
        public int AddedBy { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllApprovedProspectQuery, PagedList<GetAllApprovedProspectResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllApprovedProspectResult>> Handle(GetAllApprovedProspectQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> approvedProspect = _context.Approvals
                .Include(x => x.FreebieRequest)
                .Where(x => x.FreebieRequest == null)
                .Where(x => x.ApprovalType == "Approver Approval" && x.IsActive == true && x.IsApproved == true)
                .Include(x => x.Client)
                .ThenInclude(x => x.StoreType)
                .Where(x => x.ApprovedBy == request.AddedBy);

            if (!string.IsNullOrEmpty(request.RegistrationStatus))
            {
                approvedProspect =
                    approvedProspect.Where(x => x.Client.RegistrationStatus == request.RegistrationStatus);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                approvedProspect = approvedProspect.Where(x => x.Client.Fullname == request.Search && x.Client.CustomerType == "Prospect");
            }
            
            if (request.Status != null)

            {
                approvedProspect = approvedProspect.Where(x => x.IsActive == request.Status && x.Client.CustomerType == "Prospect");
            }

            var result = approvedProspect.Select(x => x.ToGetGetAllApprovedProspectResult());

            return await PagedList<GetAllApprovedProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
    [HttpGet("GetAllApprovedProspect")]
    public async Task<IActionResult> GetAllRequestedProspect([FromQuery]GetAllApprovedProspectQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            if (User.Identity is ClaimsIdentity identity 
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                query.AddedBy = userId;
            } 
            
            var approvedProspect =  await _mediator.Send(query);
            
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
}