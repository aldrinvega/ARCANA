using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Features.Expenses;

public class ExpensesErrors
{
    public static Error NotFound() => new("Expenses.NotFound", "Expense request not found");
}