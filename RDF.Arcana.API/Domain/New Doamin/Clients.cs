using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain.New_Doamin;

public class Clients : BaseEntity
{
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string BusinessName { get; set; }
    public string RepresentativeName { get; set; }
    public string RepresentativePosition { get; set; }
    public string BusinessAddress { get; set; }
    public bool Freezer { get; set; }
    public string CustomerType { get; set; }
    public int? TermDays { get; set; }
    public int? DiscountId { get; set; }
    public string ClientType { get; set; }
    public string StoreType { get; set; }
    public string RegistrationStatus { get; set; }
    public int? FixedDiscountId { get; set; }
    public int? VariableDiscountId { get; set; }
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
    
}