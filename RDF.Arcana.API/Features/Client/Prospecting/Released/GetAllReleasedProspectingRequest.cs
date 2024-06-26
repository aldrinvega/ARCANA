/*using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

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

    [HttpGet("GetAllReleasedProspectingRequest")]
    public async Task<IActionResult> GetAllReleasedProspectingRequestAsync(
        [FromQuery] GetAllReleasedProspectingRequestQuery query)
    {
        try
        {
            var releasedProspecting = await _mediator.Send(query);

            Response.AddPaginationHeader(
                releasedProspecting.CurrentPage,
                releasedProspecting.PageSize,
                releasedProspecting.TotalCount,
                releasedProspecting.TotalPages,
                releasedProspecting.HasPreviousPage,
                releasedProspecting.HasNextPage
            );

            var result = new
            {
                releasedProspecting,
                releasedProspecting.CurrentPage,
                releasedProspecting.PageSize,
                releasedProspecting.TotalCount,
                releasedProspecting.TotalPages,
                releasedProspecting.HasPreviousPage,
                releasedProspecting.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
    }

    public class GetAllReleasedProspectingRequestQuery : UserParams,
        IRequest<PagedList<GetAllReleasedProspectingRequestResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetAllReleasedProspectingRequestResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string PhoneNumber { get; set; }
        public string AddedBy { get; set; }
        public string CustomerType { get; set; }
        public string BusinessName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public string RegistrationStatus { get; set; }
        public int? TransactionNumber { get; set; }
        public string PhotoProofPath { get; set; }
        public string ESignaturePath { get; set; }
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

    public class Handler : IRequestHandler<GetAllReleasedProspectingRequestQuery,
        PagedList<GetAllReleasedProspectingRequestResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllReleasedProspectingRequestResult>> Handle(
            GetAllReleasedProspectingRequestQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Approvals> validateClient = _context.Approvals
                .Where(a =>
                    a.IsApproved
                    && a.Client.RegistrationStatus == "Released"
                    && a.FreebieRequest.Any(x => x.IsDelivered))
                .Include(a => a.Client)
                .ThenInclude(x => x.AddedByUser)
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
                validateClient = validateClient.Where(x => x.IsActive == request.Status);
            }

            var result = validateClient.Select(x => x.GetAllReleasedProspectingRequestResult());

            return await PagedList<GetAllReleasedProspectingRequestResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}*/