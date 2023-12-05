using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category;

public class ProductSubCategoryErrors
{
    public static Error NotFound() => new Error("ProductSubCategory.NotFound", "No product sub category found");

    public static Error AlreadyExist(string productSubCategory) => new Error("ProductSubCategory.AlreadyExist", $"{productSubCategory} already exist");
}