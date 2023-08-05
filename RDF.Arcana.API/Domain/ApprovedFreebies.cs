using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ApprovedFreebies : BaseEntity
{
        [ForeignKey("Freebie")]
        public int FreebiesId { get; set; }
        public virtual Freebies Freebie { get; set; }
 
        public string TransactionNumber { get; set; }
 
        [ForeignKey("FreebieStatus")]
        public int StatusId { get; set; }
        public virtual Status FreebieStatus { get; set; }
    
        [ForeignKey("AddedByUser")]
        public int ApprovedBy { get; set; }
        public virtual User AddedByUser { get; set; }

        public string PhotoProofPath { get; set; }
 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Freebies> Freebies { get; set; }
}