using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Discount : BaseEntity
{
    public decimal LowerBound { get; set; }
    public decimal UpperBound { get; set; }
    public decimal CommissionRateLower { get; set; }
    public decimal CommissionRateUpper { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    [ForeignKey("AddedByUser")]
    public int AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public virtual User AddedByUser { get; set; }
}