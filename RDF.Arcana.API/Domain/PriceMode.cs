using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class PriceMode : BaseEntity
    {
        public string PriceModeCode  { get; set; }
        public string PriceModeDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
        public virtual ICollection<Clients> Clients { get; set; }
    }
}
