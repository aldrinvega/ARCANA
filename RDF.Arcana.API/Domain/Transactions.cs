using System.Diagnostics;
using MySqlX.XDevAPI;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class Transactions : BaseEntity
{
    public int ClientId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
    public int AddedBy { get; set; }
    public bool IsActive { get; set; } = true;
    public string Status { get; set; }
    public string InvoiceType { get; set; }
    public string InvoiceNo { get; set; }
    public string InvoiceAttach { get; set; }
    public DateTime InvoiceAttachDateReceived { get; set; }

    public virtual Clients Client { get; set; }
    public virtual User AddedByUser { get; set; }
    public virtual TransactionSales TransactionSales { get; set; }
    public virtual ICollection<TransactionItems> TransactionItems { get; set; }
    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; }
}