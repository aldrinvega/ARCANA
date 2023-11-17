//using Microsoft.AspNetCore.Mvc;
//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;

//namespace RDF.Arcana.API.Features.Setup.Mode_Of_Payment
//{
//    [Route("api/ModeOfPayment")]
//    [ApiController]
//    public class UpdatePaymentStatus : ControllerBase
//    {
//        public class UpdatePaymentStatusCommand : IRequest<Unit>
//        {
//            public int PaymentId { get; set; }
//        }

//        public class Handler : IRequestHandler<UpdatePaymentStatusCommand, Unit>
//        {
//            private readonly ArcanaDbContext _context;

//            public Handler(ArcanaDbContext context)
//            {
//                _context = context;
//            }

//            public async Task<Unit> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
//            {
//               var existingPaymentMethod = await _context.Payments.FirstOrDefaultAsync(x => x.Id == request.PaymentId) 
//                    ?? throw new InvalidOperationException("No payment method found");

//                existingPaymentMethod.IsActive = !existingPaymentMethod.IsActive;
//                existingPaymentMethod.UpdatedAt = DateTime.UtcNow;

//                await _context.SaveChangesAsync(cancellationToken);
//                return Unit.Value;
//            }
//        }
//    }

//    [HttpPatch("UpdateModeOfPaymentStatus/{id}")]
//    public async Task<IActionResult> Update([FromBody]UpdatePaymentStatus command, [FromRoute]int id)
//    {
//        var response = new QueryOrCommandResult<object>();
//        try
//        {

//        }
//        catch
//        {

//        }
//    }
//}
