using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ExpensesRequest : BaseEntity
{
    public int ExpensesId  { get; set; }
    public int OtherExpenseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual Expenses Expenses { get; set; }
    public virtual OtherExpenses OtherExpense { get; set; }
}