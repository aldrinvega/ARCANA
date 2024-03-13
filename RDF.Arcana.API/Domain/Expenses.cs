using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Expenses : BaseEntity
{
    public int ClientId { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int AddedBy { get; set; }
    public string Status { get; set; }
    public int? ModifiedBy { get; set; }
    public int RequestId { get; set; }

    public virtual Request Request { get;set; }
    public virtual User AddedByUser { get; set; }
    public virtual User ModifiedByUser { get; set; }
    public virtual Clients Client { get; set; }
    public virtual ICollection<ExpensesRequest> ExpensesRequests { get; set; }
}