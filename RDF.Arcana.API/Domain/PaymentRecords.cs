using RDF.Arcana.API.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace RDF.Arcana.API.Domain
{
    public class PaymentRecords : BaseEntity
    {       
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string Status { get; set; }
        public string Reason { get; set; }       
        public virtual ClearedPayments ClearedPayments { get; set; }
        public virtual User AddedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; }
    }
}
