using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.New_Doamin;

public class FreebieItems : BaseEntity
{
    [ForeignKey("FreebieRequest")]
    public int RequestId { get; set; }
    public FreebieRequest FreebieRequest { get; set; }
    [ForeignKey("Items")]
    public int ItemId { get; set; }
    public Items Items { get; set; }
    public int Quantity { get; set; }
}