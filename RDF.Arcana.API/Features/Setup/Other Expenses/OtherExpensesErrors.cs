using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Setup.Other_Expenses;

public class OtherExpensesErrors
{
    public static Error NotFound() => new("OtherExpenses.NotFound", "Expenses not found");

    public static Error AlreadyExist(string expenses) =>
        new("OtherExpenses.AlreadyExist", $"{expenses} is already exist");
}