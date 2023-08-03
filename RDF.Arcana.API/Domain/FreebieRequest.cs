using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class FreebieRequest : BaseEntity
{
    [ForeignKey("ApprovedClient")]
    public int ClientId { get; set; }
    public virtual ApprovedClient ApprovedClient { get; set; }
 
    public string TransactionNumber { get; set; }
 
    [ForeignKey("FreebieStatus")]
    public int StatusId { get; set; }
    public virtual Status FreebieStatus { get; set; }
    
    [ForeignKey("AddedByUser")]
    public int AddedBy { get; set; }
    public virtual User AddedByUser { get; set; }
 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }

    public virtual ICollection<Freebies> Freebies { get; set; }
}