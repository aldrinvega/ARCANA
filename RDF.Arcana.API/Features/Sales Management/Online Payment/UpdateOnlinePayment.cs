using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment;

[Route("api/UpdateOnlinePayment"), ApiController]
public class UpdateOnlinePayment : ControllerBase
{
    private readonly IMediator _mediator;
    public UpdateOnlinePayment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOnlinePaymentCommand command)
    {
        try
        {
            if (User.Identity is ClaimsIdentity identity
                     && IdentityHelper.TryGetUserId(identity, out var userId))
            {
                command.OnlinePaymentId = id;
                command.ModifiedBy = userId;
            }
            var result = await _mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return Conflict(ex.Message);
        }
    }

    public class UpdateOnlinePaymentCommand : IRequest<Result>
    {
        public int OnlinePaymentId { get; set; }
        public string OnlinePlatform { get; set; }
        public bool IsActive { get; set; }
        public int ModifiedBy { get; set; }
    }


    public class Handler : IRequestHandler<UpdateOnlinePaymentCommand, Result>
    {
        private readonly ArcanaDbContext _context;
        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateOnlinePaymentCommand request, CancellationToken cancellationToken)
        {
            var onlinePlatform = await _context.OnlinePayments
                .FirstOrDefaultAsync(op => op.Id == request.OnlinePaymentId, cancellationToken);

            if (onlinePlatform is null) 
            {
                return OnlinePaymentErrors.NotFound();
            }

            var existingPlatform = await _context.OnlinePayments
                .AnyAsync(op => op.OnlinePlatform == request.OnlinePlatform && op.Id != request.OnlinePaymentId, cancellationToken);

            if (existingPlatform)
            {
                return OnlinePaymentErrors.ExistingOnlinePlatform();
            }

            if (request.IsActive != onlinePlatform.IsActive)
            {
                onlinePlatform.IsActive = request.IsActive;
            }
            else
            {
                onlinePlatform.OnlinePlatform = request.OnlinePlatform;
            }

            onlinePlatform.UpdatedAt = DateTime.Now;
            onlinePlatform.ModifiedBy = request.ModifiedBy;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
