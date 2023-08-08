using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.New_Doamin;

public class FreebieRequest : BaseEntity
{
    [ForeignKey("Clients")]
    public int ClientId { get; set; }
    public Clients Clients { get; set; }
    public int ApprovalId { get; set; }
    public Approvals Approvals { get; set; }
    public string TransactionNumber { get; set; }
    public string Status { get; set; }
    public bool IsDelivered { get; set; }
    public string PhotoProofPath { get; set; }
    
    public virtual ICollection<FreebieItems> FreebieItems { get; set; }
}