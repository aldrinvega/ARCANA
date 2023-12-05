using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Regular;

namespace RDF.Arcana.API.Features.Client.Prospecting.All;

[Route("api/Prospecting")]
[ApiController]
public class GetAllProspects : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllProspects(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllProspects")]
    public async Task<IActionResult> GetAllDirectRegistrationClient(
        [FromQuery] GetAllProspectQuery query)
    {
        try
        {
            var regularClient = await _mediator.Send(query);

            Response.AddPaginationHeader(
                regularClient.CurrentPage,
                regularClient.PageSize,
                regularClient.TotalCount,
                regularClient.TotalPages,
                regularClient.HasPreviousPage,
                regularClient.HasNextPage
            );

            var result = new
            {
                regularClient,
                regularClient.CurrentPage,
                regularClient.PageSize,
                regularClient.TotalCount,
                regularClient.TotalPages,
                regularClient.HasPreviousPage,
                regularClient.HasNextPage
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            return Ok(e.Message);
        }
    }

    public class GetAllProspectQuery : UserParams, IRequest<PagedList<GetAllProspectQueryResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string StoreType { get; set; }
        public string RegistrationStatus { get; set; }
    }

    public class GetAllProspectQueryResult
    {
        public string OwnersName { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string TinNumber { get; set; }
        public string BusinessName { get; set; }
        public BusinessAddressCollection BusinessAddress { get; set; }
        public string StoreType { get; set; }
        public string AuthorizedRepresentative { get; set; }
        public string AuthorizedRepresentativePosition { get; set; }
        public int Cluster { get; set; }
        public bool Freezer { get; set; }
        public string TypeOfCustomer { get; set; }
        public bool? DirectDelivery { get; set; }
        public string BookingCoverage { get; set; }
        public string ModeOfPayment { get; set; }
        public ClientTerms Terms { get; set; }
        public FixedDiscounts FixedDiscount { get; set; }
        public bool? VariableDiscount { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }

        public class FixedDiscounts
        {
            public decimal? DiscountPercentage { get; set; }
        }

        public class Attachment
        {
            public int DocumentId { get; set; }
            public string DocumentLink { get; set; }
            public string DocumentType { get; set; }
        }

        public class BusinessAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class OwnersAddressCollection
        {
            public string HouseNumber { get; set; }
            public string StreetName { get; set; }
            public string BarangayName { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
        }

        public class ClientTerms
        {
            public int TermId { get; set; }
            public string Term { get; set; }
            public int? CreditLimit { get; set; }
            public int? TermDays { get; set; }
        }
    }

    public class Handler : IRequestHandler<GetAllProspectQuery, PagedList<GetAllProspectQueryResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllProspectQueryResult>> Handle(GetAllProspectQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> regularClients = _context.Clients
                .Include(oa => oa.OwnersAddress)
                .Include(ba => ba.BusinessAddress)
                .Include(st => st.StoreType)
                .Include(fd => fd.FixedDiscounts)
                .Include(bc => bc.BookingCoverages)
                .Include(cd => cd.ClientDocuments)
                .Include(fr => fr.FreebiesRequests)
                .ThenInclude(fi => fi.FreebieItems)
                .ThenInclude(x => x.Items)
                .Include(terms => terms.Term)
                .ThenInclude(to => to.Terms)
                .Where(x => x.Origin == "Prospecting");

            if (!string.IsNullOrEmpty(request.RegistrationStatus))
            {
                regularClients = regularClients.Where(x => x.RegistrationStatus == request.RegistrationStatus);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                regularClients = regularClients.Where(x =>
                    x.BusinessName.Contains(request.Search) ||
                    x.StoreType.StoreTypeName.Contains(request.Search) ||
                    x.Fullname.Contains(request.Search)
                );
            }

            if (request.RegistrationStatus != null)
            {
                regularClients = regularClients.Where(x => x.RegistrationStatus == request.RegistrationStatus);
            }

            if (!string.IsNullOrEmpty(request.StoreType))
            {
                regularClients = regularClients.Where(x => x.StoreType.StoreTypeName == request.StoreType);
            }

            if (request.Status != null)
            {
                regularClients = regularClients.Where(x => x.IsActive == request.Status);
            }

            var result = regularClients.Select(x => x.ToGetAllProspectResult());

            return await PagedList<GetAllProspectQueryResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}