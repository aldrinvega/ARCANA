using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class RejectedClients : BaseEntity
{
    public int ClientId { get; set; }
    public string Reason { get; set; }
    public DateTime DateRejected { get; set; }
    public int RejectedBy { get; set; }
    public int Status { get; set; }
    public bool IsActive { get; set; }

    // Navigation property
    public Client Client { get; set; }
    public User RejectedByUser { get; set; }
    public Status RejectedStatus { get; set; }
}