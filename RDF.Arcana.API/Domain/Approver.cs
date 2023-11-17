using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Approver : BaseEntity
{
    public int UserId { get; set; }
    public string ModuleName { get; set; }
    public int Level { get; set; }

    public virtual User User { get; set; }
}