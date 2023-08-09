using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class StoreType : BaseEntity
{
    public string StoreTypeName { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; }
    [ForeignKey("AddedBy")]
    public int? AddedBy { get; set; }
    [ForeignKey("ModifiedByUser")]
    public int? ModifiedBy { get; set; }

    public bool IsActive { get; set; }
    
    public virtual User AddedByUser { get; set; }
    public virtual User ModifiedByUser { get; set; }
}