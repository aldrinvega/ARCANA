/*using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Client.Prospecting.Rejected;

[Route("api/Prospecting")]
[ApiController]
public class GetAllRejectProspectAsync : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRejectProspectAsync(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllRejectedProspect")]
    public async Task<IActionResult> GetAllRejectedProspect([FromQuery] GetAllRejectProspectQuery query)
    {
        try
        {
            var rejectedProspect = await _mediator.Send(query);

            Response.AddPaginationHeader(
                rejectedProspect.CurrentPage,
                rejectedProspect.PageSize,
                rejectedProspect.TotalCount,
                rejectedProspect.TotalPages,
                rejectedProspect.HasPreviousPage,
                rejectedProspect.HasNextPage
            );

            var result = new
            {
                requestedProspect = rejectedProspect,
                rejectedProspect.CurrentPage,
                rejectedProspect.PageSize,
                rejectedProspect.TotalCount,
                rejectedProspect.TotalPages,
                rejectedProspect.HasPreviousPage,
                rejectedProspect.HasNextPage

            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Ok(e.Message);
        }
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

    public class Handler : IRequestHandler<GetAllRejectProspectQuery, PagedList<GetAllRejectProspectResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRejectProspectResult>> Handle(GetAllRejectProspectQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Approvals> rejectProspect = _context.Approvals.Where(
                    x => x.Client.RegistrationStatus == "Rejected"
                         && x.IsApproved == false)
                .Include(x => x.Client)
                .ThenInclude(x => x.StoreType);

            if (!string.IsNullOrEmpty(request.Search))
            {
                rejectProspect = rejectProspect.Where(x =>
                    x.Client.Fullname == request.Search && x.Client.CustomerType == "Prospect");
            }

            if (request.Status != null)
            {
                rejectProspect =
                    rejectProspect.Where(x => x.IsActive == request.Status && x.Client.CustomerType == "Prospect");
            }

            var result = rejectProspect.Select(x => x.ToGetGetAllRejectProspectResult());

            return await PagedList<GetAllRejectProspectResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}*/