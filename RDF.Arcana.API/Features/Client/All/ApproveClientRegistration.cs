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
    public async Task<IActionResult> ApproveForRegularRegistration([FromRoute] int id, [FromQuery] int ListingFeeRequestId)
    {
        try
        {
            if (User.Identity is not ClaimsIdentity identity || !IdentityHelper.TryGetUserId(identity, out var userId))
                return Unauthorized();

            var command = new ApprovedClientRegistrationCommand
            {
                RegistrationRequestId = id,
                ListingFeeRequestId = ListingFeeRequestId,
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

    public sealed record ApprovedClientRegistrationCommand : IRequest<Result>
    {
        public int? ListingFeeRequestId { get; set; }
        public int RegistrationRequestId { get; set; }
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
                .ThenInclude(lf => lf.ListingFees)
                .Where(client => client.Id == request.RegistrationRequestId)
                .FirstOrDefaultAsync(cancellationToken);

            if (requestedClient?.Clients is null)
            {
                return ClientErrors.NotFound();
            }

            #region Registration Approvers

            var registrationApprovers = await _context.RequestApprovers
                .Where(rq => rq.RequestId == request.RegistrationRequestId)
                .ToListAsync(cancellationToken);
            
            var currentApproverLevel = registrationApprovers
                .FirstOrDefault(approver => approver.ApproverId == requestedClient.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
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
                
                var notificationForCurrentApprover = new Domain.Notification
                {
                    UserId = requestedClient.CurrentApproverId,
                    Status = Status.ApprovedClients
                };
                
                await _context.Notifications.AddAsync(notificationForCurrentApprover, cancellationToken);
                
                var notification = new Domain.Notification
                {
                    UserId = requestedClient.RequestorId,
                    Status = Status.ApprovedClients
                };
                
                await _context.Notifications.AddAsync(notification, cancellationToken);
                
            }
            else
            {
                var notificationForCurrentApprover = new Domain.Notification
                {
                    UserId = requestedClient.CurrentApproverId,
                    Status = Status.ApprovedClients
                };
                
                await _context.Notifications.AddAsync(notificationForCurrentApprover, cancellationToken);
                
                requestedClient.CurrentApproverId = nextApprover.ApproverId;
                
                var notificationForApprover = new Domain.Notification
                {
                    UserId = nextApprover.ApproverId,
                    Status = Status.PendingClients
                };
                
                await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);
            }
            
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            #endregion

            #region Listing Fee Approvals
            if(requestedClient.Clients.ListingFees != null)
            {
                var listingFees = await _context.Requests
                .Include(listing => listing.ListingFee)
                .Where(lf => lf.Id == request.ListingFeeRequestId)
                .FirstOrDefaultAsync(cancellationToken);

                var approvers = await _context.RequestApprovers
                    .Where(module => module.RequestId == request.ListingFeeRequestId)
                    .ToListAsync(cancellationToken);
                var currentListingFeeApproverLevel = approvers
                    .FirstOrDefault(approver =>
                        approver.ApproverId == listingFees.CurrentApproverId)?.Level;

                if (currentApproverLevel == null)
                {
                    return ApprovalErrors.NoApproversFound(Modules.ListingFeeApproval);
                }

                var newListingFeeApproval = new Approval(
                    listingFees.Id,
                    listingFees.CurrentApproverId,
                    Status.Approved,
                    null,
                    true
                );

                var nextLisitngFeeLevel = currentApproverLevel.Value + 1;
                var nextListingFeeApprover = approvers
                    .FirstOrDefault(approver => approver.Level == nextLevel);

                if (nextApprover == null)
                {
                    listingFees.Status = Status.Approved;
                    listingFees.ListingFee.Status = Status.Approved;
                    listingFees.ListingFee.ApprovalDate = DateTime.Now;

                    var notificationForApprover = new Domain.Notification
                    {
                        UserId = listingFees.CurrentApproverId,
                        Status = Status.ApprovedListingFee
                    };

                    await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

                    var notification = new Domain.Notification
                    {
                        UserId = listingFees.RequestorId,
                        Status = Status.ApprovedListingFee
                    };

                    await _context.Notifications.AddAsync(notification, cancellationToken);
                }
                else
                {
                    listingFees.CurrentApproverId = nextApprover.ApproverId;

                    var notificationForNextApprover = new Domain.Notification
                    {
                        UserId = nextApprover.ApproverId,
                        Status = Status.PendingListingFee
                    };

                    await _context.Notifications.AddAsync(notificationForNextApprover, cancellationToken);
                }

                await _context.Approval.AddAsync(newApproval, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            #endregion

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}