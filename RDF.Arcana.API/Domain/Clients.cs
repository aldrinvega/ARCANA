using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Clients : BaseEntity
{
    private DateTime dateOfBirth;
    public string Fullname { get; set; }
    public int OwnersAddressId { get; set; }
    public string PhoneNumber { get; set; }

    [NotMapped]
    public DateOnly DateOfBirthDB
    {
        get => DateOnly.FromDateTime(dateOfBirth);
        set => dateOfBirth = new DateTime(value.Year, value.Month, value.Day);
    }

    public DateTime DateOfBirth
    {
        get => dateOfBirth;
        private set => dateOfBirth = value;
    }

    public string EmailAddress { get; set; }
    public string BusinessName { get; set; }
    public string TinNumber { get; set; }
    public string RepresentativeName { get; set; }
    public string RepresentativePosition { get; set; }
    public int? BusinessAddressId { get; set; }
    public int Cluster { get; set; }
    public bool Freezer { get; set; }
    public string CustomerType { get; set; }
    public string Origin { get; set; }
    public int? TermDays { get; set; }
    public int? DiscountId { get; set; }
    public string ClientType { get; set; }

    [ForeignKey("StoreType")] public int? StoreTypeId { get; set; }

    public string RegistrationStatus { get; set; }
    public int? Terms { get; set; }
    public int? ModeOfPayment { get; set; }
    public bool? DirectDelivery { get; set; }
    public int? BookingCoverageId { get; set; }
    public int AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    //Pin Location

    public string Longitude { get; set; }
    public string Latitude { get; set; }

    //Discounts
    public int? FixedDiscountId { get; set; }
    public bool? VariableDiscount { get; set; }

    public virtual User ModifiedByUser { get; set; }
    public virtual User RequestedByUser { get; set; }
    public virtual List<ClientDocuments> ClientDocuments { get; set; }
    public virtual List<Approvals> Approvals { get; set; }
    public virtual List<ListingFee> ListingFees { get; set; }
    public virtual List<FreebieRequest> FreebiesRequests { get; set; }
    public virtual FixedDiscounts FixedDiscounts { get; set; }
    public virtual VariableDiscounts VariableDiscounts { get; set; }
    public virtual StoreType StoreType { get; set; }
    public virtual BookingCoverages BookingCoverages { get; set; }
    public virtual ModeOfPayment ModeOfPayments { get; set; }
    public virtual TermOptions Term { get; set; }
    public virtual OwnersAddress OwnersAddress { get; set; }
    public virtual BusinessAddress BusinessAddress { get; set; }
}