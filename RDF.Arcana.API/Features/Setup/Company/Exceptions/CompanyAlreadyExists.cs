namespace RDF.Arcana.API.Features.Setup.Company.Exceptions;

public class CompanyAlreadyExists : Exception
{
    public CompanyAlreadyExists(string companyName) : base($"{companyName} is already exists, try something else"){}
}