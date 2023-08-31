using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Clients : BaseEntity
{
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string BusinessName { get; set; }
    public string RepresentativeName { get; set; }
    public string RepresentativePosition { get; set; }
    public string BusinessAddress { get; set; }
    public int Cluster { get; set; }
    public bool Freezer { get; set; }
    public string CustomerType { get; set; }
    public int? TermDays { get; set; }
    public int? DiscountId { get; set; }
    public string ClientType { get; set; }
    [ForeignKey("StoreType")]
    public int? StoreTypeId { get; set; }
    public string RegistrationStatus { get; set; }
    public int? Terms { get; set; }
    public int? ModeOfPayment { get; set; }
    public bool? DirectDelivery { get; set; }
    public DiscountTypes DiscountType { get; set; }
    public int? BookingCoverageId { get; set; }
    public int AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public virtual User ModifiedByUser { get; set; }
    public virtual User RequestedByUser { get; set; }
    public virtual List<ClientDocuments> ClientDocuments { get; set; }
    public virtual List<Approvals> Approvals { get; set; }
    public virtual List<FreebieRequest> FreebiesRequests { get; set; }
    public virtual FixedDiscounts FixedDiscounts { get; set; }
    public virtual VariableDiscounts VariableDiscounts { get; set; }
    public virtual StoreType StoreType { get; set; }
    public virtual BookingCoverages BookingCoverages { get; set; }
    public virtual ModeOfPayment ModeOfPayments { get; set; }
    public virtual Terms Term { get; set; }




}