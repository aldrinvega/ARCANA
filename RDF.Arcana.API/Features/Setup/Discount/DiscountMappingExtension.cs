namespace RDF.Arcana.API.Features.Setup.Discount;

public static class DiscountMappingExtension
{
    public static GetDiscountsAsync.GetDiscountAsyncQueryResult
        ToGetDiscountAsyncQueryResult(this Domain.Discount discount)
    {
        return new GetDiscountsAsync.GetDiscountAsyncQueryResult
        {
            Id = discount.Id,
            LowerBound = discount.LowerBound,
            UpperBound = discount.UpperBound,
            CommissionRateLower = discount.CommissionRateLower,
            CommissionRateUpper = discount.CommissionRateUpper,
            AddedBy = discount.AddedByUser.Fullname,
            CreatedAt = discount.CreatedAt,
            IsActive = discount.IsActive,
            UpdateAt = discount.UpdateAt
        };
    }
}