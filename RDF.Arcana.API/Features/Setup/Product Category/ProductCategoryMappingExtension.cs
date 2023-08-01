using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

public static class ProductCategoryMappingExtension
{
    public static GetProductCategoryAsync.GetProductCategoryAsyncResult
        ToGetProductCategoryAsyncResult(this ProductCategory productCategory)
    {
        return new GetProductCategoryAsync.GetProductCategoryAsyncResult
        {
            Id = productCategory.Id,
            ProductCategoryName = productCategory.ProductCategoryName,
            AddedBy = productCategory.AddedByUser.Fullname,
            CreatedAt = productCategory.CreatedAt,
            IsActive = productCategory.IsActive,
            ProductSubCategory = productCategory.ProductSubCategory.Select(x => x.ProductSubCategoryName),
        };
    }
}