//using RDF.Arcana.API.Common;
//using RDF.Arcana.API.Data;
//using RDF.Arcana.API.Features.Listing_Fee.Errors;

//namespace RDF.Arcana.API.Features.Listing_Fee;

//public class GetListingFeeBalanceByClientId
//{
//    public class GetListingFeeBalanceByClientIdQuery : IRequest<Result>
//    {
//        public int ClientId { get; set; }
//    }

//    public class GetListingFeeBalanceByClientQueryResult
//    {
//        public int ClientId { get; set; }
//        public string FullName { get; set; }
//        public decimal RemainingBalance { get; set; }
//    }

//    public class Handler : IRequestHandler<GetListingFeeBalanceByClientIdQuery, Result>
//    {
//        private readonly ArcanaDbContext _context;
//        public Handler(ArcanaDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<Result> Handle(GetListingFeeBalanceByClientIdQuery request, CancellationToken cancellationToken)
//        {
//            var listingFees = await _context.ListingFees
//                .Include(c => c.Client)
//                .Where(lf => lf.ClientId == request.ClientId)
//                .ToListAsync();

//            if (!listingFees.Any()) 
//            {
//                return ListingFeeErrors.NotFound();
//            }

//            var result = listingFees.Select(x => new GetListingFeeBalanceByClientQueryResult
//            {
//                ClientId = request.ClientId,
//                FullName = Client.
//            });

//        }
//    }
//}
