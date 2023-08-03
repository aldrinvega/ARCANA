using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ApprovedClient : BaseEntity
{
    public int ClientId { get; set; }
    public string Reason { get; set; }
    public DateTime DateApproved { get; set; }
    public int ApprovedBy { get; set; }
    public int Status { get; set; }
    public bool IsActive { get; set; }

    // Navigation property
    public Client Client { get; set; }
    public User ApprovedByUser { get; set; }
    public Status ApprovedStatus { get; set; }
    public virtual IEnumerable<FreebieRequest> Freebies { get; set; }
}