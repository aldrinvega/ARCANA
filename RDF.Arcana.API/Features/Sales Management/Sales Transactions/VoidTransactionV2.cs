
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Sales_Management.Sales_transactions;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Sales_Management.Sales_Transactions
{
    [Route("api/sales-transaction"), ApiController]
    public class VoidTransactionV2 : ControllerBase
    {
        private readonly IMediator _mediator;
        public VoidTransactionV2(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("voidV2")]
        public async Task<IActionResult> Void([FromBody] VoidTransactionV2Command command)
        {
            try
            {               
                var result = await _mediator.Send(command);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class VoidTransactionV2Command : IRequest<Result> 
        {
            public List<int> TransactionId { get; set; }
            public string Reason { get; set; }
        }

        public class Handler : IRequestHandler<VoidTransactionV2Command, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(VoidTransactionV2Command request, CancellationToken cancellationToken)
            {
                
                foreach(var transactionId in request.TransactionId) 
                {
                    var transaction = await _context.Transactions
                        .FirstOrDefaultAsync(t => t.Id == transactionId);

                    if(transaction is not null && transaction.Status is Status.Pending)
                    {
                        transaction.Status = Status.Voided;
                        transaction.Reason = request.Reason;
                    }
                    else if(transaction is not null && transaction.Status is Status.Paid)
                    {
                        return TransactionErrors.AlreadyHasPayment();
                    }
                    else if (transaction is not null && transaction.Status is Status.Voided)
                    {
                        return TransactionErrors.Voided();
                    }
                    else
                    {
                        return TransactionErrors.NotFound();
                    }

                }

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }
        }
    }
}
