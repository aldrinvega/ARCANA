using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common.Extension;
using RDF.Arcana.API.Common.Pagination;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using Result = RDF.Arcana.API.Common.Result;

namespace RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;

[Route("api/advance-payment"), ApiController]

public class GetAdvancePayments : ControllerBase
{
    private readonly IMediator _medaitor;

    public GetAdvancePayments(IMediator medaitor)
    {
        _medaitor = medaitor;
    }

    [HttpGet("page")]
    public async Task<IActionResult> Get([FromQuery] GetAdvancePaymentsAsyncQuery query)
    {
        try
        {
            var advancePayments = await _medaitor.Send(query);
            
            Response.AddPaginationHeader(
                advancePayments.CurrentPage,
                advancePayments.PageSize,
                advancePayments.TotalCount,
                advancePayments.TotalPages,
                advancePayments.HasPreviousPage,
                advancePayments.HasNextPage);

            var result = new
            {
                advancePayments,
                advancePayments.CurrentPage,
                advancePayments.PageSize,
                advancePayments.TotalCount,
                advancePayments.TotalPages,
                advancePayments.HasPreviousPage,
                advancePayments.HasNextPage
                
            };

            var successResult = Result.Success(result);

            return Ok(successResult);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    public class GetAdvancePaymentsAsyncQuery : UserParams, IRequest<PagedList<GetAdvancePaymentResult>>
    {
        public string Search { get; set; }
        public bool? Status { get; set; }
    }

    public class GetAdvancePaymentResult
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Fullname { get; set; }
        public string BusinessName { get; set; }
        public string PaymentMethod { get; set; }
        public decimal RemainingBalance { get; set; }
        public decimal AdvancePaymentAmount { get; set; }
        public string Payee { get; set; }
        public DateTime ChequeDate { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime DateReceived { get; set; }
        public decimal ChequeAmount { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string AddedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
    public class Handler : IRequestHandler<GetAdvancePaymentsAsyncQuery, PagedList<GetAdvancePaymentResult>>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public Task<PagedList<GetAdvancePaymentResult>> Handle(GetAdvancePaymentsAsyncQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<AdvancePayment> advancePayments = _context.AdvancePayments
                .Include(cl => cl.Client)
                .Include(u => u.AddedByUser);

            if (!string.IsNullOrEmpty(request.Search))
            {
                advancePayments = advancePayments.Where(ap =>
                    ap.Client.Fullname.Contains(request.Search) ||
                    ap.Client.BusinessName.Contains(request.Search) ||
                    ap.PaymentMethod.Contains(request.Search));
            }

            if (request.Status is not null)
            {
                advancePayments = advancePayments.Where(ap => ap.IsActive == request.Status);
            }

            var result = advancePayments.Select(ap => new GetAdvancePaymentResult
            {
                Id = ap.Id,
                ClientId = ap.ClientId,
                Fullname = ap.Client.Fullname,
                BusinessName = ap.Client.BusinessName,
                PaymentMethod = ap.PaymentMethod,
                RemainingBalance = ap.RemainingBalance,
                AdvancePaymentAmount = ap.AdvancePaymentAmount,
                Payee = ap.Payee,
                ChequeDate = ap.ChequeDate,
                BankName = ap.BankName,
                ChequeNo = ap.ChequeNo,
                DateReceived = ap.DateReceived,
                ChequeAmount = ap.ChequeAmount,
                AccountName = ap.AccountName,
                AccountNo = ap.AccountNo,
                AddedBy = ap.AddedByUser.Fullname,
                CreatedAt = ap.CreatedAt
            });

            return PagedList<GetAdvancePaymentResult>.CreateAsync(result, request.PageNumber, request.PageSize);
        }
    }
}