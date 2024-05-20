
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Advance_Payment
{
    [Route("api/AdvancePayment"), ApiController]
    public class GetAllAdvancePaymentBalance : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllAdvancePaymentBalance(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllAdvancePaymentTotals")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllAdvancePaymentTotalsQuery();
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetAllAdvancePaymentTotalsQuery : IRequest<Result> { }

        public class GetAllAdvancePaymentTotalsResult
        {
            public string BusinessName { get; set; }
            public string FullName { get; set; }
            public decimal RemainingBalance { get; set; }
        }

        public class Handler : IRequestHandler<GetAllAdvancePaymentTotalsQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllAdvancePaymentTotalsQuery request, CancellationToken cancellationToken)
            {
                var advancePayments = await _context.AdvancePayments
                    .Include(c => c.Client)
                    .Where(ap => ap.Status != Status.Voided &&
                                 ap.IsActive)
                    .GroupBy(ap => new { ap.Client.Fullname, ap.Client.BusinessName })
                    .Select(ap => new GetAllAdvancePaymentTotalsResult
                    {
                        BusinessName = ap.Key.BusinessName,
                        FullName = ap.Key.Fullname,
                        RemainingBalance = ap.Sum(ap => ap.RemainingBalance)
                    })
                    .OrderBy(ap => ap.BusinessName)
                    .ToListAsync(cancellationToken);

                return Result.Success(advancePayments);
            }
        }
    }
}
