using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Terms;

public static class TermErrors
{
    public static Error NotFound() =>
        new Error("Terms.NotFound", "Terms not exist");
    
    public static Error AlreadyExist(string term) =>
        new Error("Terms.NotFound", $"{term} already exist");
}