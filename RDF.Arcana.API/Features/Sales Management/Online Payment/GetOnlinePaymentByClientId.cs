
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment
{
    [Route("api/online-payment"), ApiController]
    public class GetOnlinePaymentByClientId : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetOnlinePaymentByClientId(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}/online-client-by-id")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var query = new GetOnlinePaymentByClientIdQuery
                {
                    ClientId = id
                };

                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetOnlinePaymentByClientIdQuery : IRequest<Result> 
        {
            public int ClientId { get; set; }
        }

        public class GetOnlinePaymentByClientIdResult
        {
            public string OnlinePaymentMethod { get; set; }
            public decimal PaymentAmount { get; set; }
        }

        public class Handler : IRequestHandler<GetOnlinePaymentByClientIdQuery, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetOnlinePaymentByClientIdQuery request, CancellationToken cancellationToken)
            {
                var onlinePayments = await _context.OnlinePayments
                    .Where(op => op.ClientId == request.ClientId &&
                        op.IsActive && 
                        op.Status != Status.Voided)
                    .ToListAsync(cancellationToken);

                var result = onlinePayments.Select(op => new GetOnlinePaymentByClientIdResult
                {
                    OnlinePaymentMethod = op.OnlinePaymentName,
                    PaymentAmount = op.PaymentAmount
                });

                return Result.Success(result);
            }
        }
    }
}
