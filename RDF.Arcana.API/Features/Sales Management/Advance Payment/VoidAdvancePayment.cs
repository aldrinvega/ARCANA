using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;

namespace RDF.Arcana.API.Features.Sales_Management.Advance_Payment;
[Route("api/advance-payment"), ApiController]

public class VoidAdvancePayment : ControllerBase
{
    private readonly IMediator _mediator;

    public VoidAdvancePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Void([FromRoute] int id)
    {
        try
        {
            var command = new VoidAdvancePaymentCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command);

            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class VoidAdvancePaymentCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int Reason { get; set; }
    }
    
    public class Handler : IRequestHandler<VoidAdvancePaymentCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(VoidAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            var existingAdvancePayment = await _context.AdvancePayments
                    .FirstOrDefaultAsync(ap => 
                        ap.Id == request.Id, 
                        cancellationToken);

            if (existingAdvancePayment is null)
            {
                return AdvancePaymentErrors.NotFound();
            }

            existingAdvancePayment.Status = Status.Voided;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}