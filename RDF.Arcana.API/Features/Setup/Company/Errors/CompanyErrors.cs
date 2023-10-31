using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Company.Errors;

public static class CompanyErrors
{
    public static Error AlreadyExist(string company) =>
        new Error("Company.AlreadyExist", $"{company} is already exist");
}