using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Approval : BaseEntity
{
    public Approval(int requestId, 
        int approverId, 
        string status)
    {
        RequestId = requestId;
        ApproverId = approverId;
        Status = status;
    }
    public int RequestId { get; set; }
    public int ApproverId { get; set; }
    public string Status { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    public virtual User Approver { get; set; }
    public virtual Request Request { get; set; }
}