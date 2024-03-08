using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Client.All
{
    [Route("api/other-expenses")]
    public class GetClientOtherExpensesById : ControllerBase
    {
        private readonly IMediator _medaitor;

        public GetClientOtherExpensesById(IMediator medaitor)
        {
            _medaitor = medaitor;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var query = new GetClientOtherExpensesByIdQuery
                {
                    ClientId = id
                };

                var result = await _medaitor.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetClientOtherExpensesByIdQuery : IRequest<Result>
        {
            public int ClientId { get; set; }
        }

        public class ClientOtherExpensesResult
        {
            public IEnumerable<ExpensesResult> Expenses { get; set; }
            public class ExpensesResult
            {

                public int Id { get; set; }
                public int RequestId { get; set; }
                public string Status { get; set; }
                public IEnumerable<ClientExpensesCollection> Expenses { get; set; }

                public class ClientExpensesCollection
                {
                    public string ExpenseType { get; set; }
                    public decimal Amount { get; set; }
                }
            }
        }

        public class Handler : IRequestHandler<GetClientOtherExpensesByIdQuery, Result>
        {
            private readonly ArcanaDbContext _contecxt;

            public Handler(ArcanaDbContext contecxt)
            {
                _contecxt = contecxt;
            }

            public async Task<Result> Handle(GetClientOtherExpensesByIdQuery request, CancellationToken cancellationToken)
            {
                var documents = await _contecxt.Expenses
                    .Include(ex => ex.ExpensesRequests)
                    .ThenInclude(oe => oe.OtherExpense)
                    .Where(x => x.ClientId == request.ClientId)
                    .ToListAsync();

                var result = new ClientOtherExpensesResult
                {
                    Expenses = documents.Select(x => new ClientOtherExpensesResult.ExpensesResult
                    {
                        Id = x.Id,
                        RequestId = x.RequestId,
                        Status = x.Status,
                        Expenses = x.ExpensesRequests.Select(x => new ClientOtherExpensesResult.ExpensesResult.ClientExpensesCollection
                        {
                            Amount = x.Amount,
                            ExpenseType = x.OtherExpense.ExpenseType
                        })
                    })
                };

                return Result.Success(result);
            }
        }
    }
}
