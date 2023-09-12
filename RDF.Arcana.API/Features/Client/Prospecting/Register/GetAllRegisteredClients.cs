using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

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

    public class GetAllRegisteredClientsCommand : UserParams, IRequest<PagedList<GetAllRegisteredClientsResult>>
    {
        public string Search { get; set; }
        public bool Status { get; set; }
    }

    public class GetAllRegisteredClientsResult
    {
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public string RepresentativeName { get; set; }
        public string RepresentativePosition { get; set; }
        public string BusinessAddress { get; set; }
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
            public decimal DiscountPercentage { get; set; }
        }
        
    }

    public class Handler : IRequestHandler<GetAllRegisteredClientsCommand, PagedList<GetAllRegisteredClientsResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllRegisteredClientsResult>> Handle(
            GetAllRegisteredClientsCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Domain.Clients> registeredClientsQuery = _context.Clients
                .Include(x => x.FreebiesRequests)
                .Include(x => x.FixedDiscounts)
                .Include(x => x.VariableDiscounts)
                .Include(x => x.Approvals)
                .Include(x => x.ModeOfPayments)
                .Include(x => x.Term)
                .Include(x => x.ClientDocuments)
                .Include(x => x.StoreType)
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
                    Address = client.Address,
                    PhoneNumber = client.PhoneNumber,
                    BusinessName = client.BusinessName,
                    RepresentativeName = client.RepresentativeName,
                    RepresentativePosition = client.RepresentativePosition,
                    BusinessAddress = client.BusinessAddress,
                    Cluster = client.Cluster,
                    Freezer = client.Freezer,
                    CustomerType = client.CustomerType,
                    TermDays = client.TermDays ?? 0,
                    DiscountId = client.DiscountId ?? 0,
                    ClientType = client.ClientType,
                    StoreTypeId = client.StoreTypeId ?? 0,
                    RegistrationStatus = client.RegistrationStatus,
                    Terms = client.Terms ?? 0,
                    ModeOfPayment = client.ModeOfPayment ?? 0,
                    DirectDelivery = client.DirectDelivery,
                    FixedDiscounts = new GetAllRegisteredClientsResult.FixedDiscount
                    {
                        FixedDiscountId = client.FixedDiscounts.ClientId,
                        DiscountPercentage = client.FixedDiscounts.DiscountPercentage
                    },
                    BookingCoverageId = client.BookingCoverageId ?? 0,
                    AddedBy = client.RequestedByUser.Fullname
                });

            return await PagedList<GetAllRegisteredClientsResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }

    [HttpGet("GetRegisteredClients")]
    public async Task<IActionResult> Get([FromQuery] GetAllRegisteredClientsCommand query)
    {
        var response = new QueryOrCommandResult<object>();
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

            var result = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    requestedProspect = registeredClients,
                    registeredClients.CurrentPage,
                    registeredClients.PageSize,
                    registeredClients.TotalCount,
                    registeredClients.TotalPages,
                    registeredClients.HasPreviousPage,
                    registeredClients.HasNextPage
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
