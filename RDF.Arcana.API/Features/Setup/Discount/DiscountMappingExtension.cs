namespace RDF.Arcana.API.Features.Setup.Discount;

public static class DiscountMappingExtension
{
    public static GetDiscountsAsync.GetDiscountAsyncQueryResult
        ToGetDiscountAsyncQueryResult(this Domain.Discount discount)
    {
        return new GetDiscountsAsync.GetDiscountAsyncQueryResult
        {
            Id = discount.Id,
            MinimumAmount = discount.LowerBound,
            MaximumAmount = discount.UpperBound,
            MinimumPercentage = discount.CommissionRateLower,
            MaximumPercentage = discount.CommissionRateUpper,
            AddedBy = discount.AddedByUser.Fullname,
            CreatedAt = discount.CreatedAt,
            IsActive = discount.IsActive,
            UpdateAt = discount.UpdateAt
        };
    }
}