using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ItemPriceChange : BaseEntity
{
    public int PriceModeItemId { get; set; }
    public decimal Price { get; set; }
    public DateTime EffectivityDate { get; set; }
    public int AddedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual PriceModeItems PriceModeItem { get; set; }
}