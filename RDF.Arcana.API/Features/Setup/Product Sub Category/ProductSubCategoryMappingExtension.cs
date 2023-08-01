using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

public static class ProductSubCategoryMappingExtension
{
    public static GetProductSubCategories.GetProductSubCategoriesResult
        GetProductSubCategoriesResult(this ProductSubCategory productSubCategory)
    {
        return new GetProductSubCategories.GetProductSubCategoriesResult
        {
            Id = productSubCategory.Id,
            ProductCategoryName = productSubCategory.ProductCategory.ProductCategoryName,
            ProductSubCategoryName = productSubCategory.ProductSubCategoryName,
            CreatedAt = productSubCategory.CreatedAt,
            AddedBy = productSubCategory.AddedByUser.Fullname,
            UpdatedAt = productSubCategory.UpdatedAt,
            ModifiedBy = productSubCategory.ModifiedBy,
            IsActive = productSubCategory.IsActive
        };
    }
}