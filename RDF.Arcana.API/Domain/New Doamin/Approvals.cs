using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.New_Doamin;

public class Approvals : BaseEntity
{
    public int ClientId { get; set; }
    public Clients Client { get; set; }

    public string ApprovalType { get; set; }
    public string Reason { get; set; }
    public bool IsApproved { get; set; }
    public bool IsActive { get; set; }
    
    public virtual FreebieRequest FreebieRequest { get; set; }
}