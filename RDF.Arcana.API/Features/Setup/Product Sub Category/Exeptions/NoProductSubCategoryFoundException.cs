namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

public class NoProductSubCategoryFoundException : Exception
{
    public NoProductSubCategoryFoundException() : base("No Product Sub Category found"){}
}