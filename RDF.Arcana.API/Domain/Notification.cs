using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Notification : BaseEntity
{
    public int? UserId { get; set; }
    public int? ClusterId { get; set; }
    public string Status { get; set; }
    public bool IsRead { get; set; }
    
    public virtual User User { get; set; }
}