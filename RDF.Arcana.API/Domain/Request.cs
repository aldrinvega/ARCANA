using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Request : BaseEntity
{
    public Request(
        string module, 
        int requestorId, 
        int currentApproverId, 
        int? nextApproverId,
        string status)
    {
        Module = module;
        RequestorId = requestorId;
        CurrentApproverId = currentApproverId;
        NextApproverId = nextApproverId;
        Status = status;
    }
    public string Module { get; set; }
    public int RequestorId { get; set; }
    public int CurrentApproverId { get; set; }
    public int? NextApproverId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public virtual User Requestor { get; set; }
    public virtual User CurrentApprover { get; set; }
    public virtual User NextApprover { get; set; }
    public virtual FreebieRequest FreebieRequest { get; set; }
    public virtual ListingFee ListingFee { get; set; }
    public virtual Expenses Expenses { get; set; }
    public virtual Clients Clients { get; set; }
    public virtual ICollection<Approval> Approvals { get; set; }
    public virtual ICollection<UpdateRequestTrail> UpdateRequestTrails { get; set; }
    public virtual ICollection<RequestApprovers> RequestApprovers { get; set; }
    public virtual SpecialDiscount SpecialDiscount { get; set; }
}