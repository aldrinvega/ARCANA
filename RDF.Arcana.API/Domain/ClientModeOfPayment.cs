using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ClientModeOfPayment : BaseEntity
{
    public int ClientId { get; set; }
    public int ModeOfPaymentId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    
    public virtual ModeOfPayment ModeOfPayment { get; set; }
    public virtual Clients Client { get; set; }
}