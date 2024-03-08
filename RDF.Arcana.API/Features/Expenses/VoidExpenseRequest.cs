using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Expenses;

[Route("api/Expenses"), ApiController]

public class VoidExpenseRequest : ControllerBase
{

    private readonly IMediator _mediator;

    public VoidExpenseRequest(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("VoidExpenseRequest/{id:int}")]
    public async Task<IActionResult> Void([FromRoute] int id)
    {
        try
        {
            var command = new VoidExpenseRequestCommand
            {
                RequestId = id
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

    public class VoidExpenseRequestCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
    }
    
    public class Handler : IRequestHandler<VoidExpenseRequestCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(VoidExpenseRequestCommand request, CancellationToken cancellationToken)
        {
            var existingExpenseRequest = await _context.Requests
                .Include(x => x.Expenses)
                .FirstOrDefaultAsync(x => x.Id == request.RequestId, cancellationToken);

            if (existingExpenseRequest is not null)
            {
                existingExpenseRequest.Status = Status.Voided;
                existingExpenseRequest.Expenses.Status = Status.Voided;
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}