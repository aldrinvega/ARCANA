using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class CdoCluster : BaseEntity
{
    public int ClusterId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    
    public virtual Cluster Cluster { get; set; }
    public virtual User User { get; set; }
}