using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using System.Security.Claims;

namespace RDF.Arcana.API.Features.Setup.Mode_Of_Payment
{
    [Route("api/ModeOfPayment")]
    [ApiController]
    public class AddNewModeOfPayment : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddNewModeOfPayment(IMediator mediator)
        {
            _mediator = mediator;
        }

        public class AddNewModeOfPaymentCommand : IRequest<Result>
        {
            public string Payment { get; set; }
            public int AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<AddNewModeOfPaymentCommand, Result>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Result> Handle(AddNewModeOfPaymentCommand request, CancellationToken cancellationToken)
            {
                var existingPayment = await _context.ModeOfPayments.FirstOrDefaultAsync(x => x.Payment == request.Payment, cancellationToken);

                if (existingPayment != null)
                {
                    return ModeOfPaymentErrors.AlreadyExist(request.Payment);
                }

                var paymentMethod = new ModeOfPayment
                {
                    Payment = request.Payment,
                    AddedBy = request.AddedBy,
                  
                };

                await _context.ModeOfPayments.AddAsync(paymentMethod, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }

        [HttpPost("AddNewPaymentMethod")]
        public async Task<IActionResult> AddPaymentMethod(AddNewModeOfPaymentCommand command)
        {
            try
            {
                if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AddedBy = userId;
                }
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}