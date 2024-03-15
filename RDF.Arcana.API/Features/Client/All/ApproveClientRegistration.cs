using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Expenses;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
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
    public async Task<IActionResult> ApproveForRegularRegistration([FromRoute] int id, [FromQuery] int ListingFeeRequestId, [FromQuery] int OtherExpensesRequestId)
    {
        try
        {
            if (User.Identity is not ClaimsIdentity identity || !IdentityHelper.TryGetUserId(identity, out var userId))
            {
                return Unauthorized();
            }

            var command = new ApprovedClientRegistrationCommand
            {
                RegistrationRequestId = id,
                ListingFeeRequestId = ListingFeeRequestId,
                OtherExpensesRequestId = OtherExpensesRequestId,
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
        public int? OtherExpensesRequestId { get; set; }
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

            //Validate client if exisit
            var requestedClient = await _context.Requests
                .Include(client => client.Clients)
                .ThenInclude(lf => lf.ListingFees)
                .Include(client => client.Clients)
                .ThenInclude(expenses => expenses.Expenses)
                .Where(client => client.Id == request.RegistrationRequestId)
                .FirstOrDefaultAsync(cancellationToken);

            if (requestedClient?.Clients is null)
            {
                return ClientErrors.NotFound();
            }

            if(requestedClient.CurrentApproverId != request.UserId)
            {
                return ApprovalErrors.NotAllowed(Modules.RegistrationApproval);
            }

            #region Registration Approvers

            //Get all the approvers for the request 
            var registrationApprovers = await _context.RequestApprovers
                .Where(rq => rq.RequestId == request.RegistrationRequestId)
                .ToListAsync(cancellationToken);
            
            //Get the current approval level of current approver
            var currentApproverLevel = registrationApprovers
                .FirstOrDefault(approver => approver.ApproverId == requestedClient.CurrentApproverId)?.Level;
            
            if (currentApproverLevel == null)
            {
                return ApprovalErrors.NoApproversFound(Modules.RegistrationApproval);
            }
            
            //Add new approval
            var newApproval = new Approval(
                requestedClient.Id,
                requestedClient.CurrentApproverId,
                Status.Approved,
                null,
                true
            );
            
            //Validate if there is a next level for this approval
            var nextLevel = currentApproverLevel.Value + 1;
            var nextApprover = registrationApprovers
                .FirstOrDefault(approver => approver.Level == nextLevel);

            //Get the succeeding approver
            var suceedingApprover = registrationApprovers.FirstOrDefault(ap => ap.Level ==  nextLevel + 1);

            if(suceedingApprover == null )
            {
                requestedClient.NextApproverId = null;
            }

            //If no approver, approve the request
            
            if (nextApprover == null)
            {
                requestedClient.Status = Status.Approved;
                requestedClient.Clients.RegistrationStatus = Status.Approved;
                requestedClient.NextApproverId = null;
                
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
            //Send the request to the next approver
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

            //Approval of listing fee
            if (requestedClient.Clients.ListingFees != null && request.ListingFeeRequestId != 0)
            {
                var listingFees = await _context.Requests
                .Include(listing => listing.ListingFee)
                .Where(lf => lf.Id == request.ListingFeeRequestId && lf.ListingFee.ClientId == requestedClient.Clients.Id)
                .FirstOrDefaultAsync(cancellationToken);

                if (listingFees is null)
                {
                    return ListingFeeErrors.NotFound();
                }

                var approvers = await _context.RequestApprovers
                    .Where(module => module.RequestId == request.OtherExpensesRequestId)
                    .ToListAsync(cancellationToken);
                var currentListingFeeApproverLevel = approvers
                    .FirstOrDefault(approver =>
                        approver.ApproverId == listingFees.CurrentApproverId)?.Level;

                if (approvers == null)
                {
                    return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
                }
                // Iterate over each approver
                foreach (var approver in approvers)
                {
                    // Set the current approver
                    listingFees.CurrentApproverId = approver.ApproverId;

                    // Add notification for the approver
                    var notificationForApprover = new Domain.Notification
                    {
                        UserId = approver.ApproverId,
                        Status = Status.ApprovedListingFee
                    };
                    await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

                    // Create a new approval entry
                    var newListingFeeApproval = new Approval(
                        listingFees.Id,
                        approver.ApproverId,
                        Status.Approved,
                        null,
                        true
                    );
                    await _context.Approval.AddAsync(newListingFeeApproval, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                listingFees.Status = Status.Approved;
                listingFees.ListingFee.Status = Status.Approved;

                var notification = new Domain.Notification
                {
                    UserId = listingFees.RequestorId,
                    Status = Status.ApprovedListingFee
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);
            }
            #endregion

            if (requestedClient.Clients.Expenses != null && request.OtherExpensesRequestId != 0)
            {
                var expenses = await _context.Requests
                .Include(expenses => expenses.Expenses)
                .Where(ex => ex.Id == request.OtherExpensesRequestId && ex.Expenses.ClientId == requestedClient.Clients.Id)
                .FirstOrDefaultAsync(cancellationToken);

                if (expenses is null)
                {
                    return ExpensesErrors.NotFound();
                }

                var approvers = await _context.RequestApprovers
                    .Where(module => module.RequestId == request.OtherExpensesRequestId)
                    .ToListAsync(cancellationToken);
                var currentExpensesApproverLevel = approvers
                    .FirstOrDefault(approver =>
                        approver.ApproverId == expenses.CurrentApproverId)?.Level;

                if (approvers == null)
                {
                    return ApprovalErrors.NoApproversFound(Modules.OtherExpensesApproval);
                }
                // Iterate over each approver
                foreach (var approver in approvers)
                {
                    // Set the current approver
                    expenses.CurrentApproverId = approver.ApproverId;

                    // Add notification for the approver
                    var notificationForApprover = new Domain.Notification
                    {
                        UserId = approver.ApproverId,
                        Status = Status.PendingExpenses
                    };
                    await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

                    // Create a new approval entry
                    var newExpensesApproval = new Approval(
                        expenses.Id,
                        approver.ApproverId,
                        Status.Approved,
                        null,
                        true
                    );
                    await _context.Approval.AddAsync(newExpensesApproval, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                expenses.Status = Status.Approved;
                expenses.Expenses.Status = Status.Approved;

                var notification = new Domain.Notification
                {
                    UserId = expenses.RequestorId,
                    Status = Status.ApprovedExpenses
                };

                await _context.Notifications.AddAsync(notification, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success();
        }
    }
}