using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;
[Route("api/advance-payment")]

public class AddCashAdvancePayment : ControllerBase
{
    private readonly IMediator _mediator;

    public AddCashAdvancePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("cash")]
    public async Task<IActionResult> Add([FromBody] AddCashAdvancePaymentCommand command)
    {
        try
        {
            
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
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

    public class AddCashAdvancePaymentCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public decimal AdvancePaymentAmount { get; set; }
        public int AddedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<AddCashAdvancePaymentCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddCashAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(cl => 
                    cl.Id == request.ClientId, 
                    cancellationToken);

            if (existingClient is null)
            {
                return ClientErrors.NotFound();
            }

            var cashAdvancePayment = new CashAdvancePayment
            {
                ClientId = request.ClientId,
                AdvancePaymentAmount = request.AdvancePaymentAmount,
                AddedBy = request.AddedBy
            };

            await _context.CashAdvancePayments.AddAsync(cashAdvancePayment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}