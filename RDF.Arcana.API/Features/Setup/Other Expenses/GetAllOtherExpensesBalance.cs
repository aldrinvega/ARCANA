
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses
{
    [Route("api/OtherExpenses"), ApiController]
    public class GetAllOtherExpensesBalance : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetAllOtherExpensesBalance(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllOtherExpensesBalance")]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllOtherExpensesBalanceQuery();
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

        public class GetAllOtherExpensesBalanceQuery : IRequest<Result> { }

        public class OtherExpensesResult
        {
            public string BusinessName { get; set; }
            public string FullName { get; set; }
            public decimal RemainingBalance { get; set; } 
        }

        public class Handler : IRequestHandler<GetAllOtherExpensesBalanceQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetAllOtherExpensesBalanceQuery request, CancellationToken cancellationToken)
            {
                //var otherExpenses = await _context.Expenses
                //    .Include(c => c.Client)
                //    .Include(er => er.ExpensesRequests)
                //    .Where(oe => oe.Status == Status.Approved &&
                //           oe.IsActive)
                //    .GroupBy(g => new { g.Client.BusinessName, g.Client.Fullname })
                //    .Select(oe => new OtherExpensesResult
                //    {
                //        BusinessName = oe.Key.BusinessName,
                //        FullName = oe.Key.Fullname,
                //        RemainingBalance = oe.Sum(e => e.ExpensesRequests.Sum(er => er.Amount))
                //    })
                //    .OrderBy(bn => bn.BusinessName)
                //    .ToListAsync(cancellationToken);

                var otherExpenses = await _context.ExpensesRequests
                    .Include(e => e.Expenses)
                    .ThenInclude(c => c.Client)
                    .Where(er => er.Expenses.Status == Status.Approved &&
                                 er.Expenses.IsActive)
                    .GroupBy(er => new { er.Expenses.Client.BusinessName, er.Expenses.Client.Fullname })
                    .Select(er => new OtherExpensesResult
                    {
                        BusinessName = er.Key.BusinessName,
                        FullName = er.Key.Fullname,
                        RemainingBalance = er.Sum(a => a.Amount)
                    })
                    .OrderBy(bn => bn.BusinessName)
                    .ToListAsync(cancellationToken);

                return Result.Success(otherExpenses);
            }
        }
    }
}
