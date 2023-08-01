using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Items : BaseEntity
{
    public string ItemCode { get; set; }
    public string ItemDescription { get; set; }
    public int UomId { get; set; }
    public int ProductSubCategoryId { get; set; }
    public int MeatTypeId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    [ForeignKey("AddedByUser")]
    public int AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public ProductSubCategory ProductSubCategory { get; set; }
    public Uom Uom { get; set; }
    public MeatType MeatType { get; set; }
    public virtual User AddedByUser { get; set; }
}