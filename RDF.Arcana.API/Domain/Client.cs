using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Client : BaseEntity
{
    public string OwnersName { get; set; }
    public string OwnersAddress { get; set; }
    public string BusinessName { get; set; }
    public string BusinessAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string BusinessType { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string EmailAddress { get; set; }
    public int AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool Freezer { get; set; }
    public string CustomerType { get; set; }
    public bool DirectDelivery { get; set; }
    public string PaymentMethod { get; set; }
    public int? TermDaysId { get; set; }
    public int? DiscountId { get; set; }
    
    
    //Navigational Properties
    public virtual User User { get; set; }
    public virtual User AddedByUser { get; set; }
    public virtual User ModifiedByUser { get; set; }
    public virtual Department Department { get; set; }
    public virtual Permit Permit { get; set; }
    public TermDays TermDays { get; set; }
    public Discount Discount { get; set; }
    public virtual BookingCoverages BookingCoverages { get; set; }
    
}