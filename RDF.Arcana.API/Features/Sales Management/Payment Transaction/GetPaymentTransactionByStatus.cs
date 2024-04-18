using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Sales_Management.Payment_Transaction;
[Route("api/payment-transaction-status"), ApiController]

    public class GetPaymentTransactionByStatus : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetPaymentTransactionByStatus(IMediator mediator)
        {
            _mediator = mediator;
        }

    
    public class GetPaymentTransactionByStatusQuery : IRequest<Result>
        {
            public string Status { get; set; }
        }

        //public class Handler : IRequestHandler<GetPaymentTransactionByStatusQuery, Result>
        //{
        //    private readonly ArcanaDbContext _context;
        //    public Handler(ArcanaDbContext context)
        //    {
        //        _context = context;
        //    }

        //    public async Task<Result> Handle(GetPaymentTransactionByStatusQuery request, CancellationToken cancellationToken)
        //    {
        //        var payments = await _context.Transactions  
        //            .Where(s =>
        //                s.Status == request.Status &&
        //                s.IsActive)
        //            .ToListAsync(cancellationToken);

                
        //        return Result.Success(result);
        //    }
        //}
    }

