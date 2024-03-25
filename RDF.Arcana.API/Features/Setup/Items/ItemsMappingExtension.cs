namespace RDF.Arcana.API.Features.Setup.Items;

public static class ItemsMappingExtension
{
   
    public static GetItemsAsync.GetItemsAsyncResult
        ToGetItemsAsyncResult(this Domain.Items items)
    {
        var now = DateTime.Now;
        return new GetItemsAsync.GetItemsAsyncResult
        {
            Id = items.Id,
            ItemId = items.Id,
            ItemCode = items.ItemCode,
            ItemDescription = items.ItemDescription,
            ItemImageLink = items.ItemImageLink,
            Uom = items.Uom?.UomCode,
            ProductCategory = items.ProductSubCategory.ProductCategory.ProductCategoryName,
            ProductSubCategoryName = items.ProductSubCategory.ProductSubCategoryName,
            MeatType = items.MeatType?.MeatTypeName,
            IsActive = items.IsActive,
            //PriceChangeHistories = items.ItemPriceChange
            //    .Where(pc => pc.EffectivityDate <= now)
            //    .OrderByDescending(p => p.EffectivityDate)
            //    .Select(pc => new GetItemsAsync.GetItemsAsyncResult.PriceChangeHistory
            //    {
            //        Id = pc.Id,
            //        Price = pc.Price,
            //        EffectivityDate = pc.EffectivityDate.ToString("MM/dd/yyyy HH:mm:ss")
            //    }),
            //FuturePriceChanges = items.ItemPriceChange
            //    .Where(p => p.EffectivityDate > now)
            //    .Select(pc => new GetItemsAsync.GetItemsAsyncResult.FuturePriceChange
            //    {
            //        Id = pc.Id,
            //        Price = pc.Price,
            //        EffectivityDate = pc.EffectivityDate.ToString("MM/dd/yyyy HH:mm:ss")
            //    }),
            AddedBy = items.AddedByUser.Fullname,
            ModifiedBy = items.ModifiedBy
        };
    }
}