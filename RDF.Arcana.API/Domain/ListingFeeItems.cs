using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ListingFeeItems : BaseEntity
{
    public int ListingFeeId { get; set; }
    public int ItemId { get; set; }
    public int Sku { get; set; }
    public decimal UnitCost { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual ListingFee ListingFee { get; set; }
}