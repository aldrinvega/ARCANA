using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Expenses;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
using static RDF.Arcana.API.Features.Listing_Fee.GetListingFeeApprovalHistory.ListingFeeApprovalHistoryResult;

namespace RDF.Arcana.API.Features.Listing_Fee
{
    [Route("api/listing-fee"), ApiController]
    public class GetListingFeeApprovalHistory : ControllerBase  
    {
        private readonly IMediator _mediator;

        public GetListingFeeApprovalHistory(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/approval-history")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            try
            {
                var query = new GetListingFeeApprovalHistoryQuery
                {
                    Id = id
                };

                var result = await _mediator.Send(query);

                if (result.IsFailure)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public class GetListingFeeApprovalHistoryQuery : IRequest<Result>
        {
            public int Id { get; set; }
        }

        public class ListingFeeApprovalHistoryResult
        {
            public IEnumerable<ListingFeeApprovalHistory> ApprovalHistories { get; set; }
            public IEnumerable<UpdateHistory> UpdateHistories { get; set; }
            public IEnumerable<RequestApproversForListingFee> Approvers { get; set; }
            public class ListingFeeApprovalHistory
            {
                public string Module { get; set; }
                public string Approver { get; set; }
                public DateTime CreatedAt { get; set; }
                public string Status { get; set; }
                public int? Level { get; set; }
                public string Reason { get; set; }
            }
            public class UpdateHistory
            {
                public string Module { get; set; }
                public DateTime UpdatedAt { get; set; }
            }
            public class RequestApproversForListingFee
            {
                public string Name { get; set; }
                public int Level { get; set; }
            }
        }

        public class Handler : IRequestHandler<GetListingFeeApprovalHistoryQuery, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(GetListingFeeApprovalHistoryQuery request, CancellationToken cancellationToken)
            {
                var listingFeeApproval = await _context
                    .ListingFees
                    .Include(r => r.Request)
                    .ThenInclude(up => up.UpdateRequestTrails)
                    .Include(r => r.Request)
                    .ThenInclude(x => x.Approvals)
                    .ThenInclude(x => x.Approver)
                    .Include(r => r.Request)
                    .ThenInclude(x => x.RequestApprovers)
                    .ThenInclude(x => x.Approver)
                    .FirstOrDefaultAsync(x => x.RequestId == request.Id, cancellationToken);

                if (listingFeeApproval is null)
                {
                    return ListingFeeErrors.NotFound();
                }

                var approvalHistories = listingFeeApproval.Request.Approvals == null
                    ? null
                    : listingFeeApproval.Request.Approvals
                        .OrderByDescending(a => a.CreatedAt)
                        .Select(a => new ListingFeeApprovalHistory
                        {
                            Module = a.Request.Module,
                            Approver = a.Approver.Fullname,
                            CreatedAt = a.CreatedAt,
                            Status = a.Status,
                            Level = listingFeeApproval.Request.RequestApprovers.FirstOrDefault(ra => ra.ApproverId == a.ApproverId)?.Level,
                            Reason = a.Reason
                        });

                var result = new ListingFeeApprovalHistoryResult
                {
                    ApprovalHistories = approvalHistories,
                    UpdateHistories = listingFeeApproval.Request.UpdateRequestTrails?.Select(uh => new UpdateHistory
                    {
                        Module = uh.ModuleName,
                        UpdatedAt = uh.UpdatedAt
                    }),
                    Approvers = listingFeeApproval.Request.RequestApprovers.Select(x => new RequestApproversForListingFee
                    {
                        Name = x.Approver.Fullname,
                        Level = x.Level
                    })
                };

                return Result.Success(result);
            }
        }

    }
}
