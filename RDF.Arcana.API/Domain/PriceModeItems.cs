using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain
{
    public class PriceModeItems : BaseEntity
    {
        public int ItemId { get; set; }
        public int PriceModeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public int AddedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;

        public virtual PriceMode PriceMode { get; set; }
        public virtual Items Item { get; set; }
        public virtual ICollection<ItemPriceChange> ItemPriceChanges { get; set; }
        public virtual User AddedByUser { get; set; }
        public virtual User ModifiedByUser { get; set; }
    }
}
