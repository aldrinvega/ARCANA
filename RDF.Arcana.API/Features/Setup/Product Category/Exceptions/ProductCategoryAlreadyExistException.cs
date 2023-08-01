namespace RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

public class ProductCategoryAlreadyExistException : Exception
{
    public ProductCategoryAlreadyExistException() : base("Product Category already exist"){}
}