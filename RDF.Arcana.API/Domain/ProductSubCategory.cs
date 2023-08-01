using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class ProductSubCategory : BaseEntity
{
    public string ProductSubCategoryName { get; set; }
    public int ProductCategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    [ForeignKey("AddedByUser")]
    public int AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public virtual User AddedByUser { get; set; }
}