using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class OwnersAddress : BaseEntity
{
    public string HouseNumber { get; set; }
    public string StreetName { get; set; }
    public string Barangay { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
}