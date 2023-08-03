using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Freebies : BaseEntity
 {
     public int ItemId { get; set; }

     [ForeignKey("ItemId")]
     public Items Item { get; set; }

     public int Quantity { get; set; }
    
     public int FreebieRequestId { get; set; }

     [ForeignKey("FreebieRequestId")]
     public FreebieRequest FreebieRequest { get; set; }
 }