using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;

[Route("api/OtherExpenses")]

public class UpdateOtherExpenses : ControllerBase
{

    private readonly IMediator _mediator;

    public UpdateOtherExpenses(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateOtherExpenses/{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateOtherExpensesCommand command, [FromRoute] int id)
    {
        try
        {
            command.OtherExpensesId = id;
            var result = await _mediator.Send(command);

            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateOtherExpensesCommand : IRequest<Result>
    {
        public int OtherExpensesId { get; set; }
        public string ExpenseType { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateOtherExpensesCommand, Result>
    {

        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateOtherExpensesCommand request, CancellationToken cancellationToken)
        {
            var existingOtherExpenses = await _context.OtherExpenses.FirstOrDefaultAsync(oe => oe.Id == request.OtherExpensesId,
                    cancellationToken);

            var alreadyExisit = await _context.OtherExpenses
                .AnyAsync(oe => 
                oe.ExpenseType == request.ExpenseType && 
                oe.Id != request.OtherExpensesId, 
                cancellationToken);

            if (existingOtherExpenses is null)
            {
                return OtherExpensesErrors.NotFound();
            }

            if (alreadyExisit)
            {
                return OtherExpensesErrors.AlreadyExist(request.ExpenseType);
            }

            existingOtherExpenses.ExpenseType = request.ExpenseType;
            existingOtherExpenses.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}