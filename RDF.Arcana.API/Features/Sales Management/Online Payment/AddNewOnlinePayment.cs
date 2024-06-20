//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;
//using RDF.Arcana.API.Features.Setup.UserRoles;

//namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment
//{
//    public class AddNewOnlinePayment
//    {
//        public class AddNewOnlinePaymentCommand : IRequest<Result>
//        {
//            public string OnlinePlatform { get; set; }
//        }

//        public class Handler : IRequestHandler<AddNewOnlinePaymentCommand, Result>
//        {
//            private readonly ArcanaDbContext _context;
//            public Handler(ArcanaDbContext context)
//            {
//                _context = context;
//            }

//            public async Task<Result> Handle(AddNewOnlinePaymentCommand request, CancellationToken cancellationToken)
//            {
//                var existingOnlinePlatforms = await _context.OnlinePayments
//                    .FirstOrDefaultAsync(ol => ol.OnlinePlatform == request.OnlinePlatform, cancellationToken);

//                if (existingOnlinePlatforms is not null) 
//                {
//                    return OnlinePaymentErrors.ExistingOnlinePlatform();
//                }
                
//                var onlinePayment = new 
//                return Result.Success();
//            }
//        }
//    }
//}
