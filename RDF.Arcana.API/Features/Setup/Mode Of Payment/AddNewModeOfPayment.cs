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

        public class AddNewModeOfPaymentCommand : IRequest<Unit>
        {
            public string Payment { get; set; }
            public int AddedBy { get; set; }
        }

        public class Handler : IRequestHandler<AddNewModeOfPaymentCommand, Unit>
        {
            private readonly ArcanaDbContext _context;

            public Handler(ArcanaDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddNewModeOfPaymentCommand request, CancellationToken cancellationToken)
            {
                var existingPayment = await _context.ModeOfPayments.FirstOrDefaultAsync(x => x.Payment == request.Payment, cancellationToken);

                if (existingPayment != null)
                {
                    throw new InvalidOperationException("Payment Method already exist");
                }

                var paymentMethod = new ModeOfPayment
                {
                    Payment = request.Payment,
                    AddedBy = request.AddedBy,
                  
                };

                await _context.ModeOfPayments.AddAsync(paymentMethod, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

        [HttpPost("AddNewPaymentMethod")]
        public async Task<IActionResult> AddPaymentMethod(AddNewModeOfPaymentCommand command)
        {
            var response = new QueryOrCommandResult<object>();
            try
            {
                if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AddedBy = userId;
                }
                await _mediator.Send(command);
                response.Success = true;
                response.Status = StatusCodes.Status200OK;
                response.Messages.Add($"Payment method added successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
                return Conflict(response);
            }
        }
    }
}