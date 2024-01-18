using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Common;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Features.Client.Errors;

namespace RDF.Arcana.API.Features.Client.Prospecting.Request;

[Route("api/Prospecting")]
[ApiController]
public class UpdateProspectInformation : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateProspectInformation(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("UpdateProspectInformation/{id:int}")]
    public async Task<IActionResult> UpdateProspect([FromRoute] int id, [FromBody] UpdateProspectRequestCommand command)
    {
        try
        {
            command.ClientId = id;
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

    public class UpdateProspectRequestCommand : IRequest<Result>
    {
        public int ClientId { get; set; }
        public string OwnersName { get; set; }

        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string BarangayName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PhoneNumber { get; set; }
        public string BusinessName { get; set; }
        public int StoreTypeId { get; set; }
    }

    public class Handler : IRequestHandler<UpdateProspectRequestCommand, Result>
    {


        private readonly ArcanaDbContext _context;

        public Handler(ArcanaDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateProspectRequestCommand request, CancellationToken cancellationToken)
        {
            var existingClient =
                await _context.Clients
                    .Include(x => x.OwnersAddress)
                    .FirstOrDefaultAsync(
                        x => x.Id == request.ClientId
                             && x.IsActive == true,
                        cancellationToken);

            if (existingClient is null)
            {
                return ClientErrors.NotFound();
            }
            
            //Validate if the Client Business is already registered
            var validateBusinessName = await _context.Clients.Where(
                client => client.BusinessName == request.BusinessName && 
                          client.StoreTypeId == request.StoreTypeId && 
                          client.Fullname == request.OwnersName &&
                          client.Id != request.ClientId
            ).FirstOrDefaultAsync(cancellationToken);

            if (validateBusinessName is not null) 
            {
                return ClientErrors.BusinessAlreadyExist(request.BusinessName);
            }
            
            existingClient.Fullname = request.OwnersName;
            existingClient.PhoneNumber = request.PhoneNumber;
            existingClient.BusinessName = request.BusinessName;
            existingClient.RegistrationStatus = Status.Requested;
            existingClient.StoreTypeId = request.StoreTypeId;

            existingClient.OwnersAddress.HouseNumber = request.HouseNumber;
            existingClient.OwnersAddress.StreetName = request.StreetName;
            existingClient.OwnersAddress.Barangay = request.BarangayName;
            existingClient.OwnersAddress.City = request.City;
            existingClient.OwnersAddress.Province = request.Province;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}