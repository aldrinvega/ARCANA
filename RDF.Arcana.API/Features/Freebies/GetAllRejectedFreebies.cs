using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.New_Doamin;

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

    public class GetAllRejectedFreebiesQuery : UserParams, IRequest<PagedList<GetAllRejectedFreebiesQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }
    
    public class GetAllRejectedFreebiesQueryResult
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
        // public string DateCreated { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAllRejectedFreebiesQuery, PagedList<GetAllRejectedFreebiesQueryResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRejectedFreebiesQueryResult>> Handle(GetAllRejectedFreebiesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> rejectedFreebies = _context.Approvals
                .Where(x => x.ApprovalType == "For Freebie Approval")
                .Include(x => x.Client)
                .Include(x => x.FreebieRequest)
                .ThenInclude(x => x.FreebieItems)
                .ThenInclude(x => x.Items)
                .Where(x => x.IsApproved == false)
                .Where(x => x.FreebieRequest.Status == "Rejected");

            if (!string.IsNullOrEmpty(request.Search))
            {
                rejectedFreebies = rejectedFreebies.Where(x => x.Client.Fullname.Contains(request.Search));
            }

            if (!request.Status != null)
            {
                rejectedFreebies = rejectedFreebies.Where(x => x.IsActive == request.Status);
            }

            var result = rejectedFreebies.Select(x => x.ToGetAllRejectedFreebiesQueryResult());

            return await PagedList<GetAllRejectedFreebiesQueryResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }

    [HttpGet("GetAllRejectedFreebies")]
    public async Task<IActionResult> GetAllRejectedFreebiesAsync(
        [FromQuery] GetAllRejectedFreebiesQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var rejectedFreebies = await _mediator.Send(query);

            Response.AddPaginationHeader(
                rejectedFreebies.CurrentPage,
                rejectedFreebies.PageSize,
                rejectedFreebies.TotalCount,
                rejectedFreebies.TotalPages,
                rejectedFreebies.HasPreviousPage,
                rejectedFreebies.HasNextPage
            );

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    rejectedFreebies,
                    rejectedFreebies.CurrentPage,
                    rejectedFreebies.PageSize,
                    rejectedFreebies.TotalCount,
                    rejectedFreebies.TotalPages,
                    rejectedFreebies.HasPreviousPage,
                    rejectedFreebies.HasNextPage
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
}