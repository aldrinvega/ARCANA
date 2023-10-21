using RDF.Arcana.API.Data;

namespace RDF.Arcana.API.Features.Listing_Fee;

public class RejectListingFee
{
    public class RejectListingFeeCommand : IRequest<Unit>
    {
        public int ListingFeeId { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectListingFeeCommand, Unit>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RejectListingFeeCommand request, CancellationToken cancellationToken)
        {
            var existingListingFee = await _context.Approvals
                .Include(x => x.ListingFee)
                .ThenInclude(x => x.ListingFeeItems)
                .ThenInclude(x => x.Item)
                .FirstOrDefaultAsync(
                    x => x.IsActive &&
                         x.IsApproved == false &&
                         x.ApprovalType == "For Freebie Approval", cancellationToken);

            if (existingListingFee is null)
            {
                throw new System.Exception("No Listing Fee found");
            }

            // Locate the specific FreebieRequest to be Rejected
            var freebieToReject = existingListingFee.ListingFee
                .FirstOrDefault(lf => lf.Id == request.ListingFeeId);

            if (freebieToReject == null)
            {
                throw new System.Exception("Freebie Request not found");
            }

            // Reject the specific FreebieRequest
            freebieToReject.Status = "Rejected";
            existingListingFee.Reason = request.Reason;
            existingListingFee.IsApproved = false;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}