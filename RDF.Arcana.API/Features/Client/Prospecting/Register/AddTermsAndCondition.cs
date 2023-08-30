using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Prospecting.Register
{
    [Route("api/Registratiomn")]
    [ApiController]
    public class AddTermsAndCondition : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddTermsAndCondition(IMediator mediator)
        {
            _mediator = mediator;
        }

        public class AddTermsAndConditionsCommand : IRequest<Unit>
        {
            public int ClientId { get; set; }
            public bool Freezer { get; set; }
            public string TypeOfCustomer { get; set; }
            public bool DirectDelivery { get; set; }
            public int BookingCoverage { get; set; }
            public int ModeOfPayment { get; set; }
            public int Terms { get; set; }
            public int? VaribaleDiscount { get; set; }
            public int? FixedDiscount { get; set; }
        }

        public class Handler : IRequestHandler<AddTermsAndConditionsCommand, Unit>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddTermsAndConditionsCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _context.Clients.FirstOrDefaultAsync(
                    x => x.Id == request.ClientId, cancellationToken
                    );

                if (existingClient != null)
                {
                    existingClient.CustomerType = request.TypeOfCustomer;
                    existingClient.Freezer = request.Freezer;
                    existingClient.DirectDelivery = request.DirectDelivery;
                    existingClient.BookingCoverageId = request.BookingCoverage;
                    existingClient.ModeOfPayment = request.ModeOfPayment;
                    existingClient.Terms = request.Terms;
                    existingClient.VariableDiscountId = request.VaribaleDiscount ?? null;
                    existingClient.FixedDiscountId = request.FixedDiscount ?? null;

                    await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                else
                {
                    throw new ClientIsNotFound();
                }
            }
        }

        [HttpPut("AddTermsAndCondition/{id}")]
        public async Task<IActionResult> AddTermsConditon([FromBody]AddTermsAndConditionsCommand command, [FromRoute]int id)
        {
            var response = new QueryOrCommandResult<object>();
            try
            {
               command.ClientId = id;
               await _mediator.Send(command);
               response.Success = true;
               response.Status = StatusCodes.Status200OK;
               response.Messages.Add("Terms and Conditions added sucessfully");
                return Ok(response);
               
            }
            catch(System.Exception ex)
            {
                response.Messages.Add(ex.Message);
                response.Status = StatusCodes.Status404NotFound;

                return Conflict(response);
            }
        }
    }
}