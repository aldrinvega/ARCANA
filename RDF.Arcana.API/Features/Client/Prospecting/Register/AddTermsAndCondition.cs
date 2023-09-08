using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Client.Prospecting.Register
{
    [Route("api/Registration")]
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
            public int CreditLimit { get; set; }
            public int TermDaysId { get; set; }
            public DiscountTypes DiscountTypes { get; set; }
            public int AddedBy { get; set; }
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


                    var tersmOptions = new TermOptions
                    {
                        ClientId = request.ClientId,
                        TermId = request.Terms,
                        CreditLimit = request.CreditLimit,
                        TermDaysId = request.TermDaysId,
                        AddedBy = request.AddedBy
                    };

                    existingClient.DiscountType = request.DiscountTypes;

                    await _context.TermOptions.AddAsync( tersmOptions, cancellationToken );

                    await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                else
                {
                    throw new ClientIsNotFound(request.ClientId);
                }
            }
        }

        [HttpPut("AddTermsAndCondition/{id}")]
        public async Task<IActionResult> AddTermsConditon([FromBody]AddTermsAndConditionsCommand command, [FromRoute]int id)
        {
            var response = new QueryOrCommandResult<object>();
            try
            {

                if (User.Identity is ClaimsIdentity identity
                && int.TryParse(identity.FindFirst("id")?.Value, out var userId))
                {
                    command.AddedBy = userId;
                }

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