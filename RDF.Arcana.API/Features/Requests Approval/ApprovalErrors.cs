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
    public static Error NotAllowed(string moduleName) =>
        new Error("Approval.NotAllowed", $"You are not allowed to approve {moduleName} request");
    public static Error ExistingRequest(string moduleName) =>
        new Error("Approval.ExistingRequest", $"There are already approval requests for the module {moduleName}. Please resolve them before updating approvers.");
    public static Error ExistingFeeRangeOrModule() => new Error("ExistingFeeRangeOrModule", "Please Check Fee Range and Module");
    public static Error ApproverExist() => new Error("ApproverAlreadyExist", "Approver Already Exist");
    public static Error ApproverNotFound() => new Error("ApproverNotFound", "Approver Not Found");
    public static Error MinMaxError() => new Error("MinMaxError", "MinValue should be less than MaxValue");
}