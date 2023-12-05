using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Approvals : BaseEntity
{
    public int ClientId { get; set; }
    public Clients Client { get; set; }
    public string ApprovalType { get; set; }
    public string Reason { get; set; }
    public bool IsApproved { get; set; }
    public bool IsActive { get; set; }
    public int RequestedBy { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public User ApproveByUser { get; set; }
    public User RequestedByUser { get; set; }
    public virtual ICollection<FreebieRequest> FreebieRequest { get; set; }
    /*public virtual ICollection<ListingFee> ListingFee { get; set; }*/
}