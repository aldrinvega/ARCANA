using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class UpdateRequestTrail : BaseEntity
{
    public UpdateRequestTrail(
        int? requestId, 
        string moduleName,
        DateTime updatedAt, 
        int updatedBy)
    {
        RequestId = requestId;
        ModuleName = moduleName;
        UpdatedAt = updatedAt;
        UpdatedBy = updatedBy;
    }
    public int? RequestId { get; set; }
    public string ModuleName { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
    
    public virtual Request Request { get; set; }
}