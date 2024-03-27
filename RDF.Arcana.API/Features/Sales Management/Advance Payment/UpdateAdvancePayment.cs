using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Transactions.Advance_Payment;

[Route("api/advance-payment"), ApiController]

public class UpdateAdvancePayment : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateAdvancePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] UpdateAdvancePaymentCommand command, [FromRoute]int id)
    {
        try
        {
            command.Id = id;
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.ModifiedBy = userId;
            }

            var result = await _mediator.Send(command);
            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class UpdateAdvancePaymentCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AdvancePaymentAmount { get; set; }
        public string Payee { get; set; }
        public DateTime ChequeDate { get; set; }
        public string BankName { get; set; }
        public string ChequeNo { get; set; }
        public DateTime DateReceived { get; set; }
        public decimal ChequeAmount { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public int ModifiedBy { get; set; }
    }
    
    public class Handler : IRequestHandler<UpdateAdvancePaymentCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateAdvancePaymentCommand request, CancellationToken cancellationToken)
        {
            var existingAdvancePayment =
                await _context.AdvancePayments
                    .FirstOrDefaultAsync(ap => 
                        ap.Id == request.Id, 
                        cancellationToken);

            if (existingAdvancePayment is null)
            {
                return AdvancePaymentErrors.NotFound();
            }

            existingAdvancePayment.PaymentMethod = request.PaymentMethod;
            existingAdvancePayment.AdvancePaymentAmount = request.AdvancePaymentAmount;
            existingAdvancePayment.Payee = request.Payee;
            existingAdvancePayment.ChequeDate = request.ChequeDate;
            existingAdvancePayment.DateReceived = request.DateReceived;
            existingAdvancePayment.ChequeAmount = request.ChequeAmount;
            existingAdvancePayment.BankName = request.BankName;
            existingAdvancePayment.ChequeNo = request.ChequeNo;
            existingAdvancePayment.AccountName = request.AccountName;
            existingAdvancePayment.AccountNo = request.AccountNo;
            existingAdvancePayment.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}