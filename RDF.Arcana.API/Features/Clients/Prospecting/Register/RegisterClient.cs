using Microsoft.AspNetCore.Mvc;
using RDF.Arcana.API.Data;
using RDF.Arcana.API.Domain.New_Doamin;
using RDF.Arcana.API.Features.Clients.Prospecting.Exception;

namespace RDF.Arcana.API.Features.Clients.Direct;

[Route("api/Prospecting")]
[ApiController]

public class RegisterClient : ControllerBase
{

    public class RegisterClientCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string BusinessAdress { get; set; }
        public string AuthrizedRepreesentative { get; set; }
        public string AuthrizedRepreesentativePosition { get; set; }
        public bool Freezer  { get; set; }
        public string TypeofCustomer { get; set; }
        public bool DirectDelivery { get; set; }
        public int? BookingCoverage { get; set; }
        public int? Terms { get; set; }
        public int? FirxedDiscount { get; set; }
        public int? VariableDiscount { get; set; }

        public class Handler : IRequestHandler<RegisterClientCommand, Unit>
        {
            private readonly DataContext _conntext;

            public Handler(DataContext conntext)
            {
                _conntext = conntext;
            }

            public async Task<Unit> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
            {
                var existingClient = await _conntext.Clients
                    .Where(x => x.RegistrationStatus == "Released")
                    .FirstOrDefaultAsync(client => client.Id == request.ClientId);

                if (existingClient == null)
                {
                    throw new ClientIsNotFound();
                }

                var clientInfo = new Domain.New_Doamin.Clients
                {
                    BusinessAddress = request.BusinessAdress,
                    RepresentativeName = request.AuthrizedRepreesentative,
                    RepresentativePosition = request.AuthrizedRepreesentativePosition,
                };

                await _conntext.Clients.AddAsync(clientInfo, cancellationToken);
                await _conntext.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }
        }
    }
}