namespace RDF.Arcana.API.Features.Setup.Product_Sub_Category.Exeptions;

public class ProductSubCategoryAlreadyExistException : Exception
{
    public ProductSubCategoryAlreadyExistException(string productsubcategName) : base($"{productsubcategName} is already exist"){}
}