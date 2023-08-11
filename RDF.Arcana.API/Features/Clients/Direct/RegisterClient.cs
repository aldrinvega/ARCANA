namespace RDF.Arcana.API.Features.Clients.Direct;

public class RegisterClient
{
    public class RegisterClientCommand : IRequest<Unit>
    {
        public int ClientId { get; set; }
        public string BusinessAdress { get; set; }
    }
}