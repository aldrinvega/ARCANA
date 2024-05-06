using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Advance_Payment;
[Route("api/advance-payment"), ApiController]

public class GetAdvancePaymentsByClientId : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAdvancePaymentsByClientId(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}/remaining-balance")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        try
        {
            var query = new GetAdvancePaymentByClientIdQuery
            {
                ClientId = id
            };

            var result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class GetAdvancePaymentByClientIdQuery : IRequest<Result>
    {
        public int ClientId { get; set; }
    }

    public class GetAdvancePaymentsByClientIdResult
    {
        public decimal RemainingBalance { get; set; }
    }

    public class Handler : IRequestHandler<GetAdvancePaymentByClientIdQuery, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(GetAdvancePaymentByClientIdQuery request, CancellationToken cancellationToken)
        {
            var advancePayments = await _context.AdvancePayments
                .Where(ap => 
                    ap.ClientId == request.ClientId &&
                    ap.IsActive &&
                    ap.Status != Status.Voided)
                .ToListAsync(cancellationToken);

            var result = new GetAdvancePaymentsByClientIdResult
            {
                RemainingBalance = advancePayments.Sum(ap => ap.RemainingBalance)
            };

            return Result.Success(result);
        }
    }
}