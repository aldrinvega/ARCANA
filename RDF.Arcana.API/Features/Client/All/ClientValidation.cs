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
            Console.WriteLine(e);
            throw;
        }
    }

    public class ClientValidationCommand : IRequest<Result>
    {
        public string BusinessName { get; set; }
        public string Fullname { get; set; }
        public int BusinessTypeId { get; set; }
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
            var validateFullname =
                await _context.Clients.Where(x => x.Fullname == request.Fullname).FirstOrDefaultAsync(cancellationToken);
            var validateBusinessName = await _context.Clients
                .Where(x => x.BusinessName == request.BusinessName && x.StoreTypeId == request.BusinessTypeId).FirstOrDefaultAsync(cancellationToken);

            if (validateFullname != null)
            {
                return ClientErrors.AlreadyExist(request.Fullname);
            }

            if (validateBusinessName != null)
            {
                return ClientErrors.BusinessAlreadyExist(request.BusinessName);
            }
            
            return Result.Success();
        }
    }
}