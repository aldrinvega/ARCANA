using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class OnlinePayment : BaseEntity
    {
        public int ClientId { get; set; }
        public string OnlinePaymentName { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public decimal PaymentAmount { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string Status { get; set; }
        public string Remarks { get; set; }

        public virtual Clients Client { get; set; }
        public virtual User AddedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
        public virtual PaymentRecords PaymentRecord { get; set; }

    }
}
