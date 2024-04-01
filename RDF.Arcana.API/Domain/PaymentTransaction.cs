using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class PaymentTransaction : BaseEntity
{
    public int TransactionId { get; set; }
    public string PaymentMethod { get; set; }
    public decimal PaymentAmount { get; set; }
    public decimal TotalAmountReceived { get; set; }
    public string Payee { get; set; }
    public DateTime ChequeDate { get; set; }
    public string BankName { get; set; }
    public string ChequeNo { get; set; }
    public DateTime DateReceived { get; set; }
    public decimal ChequeAmount { get; set; }
    public string AccountName { get; set; }
    public string AccountNo { get; set; }
    public int AddedBy { get; set; }
    public bool IsActive { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    
    public virtual Transactions Transactions { get; set; }
    public virtual User AddedByUser { get; set; }
}