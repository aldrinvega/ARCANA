using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Common.Helpers;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Setup.UserRoles;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Sales_Management.Online_Payment
{
    [Route("api/OnlinePayment"), ApiController]
    public class AddNewOnlinePayment : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddNewOnlinePayment(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddNewOnlinePayment")]
        public async Task<IActionResult> Add([FromBody]AddNewOnlinePaymentCommand command)
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
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        public class AddNewOnlinePaymentCommand : IRequest<Result>
        {
            public string OnlinePlatform { get; set; }
            public int AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<AddNewOnlinePaymentCommand, Result>
        {
            private readonly ArcanaDbContext _context;
            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddNewOnlinePaymentCommand request, CancellationToken cancellationToken)
            {

                var existingOnlinePlatforms = await _context.OnlinePayments
                    .FirstOrDefaultAsync(ol => ol.OnlinePlatform == request.OnlinePlatform, cancellationToken);

                if (existingOnlinePlatforms is not null)
                {
                    return OnlinePaymentErrors.ExistingOnlinePlatform();
                }

                var onlinePayment = new OnlinePayments
                {
                    AddedBy = request.AddedBy,
                    OnlinePlatform = request.OnlinePlatform
                    
                };

                await _context.OnlinePayments.AddAsync(onlinePayment, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(onlinePayment);
            }
        }
    }
}
