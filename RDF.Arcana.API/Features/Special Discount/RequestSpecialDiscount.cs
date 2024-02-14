using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Client.Errors;
using RDF.Arcana.API.Features.Requests_Approval;

namespace RDF.Arcana.API.Features.Special_Discount;

[Route("api/special-discount"), ApiController]

public class RequestSpecialDiscount : ControllerBase
{
    private readonly IMediator _mediator;

    public RequestSpecialDiscount(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RequestSpecialDiscountCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.AddedBy = userId;
            }

            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public class RequestSpecialDiscountCommand : IRequest<Result>
    {
        public int ClientId
        {
            get;
            set;
        }
        public decimal Discount
        {
            get;
            set;
        }
        public bool IsOnetime
        {
            get;
            set;
        }

        public int AddedBy { get; set; } 
    }

    public class Handler : IRequestHandler<RequestSpecialDiscountCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(RequestSpecialDiscountCommand request, CancellationToken cancellationToken)
        {
            var discount = request.Discount / 100;

            var client = await _context.Clients
                .FirstOrDefaultAsync(cl => cl.Id == request.ClientId, cancellationToken);

            if (client == null)
            {
                return ClientErrors.NotFound();
            }

            var approvers = await _context.Approvers
                .Where(x => x.ModuleName == Modules.SpecialDiscountApproval)
                .OrderBy(x => x.Level)
                .ToListAsync(cancellationToken);

            if (!approvers.Any())
            {
                return ApprovalErrors.NoApproversFound(Modules.SpecialDiscountApproval);
            }

            var newRequest = new Request(
                Modules.SpecialDiscountApproval,
                request.AddedBy,
                approvers.First().UserId,
                Status.UnderReview
            );

            await _context.Requests.AddAsync(newRequest, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var newRequestApprover in approvers.Select(approver => new RequestApprovers
            {
                ApproverId = approver.UserId,
                RequestId = newRequest.Id,
                Level = approver.Level,
            }))
            {
                _context.RequestApprovers.Add(newRequestApprover);
            }

            var requestSpecialDiscount = new SpecialDiscount
            {
                ClientId = client.Id,
                AddedBy = request.AddedBy,
                Discount = discount,
                RequestId = newRequest.Id,
                IsOneTime = request.IsOnetime,
            };

            await _context.SpecialDiscounts.AddAsync(requestSpecialDiscount, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
