using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.All;

[Route("api/Validation"), ApiController]
public class ClientValidation : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientValidation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("ValidateClient")]
    public async Task<IActionResult> Validate([FromBody] ClientValidationCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return result.IsFailure ? BadRequest(result) : Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    public class ClientValidationCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public string BusinessName { get; set; }
        public string Fullname { get; set; }
        public int StoreTypeId { get; set; }
        public string Municipality { get; set; }
        public string BarangayName { get; set; }
    }
    public class Handler : IRequestHandler<ClientValidationCommand, Result>
    {
        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(ClientValidationCommand request, CancellationToken cancellationToken)
        {
            var existingBusiness = await _context.Clients
                .Where(x =>
                    /*(request.ClientId == 0 || x.Id != request.ClientId) &&*/
                    x.Fullname == request.Fullname &&
                     x.StoreType.Id == request.StoreTypeId &&
                     x.BusinessName == request.BusinessName &&
                     x.BusinessAddress.City == request.Municipality &&
                     x.BusinessAddress.StreetName == request.BarangayName &&
                     x.RegistrationStatus != Status.Voided)
                .FirstOrDefaultAsync(cancellationToken);

            return existingBusiness != null 
                ? ClientErrors.AlreadyExist(request.BusinessName) 
                : Result.Success();
        }
    }
}