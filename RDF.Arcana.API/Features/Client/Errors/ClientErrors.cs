using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Client.Errors;

public class ClientErrors
{
    public static Error NotFound() =>
        new("Client.NotFound", "Client not found");

    public static Error AlreadyRejected(string businessName) =>
        new("Client.AlreadyRejected", $"{businessName} is already rejected");
    
    public static Error AlreadyApproved(string businessName) =>
        new("Client.AlreadyApproved", $"{businessName} is already approved");

    public static Error AlreadyExist(string fullname) =>
        new("Client.AlreadyExist", $"{fullname} is already registered");

    public static Error BusinessAlreadyExist(string businessName) =>
        new("Client.BusinessAlreadyExist", $"{businessName} is already registered");

    public static Error Unauthorized() =>
        new("Client.Unauthorized", "You are not authorized to reject this client");

    public static Error FreezerAlreadyTagged(string tag) =>
        new("Freezer.FreezerAlreadyTagged", $"Freezer with tag {tag} is already used");
}