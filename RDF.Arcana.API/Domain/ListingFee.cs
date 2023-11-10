using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ListingFee : BaseEntity
{
    public int ClientId { get; set; }
    public int ApprovalsId { get; set; }
    public DateTime CratedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public bool IsDelivered { get; set; }
    public string Status { get; set; }
    public int RequestedBy { get; set; }
    public int? ApprovedBy { get; set; }
    public decimal Total { get; set; }
    public virtual Clients Client { get; set; }
    public virtual User RequestedByUser { get; set; }
    public virtual User ApprovedByUser { get; set; }
    public Approvals Approvals { get; set; }
    public virtual ICollection<ListingFeeItems> ListingFeeItems { get; set; }
}