using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Sales_Management.Sales_transactions;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions;
[Route("api/sales-transaction"), ApiController]

public class VoidTransaction : ControllerBase
{
    private readonly IMediator _mediator;

    public VoidTransaction(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}/void")]
    public async Task<IActionResult> Void([FromBody] VoidtransactionCommand command, [FromRoute] int id)
    {
        try
        {
            command.TransactionId = id;
            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public class VoidtransactionCommand : IRequest<Result>
    {
        public int TransactionId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<VoidtransactionCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(VoidtransactionCommand request, CancellationToken cancellationToken)
        {
            var existingTransaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == request.TransactionId, cancellationToken);

            if (existingTransaction is null)
            {
                return SalesTransactionErrors.NotFound();
            }

            existingTransaction.Status = Status.Voided;
            existingTransaction.Reason = request.Reason;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}