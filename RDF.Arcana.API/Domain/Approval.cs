using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Approval : BaseEntity
{
    public Approval(int requestId, 
        int approverId, 
        string status,
        string reason,
        bool isActive)
    {
        RequestId = requestId;
        ApproverId = approverId;
        Status = status;
        Reason = reason;
        IsActive = isActive;
    }
    public int RequestId { get; set; }
    public int ApproverId { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public bool IsActive { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    public virtual User Approver { get; set; }
    public virtual Request Request { get; set; }
}