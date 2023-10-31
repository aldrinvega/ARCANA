namespace RDF.Arcana.API.Features.Setup.Discount;

public static class DiscountMappingExtension
{
    public static GetVariableDiscountsAsync.GetDiscountAsyncQueryResult
        ToGetDiscountAsyncQueryResult(this Domain.VariableDiscounts discount)
    {
        return new GetVariableDiscountsAsync.GetDiscountAsyncQueryResult
        {
            Id = discount.Id,
            MinimumAmount = discount.MinimumAmount,
            MaximumAmount = discount.MaximumAmount,
            MinimumPercentage = discount.MinimumPercentage,
            MaximumPercentage = discount.MaximumPercentage,
            IsActive = discount.IsActive
        };
    }
}