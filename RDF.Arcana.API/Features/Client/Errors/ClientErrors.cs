using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Client.Errors;

public class ClientErrors
{
    public static Error NotFound() =>
        new Error("Client.NotFound", "Client not found");

    public static Error AlreadyRejected(string businessName) =>
        new Error("Client.AlreadyRejected", $"{businessName} is already rejected");
    
    public static Error AlreadyApproved(string businessName) =>
        new Error("Client.AlreadyApproved", $"{businessName} is already approved");

    public static Error AlreadyExist(string fullname) =>
        new Error("Client.AlreadyExist", $"{fullname} is already registered");

    public static Error BusinessAlreadyExist(string businessName) =>
        new Error("Client.BusinessAlreadyExist", $"{businessName} is already registered");

    public static Error Unauthorized() =>
        new Error("Client.Unauthorized", "You are not authorized to reject this client");
}