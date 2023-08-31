using MySqlX.XDevAPI;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Address : BaseEntity
{
    public int ClientId { get; set; }
    public string AddressName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual User User { get; set; }
    public Client Client { get; set; }
}