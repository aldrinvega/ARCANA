﻿using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class ClearedPayments : BaseEntity
    {
        public int PaymentRecordId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public string Status { get; set; }
        public string Reason { get; set; }
        public virtual User AddedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
        public virtual PaymentRecords PaymentRecord { get; set; }
    }
}
