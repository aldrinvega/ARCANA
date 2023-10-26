using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class FixedDiscounts : BaseEntity
{
    public int ClientId { get; set; }
    public decimal? DiscountPercentage { get; set; }

    //Navigational Properties
    public Clients Clients { get; set; }
}