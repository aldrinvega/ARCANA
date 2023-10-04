using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Mode_Of_Payment;

public class GetAllModeOfPaymentsAsync
{
    public class GetAllModeOfPaymentsAsyncQuery : UserParams, IRequest<PagedList<GetAllModeOfPaymentsAsyncResult>>
    {
        public string Search { get; set; }
        public bool Status { get; set; }
    }

    public class GetAllModeOfPaymentsAsyncResult
    {
        public int Id { get; set; }
        public string ModeOfPayments { get; set; }
        public string AddedBy { get; set; }
    }

    public class Handler : IRequestHandler<GetAllModeOfPaymentsAsyncQuery, PagedList<GetAllModeOfPaymentsAsyncResult>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<PagedList<GetAllModeOfPaymentsAsyncResult>> Handle(GetAllModeOfPaymentsAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<ModeOfPayment> modeOfPayments = _context.ModeOfPayments
                .Include(x => x.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                modeOfPayments = modeOfPayments.Where(x => x.Payment == request.Search);
            }

            if (request.Search != null)
            {
                modeOfPayments = modeOfPayments.Where(x => x.IsActive == request.Status);
            }

            var result = modeOfPayments.Select(x => x.ToGetModeOfPaymentAsyncResult());

            return await PagedList<GetAllModeOfPaymentsAsyncResult>.CreateAsync(result, request.PageNumber,
                request.PageSize);
        }
    }
}

[Route("api/ModeOfPayments")]
[ApiController]
public class ModeOfPayments : ControllerBase
{
    private readonly IMediator _mediator;

    public ModeOfPayments(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetAllModeOfPayments")]
    public async Task<IActionResult> DirectClientRegistration(
        [FromQuery] GetAllModeOfPaymentsAsync.GetAllModeOfPaymentsAsyncQuery query)
    {
        var response = new QueryOrCommandResult<object>();
        try
        {
            var result = await _mediator.Send(query);
            Response.AddPaginationHeader(
                result.CurrentPage,
                result.PageSize,
                result.TotalCount,
                result.TotalPages,
                result.HasPreviousPage,
                result.HasNextPage
            );

            var modeOfPayments = new QueryOrCommandResult<object>
            {
                Success = true,
                Status = StatusCodes.Status200OK,
                Data = new
                {
                    result,
                    result.CurrentPage,
                    result.PageSize,
                    result.TotalCount,
                    result.TotalPages,
                    result.HasPreviousPage,
                    result.HasNextPage
                }
            };
            modeOfPayments.Messages.Add("Successfully fetch data");
            return Ok(modeOfPayments);
        }
        catch (Exception e)
        {
            response.Messages.Add(e.Message);
            response.Status = StatusCodes.Status409Conflict;
            return Conflict(response);
        }
    }
}