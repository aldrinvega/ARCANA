using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register;

[Route("api/Registration")]
[ApiController]
public class GetAllRegisteredClients : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllRegisteredClients(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetRegisteredClients")]
    public async Task<IActionResult> Get([FromQuery] GetAllRegisteredClientsCommand query)
    {
        try
        {
            var registeredClients = await _mediator.Send(query);

            Response.AddPaginationHeader(
                registeredClients.CurrentPage,
                registeredClients.PageSize,
                registeredClients.TotalCount,
                registeredClients.TotalPages,
                registeredClients.HasPreviousPage,
                registeredClients.HasNextPage
            );

            var result = new 
            {
                    requestedProspect = registeredClients,
                    registeredClients.CurrentPage,
                    registeredClients.PageSize,
                    registeredClients.TotalCount,
                    registeredClients.TotalPages,
                    registeredClients.HasPreviousPage,
                    registeredClients.HasNextPage
            };

            var successResult = Result.Success(result);
            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllRegisteredClientsCommand : UserParams, IRequest<PagedList<GetAllRegisteredClientsResult>>
    {
        public string Search { get; set; }
        public bool Status { get; set; }
    }

    public class GetAllRegisteredClientsResult
    {
        public string Fullname { get; set; }
        public OwnersAddressCollection OwnersAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativePosition { get; set; }
        public BusinessAddressCollection BusinessAddress { get; set; }
        public int Cluster { get; set; }
        public bool Freezer { get; set; }
        public string CustomerType { get; set; }
        public int TermDays { get; set; }
        public int DiscountId { get; set; }
        public string ClientType { get; set; }
        public int StoreTypeId { get; set; }
        public string RegistrationStatus { get; set; }
        public int Terms { get; set; }
        public int ModeOfPayment { get; set; }
        public bool? DirectDelivery { get; set; }
        public int BookingCoverageId { get; set; }
        public string AddedBy { get; set; }
        public FixedDiscount FixedDiscounts { get; set; }

        public class FixedDiscount
        {
            public int FixedDiscountId { get; set; }
            public decimal? DiscountPercentage { get; set; }
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
    }

    public class Handler : IRequestHandler<GetAllRegisteredClientsCommand, PagedList<GetAllRegisteredClientsResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRegisteredClientsResult>> Handle(
            GetAllRegisteredClientsCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> registeredClientsQuery = _context.Clients
                .Include(x => x.FreebiesRequests)
                .Include(x => x.FixedDiscounts)
                .Include(x => x.Approvals)
                .Include(x => x.Term)
                .Include(x => x.ClientDocuments)
                .Include(x => x.StoreType)
                .Include(x => x.BusinessAddress)
                .Include(x => x.OwnersAddress)
                .Where(client => client.RegistrationStatus == "Registered" && client.IsActive);

            if (!string.IsNullOrEmpty(request.Search))
            {
                registeredClientsQuery =
                    registeredClientsQuery.Where(client => client.Fullname.Contains(request.Search));
            }

            if (request.Status)
            {
                registeredClientsQuery = registeredClientsQuery.Where(client => client.IsActive);
            }

            var result = registeredClientsQuery
                .Select(client => new GetAllRegisteredClientsResult
                {
                    Fullname = client.Fullname,
                    OwnersAddress = new GetAllRegisteredClientsResult.OwnersAddressCollection
                    {
                        HouseNumber = client.OwnersAddress.HouseNumber,
                        StreetName = client.OwnersAddress.StreetName,
                        City = client.OwnersAddress.City,
                        Province = client.OwnersAddress.Province
                    },
                    PhoneNumber = client.PhoneNumber,
                    BusinessName = client.BusinessName,
                    RepresentativeName = client.RepresentativeName,
                    RepresentativePosition = client.RepresentativePosition,
                    BusinessAddress = new GetAllRegisteredClientsResult.BusinessAddressCollection
                    {
                        HouseNumber = client.BusinessAddress.HouseNumber,
                        StreetName = client.BusinessAddress.StreetName,
                        BarangayName = client.BusinessAddress.Barangay,
                        City = client.BusinessAddress.City,
                        Province = client.BusinessAddress.Province
                    },
                    Cluster = client.Cluster,
                    Freezer = client.Freezer,
                    CustomerType = client.CustomerType,
                    TermDays = client.TermDays ?? 0,
                    DiscountId = client.DiscountId ?? 0,
                    StoreTypeId = client.StoreTypeId ?? 0,
                    RegistrationStatus = client.RegistrationStatus,
                    Terms = client.Terms ?? 0,
                    DirectDelivery = client.DirectDelivery,
                    FixedDiscounts = new GetAllRegisteredClientsResult.FixedDiscount
                    {
                        /*FixedDiscountId = client.FixedDiscounts.ClientId,*/
                        DiscountPercentage = client.FixedDiscounts.DiscountPercentage
                    },
                    BookingCoverageId = client.BookingCoverageId ?? 0,
                    AddedBy = client.AddedByUser.Fullname
                });

            return await PagedList<GetAllRegisteredClientsResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}