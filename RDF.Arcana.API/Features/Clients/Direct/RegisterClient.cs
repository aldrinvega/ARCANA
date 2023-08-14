namespace RDF.Arcana.API.Features.Clients.Direct;

public class RegisterClient
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
        public int MyProperty { get; set; }

    }
}