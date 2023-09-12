namespace RDF.Arcana.API.Features.Clients.Prospecting.Exception;

public class ClientIsNotFound : System.Exception
{
    public ClientIsNotFound(int clientId) : base($"Client {clientId} is not found"){}
}