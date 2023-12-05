using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Term_Days;

public static class TermDaysErrors
{
    public static Error NotFound() =>
        new Error("TermDays.NotFound", "Term Days not exist");
    
    public static Error AlreadyExist(int day) =>
        new Error("TermDays.AlreadyExist", $"Term Days {day} exist");
    
    public static Error ShouldBeNumber() =>
        new Error("TermDays.ShouldBeNumber", $"Search parameter should be a number representing days.");
}