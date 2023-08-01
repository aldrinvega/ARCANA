using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class RequestedClient : BaseEntity
{
    public int ClientId { get; set; }
    public string Reason { get; set; }
    public DateTime DateRequest { get; set; }
    public int Status { get; set; }
    public bool IsActive { get; set; }
    public int RequestedBy { get; set; }

    public Client Client { get; set; }
    public virtual User RequestedByUser { get; set; }
    public virtual Status RequestStatus { get; set; }
}