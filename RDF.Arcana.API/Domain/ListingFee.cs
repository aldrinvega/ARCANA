using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ListingFee : BaseEntity
{
    public int ClientId { get; set; }
    public int RequestId { get; set; }
    public DateTime CratedAt { get; set; } = DateTime.UtcNow;
    public DateTime ApprovalDate { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public bool IsDelivered { get; set; }
    public string Status { get; set; }
    public int RequestedBy { get; set; }
    public decimal Total { get; set; }
    public virtual Clients Client { get; set; }
    public virtual User RequestedByUser { get; set; }
    
    public virtual Request Request { get; set; }
    public virtual ICollection<ListingFeeItems> ListingFeeItems { get; set; }
}