using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class RequestApprovers : BaseEntity
{
    public int RequestId { get; set; }
    public int ApproverId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Level { get; set; }
    
    public virtual Request Request { get; set; }

    public virtual User Approver { get; set; }
}