using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ApprovedFreebies : BaseEntity
{
        [ForeignKey("FreebieRequest")]
        public int FreebieRequestId { get; set; }
    
        public virtual FreebieRequest FreebieRequest { get; set; }

        public byte[] PhotoProof { get; set; } //Store image on disk and store path in DB if the file size is large.

        public DateTime ApprovedAt { get; set; }
    
        [ForeignKey("ApprovedByUser")]
        public int ApprovedBy { get; set; }
    
        public virtual User ApprovedByUser { get; set; }
}