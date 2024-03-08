using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Special_Discount;

public static class SpecialDiscountErrors
{
    public static Error PendingRequest(string clientName) => new("SpecialDiscount.ExistingRequest", $"{clientName} has pending request");

    public static Error NotFound() => new("SpecialDiscount.NotFound", "Special Discount not found");

    public static Error AlreadyRejected() => new("SpecialDiscount.AlreadyRejected", "This special discount request is already rejected");
}
