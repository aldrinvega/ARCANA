using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using Result = RDF.Arcana.API.Common.Result;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;

[Route("api/OtherExpenses"), ApiController]

public class AddNewOtherExpenses : ControllerBase
{

    private readonly IMediator _mediator;

    public AddNewOtherExpenses(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("AddNewExpense")]
    public async Task<IActionResult> Add([FromBody] AdNewOtherExpensesCommand command)
    {
        try
        {
           if (User.Identity is ClaimsIdentity identity
               && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public record AdNewOtherExpensesCommand : IRequest<Result>
    {
        public string ExpenseType { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AdNewOtherExpensesCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AdNewOtherExpensesCommand request, CancellationToken cancellationToken)
        {

            var validateExpense = await _context.OtherExpenses.FirstOrDefaultAsync(oe => oe.ExpenseType == request.ExpenseType,
                    cancellationToken);

            if (validateExpense is not null)
            {
                return OtherExpensesErrors.AlreadyExist(request.ExpenseType);
            }

            var otherExpenses = new OtherExpenses
            {
                ExpenseType = request.ExpenseType,
                AddedBy = request.AddedBy
            };

            await _context.OtherExpenses.AddAsync(otherExpenses, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();

        }
    }
}