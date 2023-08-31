using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class FixedDiscounts : BaseEntity
{
    public decimal DiscountPercentage { get; set; }
}