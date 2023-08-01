using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Status : BaseEntity
{
    public string StatusName { get; set; }
    public int? AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Updated { get; set; }

    public virtual User AddedByUser { get; set; }
    public virtual User ModifiedByUser { get; set; }
}