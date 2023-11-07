using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Regular;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee")]
[ApiController]
public class GetAllClientsInListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllClientsInListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllClientsInListingFee")]
    public async Task<IActionResult> GetAllClientsInListingFees(
        [FromQuery] GetAllClientsInListingFeeQuery query)
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

            var successResult = Result<object>.Success(result, "Data fetch successfully");

            return Ok(successResult);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAllClientsInListingFeeQuery : UserParams, IRequest<PagedList<GetAllClientsInListingFeeResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
        public string RegistrationStatus { get; set; }
        public string StoreType { get; set; }
        public string Origin { get; set; }
    }

    public class GetAllClientsInListingFeeResult
    {
        public int Id { get; set; }
        public string OwnersName { get; set; }
        public string BusinessName { get; set; }
    }

    public class Handler : IRequestHandler<GetAllClientsInListingFeeQuery, PagedList<GetAllClientsInListingFeeResult>>
    {
        private const string APPROVED = "Approved";
        private const string UNDER_REVIEW = "Under review";
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllClientsInListingFeeResult>> Handle(GetAllClientsInListingFeeQuery request,
            CancellationToken cancellationToken)
        {
            var clientsListingFee = _context.Clients
                .Where(rs => rs.RegistrationStatus == APPROVED ||
                             rs.RegistrationStatus == UNDER_REVIEW);

            if (!string.IsNullOrEmpty(request.Search))
            {
                clientsListingFee = clientsListingFee.Where(x =>
                    x.BusinessName.Contains(request.Search) ||
                    x.StoreType.StoreTypeName.Contains(request.Search) ||
                    x.Fullname.Contains(request.Search)
                );
            }

            if (request.Origin != null)
            {
                clientsListingFee = clientsListingFee.Where(x => x.Origin == request.Origin);
            }

            if (!string.IsNullOrEmpty(request.StoreType))
            {
                clientsListingFee = clientsListingFee.Where(x => x.StoreType.StoreTypeName == request.StoreType);
            }

            if (request.Status != null)
            {
                clientsListingFee = clientsListingFee.Where(x => x.IsActive == request.Status);
            }

            var result = clientsListingFee.Select(x => x.ToGetAllClientsInListingFeeResult());

            return await PagedList<GetAllClientsInListingFeeResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}