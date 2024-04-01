using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class AdvancePayment : BaseEntity
{
    public int ClientId { get; set; }
    public string PaymentMethod { get; set; }
    public decimal AdvancePaymentAmount { get; set; }
    public decimal RemainingBalance { get; set; }
    public string Payee { get; set; }
    public DateTime ChequeDate { get; set; }
    public string BankName { get; set; }
    public string ChequeNo { get; set; }
    public DateTime DateReceived { get; set; }
    public decimal ChequeAmount { get; set; }
    public string AccountName { get; set; }
    public string AccountNo { get; set; }
    public int AddedBy { get; set; }
    public int? ModifiedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public string Status { get; set; }
    public string Reason { get; set; }
    public string Origin { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public virtual User AddedByUser { get; set; }
    public virtual User ModifiedByUser { get; set; }
    public virtual Clients Client { get; set; }
}
