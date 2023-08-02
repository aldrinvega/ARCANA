using RDF.Arcana.API.Common;
using RDF.Arcana.API.Features.Clients.Prospecting.Request;

namespace RDF.Arcana.API.Domain;

public class Freebies : BaseEntity
 {
     public int ClientId { get; set; }
     public virtual ApprovedClient Client { get; set; }
     
     public int ItemId { get; set; }
     public virtual Items Items { get; set; }
     
     public int StatusId { get; set; }
     public virtual Status FreebieStatus { get; set; }
 
     public int AddedBy { get; set; }
     public virtual User AddedByUser { get; set; }

     public int Quantity { get; set; }

     public DateTime CreatedAt { get; set; }
     public DateTime UpdatedAt { get; set; }
     public bool IsActive { get; set; }
 }