using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Uom : BaseEntity
{
    public string UomCode { get; set; }
    public string UomDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    [ForeignKey("AddedByUser")]
    public int AddedBy { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<Items> Items { get; set; }
    public virtual User AddedByUser { get; set; }
}