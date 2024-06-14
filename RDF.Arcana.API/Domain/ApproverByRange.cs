using RDF.Arcana.API.Common;
using RDF.Arcana.API.Domain;

public class ApproverByRange : BaseEntity
{
    public int UserId { get; set; }
    public string ModuleName { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public bool IsActive { get; set; } = true;

    public virtual User User { get; set; }
}
