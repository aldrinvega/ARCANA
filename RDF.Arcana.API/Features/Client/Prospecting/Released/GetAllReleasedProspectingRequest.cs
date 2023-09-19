using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Released;

namespace RDF.Arcana.API.Features.Client.Prospecting.Released;

[Route("api/Prospect")]
[ApiController]

public class GetAllReleasedProspectingRequest : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllReleasedProspectingRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    public class GetAllReleasedProspectingRequestQuery : UserParams, IRequest<PagedList<GetAllReleasedProspectingRequestResult>>
    {
        public string Search { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetAllReleasedProspectingRequestResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddedBy { get; set; }
        public string CustomerType { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string RegistrationStatus { get; set; }
        public string TransactionNumber { get; set; }
        public string PhotoProofPath { get; set; }
        public string ESignaturePath { get; set; }
        public List<Freebie> Freebies { get; set; }
        public class Freebie
        {
            public int Id { get; set; }
            public string ItemCode { get; set; }
            public int Quantity { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllReleasedProspectingRequestQuery, PagedList<GetAllReleasedProspectingRequestResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllReleasedProspectingRequestResult>> Handle(GetAllReleasedProspectingRequestQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> validateClient = _context.Approvals
                .Where(a => 
                            a.IsApproved
                            && a.Client.RegistrationStatus == "Released"
                            && a.FreebieRequest.IsDelivered)
                .Include(a => a.Client)
                .ThenInclude(x => x.RequestedByUser)
                .Include(x => x.Client)
                .ThenInclude(x => x.StoreType)
                .Include(a => a.FreebieRequest)
                .ThenInclude(fr => fr.FreebieItems)
                .ThenInclude(fi => fi.Items);

            if (!string.IsNullOrEmpty(request.Search))
            {
                validateClient = validateClient.Where(x => x.Client.Fullname.Contains(request.Search));
            }

            if (request.Search != null)
            {
                validateClient = validateClient.Where(x => x.IsActive == request.IsActive);
            }

            var result = validateClient.Select(x => x.GetAllReleasedProspectingRequestResult());

            return await PagedList<GetAllReleasedProspectingRequestResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }

    [HttpGet("GetAllReleasedProspectingRequest")]
    public async Task<IActionResult> GetAllReleasedProspectingRequestAsync([FromQuery] GetAllReleasedProspectingRequestQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
           var releasedProspecting =  await _mediator.Send(query);
           
           Response.AddPaginationHeader(
               releasedProspecting.CurrentPage,
               releasedProspecting.PageSize,
               releasedProspecting.TotalCount,
               releasedProspecting.TotalPages,
               releasedProspecting.HasPreviousPage,
               releasedProspecting.HasNextPage
               );

           var result = new QueryOrCommandResult<object>
           {
               Success = true,
               Status = StatusCodes.Status200OK,
               Data = new
               {
                   releasedProspecting,
                   releasedProspecting.CurrentPage,
                   releasedProspecting.PageSize,
                   releasedProspecting.TotalCount,
                   releasedProspecting.TotalPages,
                   releasedProspecting.HasPreviousPage,
                   releasedProspecting.HasNextPage
               }
           };
           
           result.Messages.Add("Successfully fetch data");
           return Ok(result);
        }
        catch (System.Exception e)
        {
            response.Status = StatusCodes.Status200OK;
            response.Messages.Add(e.Message);
            return Conflict(response);
        }
    }
}