using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class BookingCoverages : BaseEntity
{
    public string BookingCoverage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    [ForeignKey("AddedByUser")]
    public int AddedBy { get; set; }
    
    public virtual User AddedByUser { get; set; }
}