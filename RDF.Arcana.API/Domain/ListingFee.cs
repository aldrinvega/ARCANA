using RDF.Arcana.API.Common;
using System;
using System.Collections;

namespace RDF.Arcana.API.Domain;

public class ListingFee : BaseEntity
{
    public int ClientId { get; set; }
    public DateOnly CratedAt { get; set; } = DateOnly.FromDateTime(DateTime.Today);
    public bool IsActive { get; set; }
    public bool IsDelivered { get; set; }
    public string Status { get; set; }
    public int RequestedBy { get; set; }
    public int ApprovedBy { get; set; }
    public decimal Total { get; set; }

    public virtual Clients Client { get; set; }
    public virtual User RequestedByUser { get; set; }
    public virtual User ApprovedByUser { get; set; }

    public virtual ICollection<ListingFeeItems> ListingFeeItems { get; set; }
}