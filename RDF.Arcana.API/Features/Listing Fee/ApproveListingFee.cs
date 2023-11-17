using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Listing_Fee.Errors;

namespace RDF.Arcana.API.Features.Listing_Fee;

[Route("api/ListingFee"), ApiController]
public class ApproveListingFee : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveListingFee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ApproveListingFee/{id:int}")]
    public async Task<IActionResult> ApproveListingFeeRequest([FromRoute] int id)
    {
        try
        {
            var command = new ApproveListingFeeCommand
            {
                ApprovalId = id
            };
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.ApprovedBy = userId;
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (System.Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class ApproveListingFeeCommand : IRequest<Result<ApprovedListingFeeResult>>
    {
        public int ApprovalId { get; set; }
        public int ApprovedBy { get; set; }
    }

    public class ApprovedListingFeeResult
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public IEnumerable<ListingFeeItem> ListingFeeItems { get; set; }

        public class ListingFeeItem
        {
            public string ItemCode { get; set; }
            public string ItemDescription { get; set; }
            public string Uom { get; set; }
            public decimal Total { get; set; }
        }
    }

    public class Handler : IRequestHandler<ApproveListingFeeCommand, Result<ApprovedListingFeeResult>>
    {
        private const string APPROVED = "Approved";
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ApprovedListingFeeResult>> Handle(ApproveListingFeeCommand request,
            CancellationToken cancellationToken)
        {
            var existingApprovalsForListingFee = await _context.Approvals
                .Include(listingFee => listingFee.ListingFee)
                .ThenInclude(listingFeeItems => listingFeeItems.ListingFeeItems)
                .ThenInclude(X => X.Item)
                .ThenInclude(X => X.Uom)
                .FirstOrDefaultAsync(approval => approval.Id == request.ApprovalId, cancellationToken);

            if (existingApprovalsForListingFee == null)
            {
                return Result<ApprovedListingFeeResult>.Failure(ListingFeeErrors.NotFound());
            }

            existingApprovalsForListingFee.IsApproved = true;
            existingApprovalsForListingFee.ApprovedBy = request.ApprovedBy;

            foreach (var listingFee in existingApprovalsForListingFee.ListingFee)
            {
                listingFee.Status = APPROVED;
            }

            await _context.SaveChangesAsync(cancellationToken);
            var result = new ApprovedListingFeeResult
            {
                Id = existingApprovalsForListingFee.Id,
                Status = APPROVED,
                ListingFeeItems = existingApprovalsForListingFee.ListingFee.SelectMany(lf =>
                    lf.ListingFeeItems.Select(lfi => new ApprovedListingFeeResult.ListingFeeItem
                    {
                        ItemCode = lfi.Item.ItemCode,
                        ItemDescription = lfi.Item.ItemDescription,
                        Uom = lfi.Item.Uom.UomCode,
                        Total = lf.Total
                    })).ToList()
            };


            return Result<ApprovedListingFeeResult>.Success(result, "Listing Fee approved successfully");
        }
    }
}