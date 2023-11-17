using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Requests_Approval;

public class ApprovalErrors
{
    public static Error ApproverAlreadyExist(string moduleName) =>
        new Error("Approval.ApproverAlreadyExist", $"Approver already exist in the {moduleName} module.");
    public static Error NoAccess(string moduleName, string fullname) =>
        new Error("Approval.NoAccess", $"{fullname} cannot be assigned due to no access to the {moduleName} module.");
    public static Error NoApproversFound(string moduleName) =>
        new Error("Approval.NoApproversFound", $"No approvers found for {moduleName}");
}