using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class OtherExpenses : BaseEntity
{
    public string ExpenseType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public int AddedBy { get; set; }

    public virtual User AddedByUser { get; set; }
}