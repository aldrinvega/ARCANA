using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Request : BaseEntity
{
    public Request(string module, int requestorId, int currentApproverId, string status)
    {
        Module = module;
        RequestorId = requestorId;
        CurrentApproverId = currentApproverId;
        Status = status;
    }
    public string Module { get; set; }
    public int RequestorId { get; set; }
    public int CurrentApproverId { get; set; }
    public string Status { get; set; }

    public virtual User Requestor { get; set; }
    public virtual User CurrentApprover { get; set; }
    public virtual FreebieRequest FreebieRequest { get; set; }
    public virtual ListingFee ListingFee { get; set; }
    public virtual Clients Clients { get; set; }
    public virtual ICollection<Approval> Approvals { get; set; }
    
    
}