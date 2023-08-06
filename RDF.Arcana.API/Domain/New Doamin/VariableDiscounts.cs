using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.New_Doamin;

public class VariableDiscounts : BaseEntity
{
    public decimal MinimumAmount { get; set; }
    public decimal MaximumAmount { get; set; }
    public decimal MinimumPercentage { get; set; }
    public decimal MaximumPercentage { get; set; }
    public bool IsSubjectToApproval { get; set; }
}