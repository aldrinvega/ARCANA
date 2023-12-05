using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Product_Category;

public class ProductCategoryErrors
{
    public static Error NotFound() => new Error("ProductCategory.NotFound", "No product category found");

    public static Error AlreadyExist(string productCategory) => new Error("ProductCategory.AlreadyExist", $"{productCategory} is already exist");
    
}