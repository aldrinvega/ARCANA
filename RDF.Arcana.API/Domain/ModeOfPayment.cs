using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ModeOfPayment : BaseEntity
{
    public string Payment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public int AddedBy { get; set; }
    public bool IsActive { get; set; }
}

