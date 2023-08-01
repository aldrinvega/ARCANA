using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Permit : BaseEntity
{
    public int ClientId { get; set; }
    public string BusinessPermit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public virtual User User { get; set; }
    public Client Client { get; set; }
}