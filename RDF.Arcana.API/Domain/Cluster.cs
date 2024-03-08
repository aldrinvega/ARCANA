using System.Collections;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Cluster : BaseEntity
{
    public string ClusterType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public virtual ICollection<CdoCluster> CdoClusters { get; set; }
}