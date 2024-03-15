using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Expenses;
using RDF.Arcana.API.Features.Listing_Fee.Errors;
using RDF.Arcana.API.Features.Requests_Approval;
using static RDF.Arcana.API.Features.Listing_Fee.GetAllClientsInListingFee.GetAllClientsInListingFeeResult;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/Clients"), ApiController]
public class RejectClientRegistration : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectClientRegistration(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("RejectClientRegistration/{id:int}")]
    public async Task<IActionResult> RejectClients(
        [FromRoute] int id, 
        [FromQuery] int listingFeeRequestId, 
        [FromQuery] int otherExpensesRequestId,
        [FromBody] RejectClientCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
            {
                command.AccessBy = userId;
            }
            
            command.ClientRequestId = id;
            command.ListingFeeRequestId = listingFeeRequestId;
            command.OtherExpensesRequestId = otherExpensesRequestId;

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public sealed record RejectClientCommand : IRequest<Result>
    {
        public int ClientRequestId { get; set; }
        public int? ListingFeeRequestId { get; set; }
        public int? OtherExpensesRequestId { get; set; }
        public string Reason { get; set; }
        public int AccessBy { get; set; }
    }

    public sealed record RejectedClientResult
    {
        public int ClientId { get; set; }
        public string Fullname { get; set; }
        public string BusinessName { get; set; }
        public string Reason { get; set; }
    }

    public class Handler : IRequestHandler<RejectClientCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RejectClientCommand request,
            CancellationToken cancellationToken)
        {

            var existingClientRequest = await _context.Requests
                .Include(client => client.Clients)
                .ThenInclude(lf => lf.ListingFees)
                .ThenInclude(x => x.ListingFeeItems)
                .Include(client => client.Clients)
                .ThenInclude(lf => lf.ListingFees)
                .ThenInclude(x => x.Request)
                .ThenInclude(x => x.UpdateRequestTrails)
                .Include(client => client.Clients)
                .ThenInclude(lf => lf.ListingFees)
                .ThenInclude(x => x.Request)
                .ThenInclude(x => x.RequestApprovers)
                .Include(client => client.Clients)
                .ThenInclude(ep => ep.Expenses)
                .ThenInclude(x => x.ExpensesRequests)
                .Include(client => client.Clients)
                .ThenInclude(lf => lf.Expenses)
                .ThenInclude(x => x.Request)
                .ThenInclude(x => x.UpdateRequestTrails)
                .Include(client => client.Clients)
                .ThenInclude(lf => lf.Expenses)
                .ThenInclude(x => x.Request)
                .ThenInclude(x => x.RequestApprovers)
                .Where(rq => rq.Id == request.ClientRequestId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingClientRequest == null)
            {
                return ClientErrors.NotFound();
            }

            if (existingClientRequest.Status == Status.Rejected)
            {
                return ClientErrors.AlreadyRejected(existingClientRequest.Clients.BusinessName);
            }

            if (existingClientRequest.CurrentApproverId != request.AccessBy)
            {
                return ApprovalErrors.NotAllowed(Modules.RegistrationApproval);
            }

            if (existingClientRequest.Status == Status.Rejected)
            {
                return ClientErrors.AlreadyRejected(existingClientRequest.Status);
            }

            //// Check if there are no listing fee items left, and delete the request if true
            //if (existingClientRequest.Clients != null && existingClientRequest.Clients.ListingFees != null &&
            //     existingClientRequest.Clients.ListingFees.Any(lf => lf.Status == Status.UnderReview))
            //{
            //    foreach (var listingFee in existingClientRequest.Clients.ListingFees)
            //    {
            //        if (listingFee.Request != null)
            //        {
            //            _context.UpdateRequestTrails.RemoveRange(listingFee.Request.UpdateRequestTrails);
            //            _context.Requests.Remove(listingFee.Request);
            //        }
            //        if (listingFee.ListingFeeItems != null)
            //        {
            //            _context.ListingFeeItems.RemoveRange(listingFee.ListingFeeItems);
            //        }
            //        if (listingFee.Request.RequestApprovers != null)
            //        {
            //            _context.RequestApprovers.RemoveRange(listingFee.Request.RequestApprovers);
            //        }
            //        _context.ListingFees.Remove(listingFee);
            //    }

            //    await _context.SaveChangesAsync(cancellationToken);
            //}

            //// Check if there are no listing fee items left, and delete the request if true
            //if (existingClientRequest.Clients != null && existingClientRequest.Clients.Expenses != null &&
            //     existingClientRequest.Clients.Expenses.Any(lf => lf.Status == Status.UnderReview))
            //{
            //    foreach (var expenses in existingClientRequest.Clients.Expenses)
            //    {
            //        if (expenses.Request != null)
            //        {
            //            _context.UpdateRequestTrails.RemoveRange(expenses.Request.UpdateRequestTrails);
            //            _context.Requests.Remove(expenses.Request);
            //        }
            //        if (expenses.ExpensesRequests != null)
            //        {
            //            _context.ExpensesRequests.RemoveRange(expenses.ExpensesRequests);
            //        }
            //        if (expenses.Request.RequestApprovers != null)
            //        {
            //            _context.RequestApprovers.RemoveRange(expenses.Request.RequestApprovers);
            //        }
            //        _context.Expenses.Remove(expenses);
            //    }

            //    await _context.SaveChangesAsync(cancellationToken);
            //}

            //Approval of listing fee
            if (existingClientRequest.Clients.ListingFees != null && request.ListingFeeRequestId != 0)
            {
                var listingFees = await _context.Requests
                .Include(listing => listing.ListingFee)
                .Where(lf => lf.Id == request.ListingFeeRequestId && lf.ListingFee.ClientId == existingClientRequest.Clients.Id)
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
                    var notificationListingFeeForApprover = new Domain.Notification
                    {
                        UserId = approver.ApproverId,
                        Status = Status.RejectedListingFee
                    };
                    await _context.Notifications.AddAsync(notificationListingFeeForApprover, cancellationToken);

                    // Create a new approval entry
                    var newListingFeeApproval = new Approval(
                        listingFees.Id,
                        approver.ApproverId,
                        Status.Rejected,
                        request.Reason,
                        true
                    );
                    await _context.Approval.AddAsync(newListingFeeApproval, cancellationToken);

                    await _context.SaveChangesAsync(cancellationToken);
                }

                listingFees.Status = Status.Rejected;
                listingFees.ListingFee.Status = Status.Rejected;

                var notificationListingFeeForRequestor = new Domain.Notification
                {
                    UserId = listingFees.RequestorId,
                    Status = Status.RejectedListingFee
                };

                await _context.Notifications.AddAsync(notificationListingFeeForRequestor, cancellationToken);
            }

            if (existingClientRequest.Clients.Expenses != null && request.OtherExpensesRequestId != 0)
            {
                var expenses = await _context.Requests
                .Include(expenses => expenses.Expenses)
                .Where(ex => ex.Id == request.OtherExpensesRequestId && ex.Expenses.ClientId == existingClientRequest.Clients.Id)
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
                    var notificationExpensesForApprover = new Domain.Notification
                    {
                        UserId = approver.ApproverId,
                        Status = Status.RejectedExpenses
                    };
                    await _context.Notifications.AddAsync(notificationExpensesForApprover, cancellationToken);

                    // Create a new approval entry
                    var newExpensesApproval = new Approval(
                        expenses.Id,
                        approver.ApproverId,
                        Status.Rejected,
                        request.Reason,
                        true
                    );
                    await _context.Approval.AddAsync(newExpensesApproval, cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                expenses.Status = Status.Rejected;
                expenses.Expenses.Status = Status.Rejected;

                var notificationForRequestor = new Domain.Notification
                {
                    UserId = expenses.RequestorId,
                    Status = Status.RejectedExpenses
                };

                await _context.Notifications.AddAsync(notificationForRequestor, cancellationToken);
            }

            var newApproval = new Approval
            (
                existingClientRequest.Id,
                existingClientRequest.CurrentApproverId,
                Status.Rejected,
                request.Reason,
                true
            );
            
            await _context.Approval.AddAsync(newApproval, cancellationToken);
            
            existingClientRequest.Status = Status.Rejected;
            existingClientRequest.Clients.RegistrationStatus = Status.Rejected;
            
            var notification = new Domain.Notification
            {
                UserId = existingClientRequest.RequestorId,
                Status = Status.RejectedClients
            };
                
            await _context.Notifications.AddAsync(notification, cancellationToken);
                
            var notificationForApprover = new Domain.Notification
            {
                UserId = existingClientRequest.CurrentApproverId,
                Status = Status.RejectedClients
            };
                
            await _context.Notifications.AddAsync(notificationForApprover, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var result = new RejectedClientResult
            {
                ClientId = existingClientRequest.Id,
                Fullname = existingClientRequest.Clients.Fullname,
                BusinessName = existingClientRequest.Clients.BusinessName,
                Reason = request.Reason
            };

            return Result.Success(result);
        }
    }
}