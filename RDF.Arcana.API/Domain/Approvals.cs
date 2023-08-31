using RDF.Arcana.API.Common;
using RDF.Arcana.API.Domain;

namespace RDF.Arcana.API.Domain;

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