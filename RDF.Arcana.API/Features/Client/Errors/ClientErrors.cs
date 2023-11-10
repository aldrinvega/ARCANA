using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Client.Errors;

public class ClientErrors
{
    public static Error NotFound() =>
        new Error("Client.NotFound", "Client not found");

    public static Error AlreadyRejected(string businessName) =>
        new Error("Client.AlreadyRejected", $"{businessName} is already rejected");

    public static Error AlreadyExist(string fullname, string businessName) =>
        new Error("Client.AlreadyRejected", $"{fullname} has already registered a business with {businessName}.");
}