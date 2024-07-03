using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class OnlinePayments : BaseEntity
    {
        public string OnlinePlatform { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
