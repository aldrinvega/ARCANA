using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/RegularClients")]
[ApiController]
public class ApproveClientRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public ApproveClientRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("ApproveClientRegistration/{id:int}")]
    public async Task<IActionResult> ApproveForRegularRegistration([FromRoute] int id)
    {
        try
        {
            if (User.Identity is not ClaimsIdentity identity || !IdentityHelper.TryGetUserId(identity, out var userId))
                return Unauthorized();

            var command = new ApprovedClientRegistrationCommand
            {
                RequestId = id,
                UserId = userId
            };
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class ApprovedClientRegistrationCommand : IRequest<Result>
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
    }

    public class Handler : IRequestHandler<ApprovedClientRegistrationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ApprovedClientRegistrationCommand request, CancellationToken cancellationToken)
        {
            var requestedClient = await _context.Requests
                .Include(client => client.Clients)
                .ThenInclude(listingfee => listingfee.ListingFees)
                .ThenInclude(rq => rq.Request)
                .Where(client => client.Id == request.RequestId)
                .FirstOrDefaultAsync(cancellationToken);
            
            
            /*var existingClientRequest = await _context.Requests
                .Include(client => client.Clients)
                .ThenInclude(listingFee => listingFee.Request)
                .Include(request => request.Clients)
                .ThenInclude(clients => clients.ListingFees)
                .FirstOrDefaultAsync(x =>
                    x.Id == request.RequestId, cancellationToken);*/

            if (requestedClient?.Clients is null)
            {
                return ClientErrors.NotFound();
            }
            
            var registrationApprovers = await _context.RequestApprovers
                .Where(rq => rq.RequestId == request.RequestId)
                .ToListAsync(cancellationToken);

            var underReviewListingFee = requestedClient.Clients?.ListingFees?
                .Where(lf => lf.Status == Status.UnderReview)
                .ToList();
            
            var currentApproverLevel = registrationApprovers
                .FirstOrDefault(approver => approver.ApproverId == requestedClient.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
            }
            
            if (underReviewListingFee is not null)
            {
                foreach (var listingFee in underReviewListingFee)
                {
                    var listingFeeApprover = await _context.RequestApprovers
                        .Where(rq => rq.RequestId == listingFee.RequestId)
                        .ToListAsync(cancellationToken);
                    
                    var newListingFeeApproval = new Approval(
                        listingFee.RequestId,
                        listingFee.Request.CurrentApproverId,
                        Status.Approved,
                        null,
                        true
                    );
                
                    var nextApprovalLevelLevel = currentApproverLevel.Value + 1;
                    var nextListingFeeApprover = listingFeeApprover
                        .FirstOrDefault(approver => approver.Level == nextApprovalLevelLevel);

                    if (nextListingFeeApprover == null)
                    {
                        listingFee.Status = Status.Approved;
                        listingFee.Request.Status = Status.Approved;
                        listingFee.ApprovalDate = DateTime.Now;
                    }
                    else
                    {
                        listingFee.Request.CurrentApproverId = nextListingFeeApprover.ApproverId;
                    }
                
                    await _context.Approval.AddAsync(newListingFeeApproval, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                
                }
            }
            
            
            var newApproval = new Approval(
                requestedClient.Id,
                requestedClient.CurrentApproverId,
                Status.Approved,
                null,
                true
            );
            
            var nextLevel = currentApproverLevel.Value + 1;
            var nextApprover = registrationApprovers
                .FirstOrDefault(approver => approver.Level == nextLevel);
            
            if (nextApprover == null)
            {
                requestedClient.Status = Status.Approved;
                requestedClient.Clients.RegistrationStatus = Status.Approved;
            }
            else
            {
                requestedClient.CurrentApproverId = nextApprover.ApproverId;
            }
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}