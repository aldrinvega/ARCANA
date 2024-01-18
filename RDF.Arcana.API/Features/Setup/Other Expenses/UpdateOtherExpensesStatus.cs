using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;

[Route("api/OtherExpenses"), ApiController]

public class UpdateOtherExpensesStatus : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateOtherExpensesStatus(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("UpdateOtherExpensesStatus/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id)
    {
        try
        {

            var command = new UpdateOtherExpensesStatusCommand
            {
                OtherExpensesId = id
            };

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
    

    public class UpdateOtherExpensesStatusCommand : IRequest<Result>
    {
        public int OtherExpensesId { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateOtherExpensesStatusCommand, Result>
    {

        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateOtherExpensesStatusCommand request, CancellationToken cancellationToken)
        {
            var validateOtherExpenses =
                await _context.OtherExpenses.FirstOrDefaultAsync(oe => oe.Id == request.OtherExpensesId,
                    cancellationToken);

            if (validateOtherExpenses is null)
            {
                return OtherExpensesErrors.NotFound();
            }

            validateOtherExpenses.IsActive = !validateOtherExpenses.IsActive;

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}