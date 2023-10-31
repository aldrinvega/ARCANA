using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class UserPasswordHistory : BaseEntity
{
    public int UserId { get; set; }
    public string HashedPassword { get; set; }
    public DateTime SetDate { get; set; }
}