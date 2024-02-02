using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class PriceMode : BaseEntity
    {
        public string PriceModeCode  { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
    }
}
