using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Company.Errors;

public static class CompanyErrors
{
    public static Error AlreadyExist(string company) => new("Company.AlreadyExist", $"{company} is already exist");

    public static Error NotFound() => new("Company.NotFound", "No company found");
    
}