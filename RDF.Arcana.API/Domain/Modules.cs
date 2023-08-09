using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Modules : BaseEntity
{
    public string ModuleName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
}
