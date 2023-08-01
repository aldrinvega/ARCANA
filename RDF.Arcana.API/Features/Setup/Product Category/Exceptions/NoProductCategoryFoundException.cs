namespace RDF.Arcana.API.Features.Setup.Product_Category.Exceptions;

public class NoProductCategoryFoundException : Exception
{
    public NoProductCategoryFoundException() : base("Product Category not exist"){}
}