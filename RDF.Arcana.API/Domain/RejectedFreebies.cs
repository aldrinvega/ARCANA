using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class RejectedFreebies : BaseEntity
{
    [ForeignKey("FreebieRequest")]
    public int FreebieRequestId { get; set; }

    public virtual FreebieRequest FreebieRequest { get; set; }

    public string RejectionReason { get; set; }

    public DateTime RejectedAt { get; set; }

    [ForeignKey("RejectedByUser")]
    public int RejectedBy { get; set; }

    public virtual User RejectedByUser { get; set; }
}