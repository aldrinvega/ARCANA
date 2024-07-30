﻿using System.ComponentModel.DataAnnotations.Schema;
using RDF.Arcana.API.Common;

namespace RDF.Arcana.API.Domain;

public class TransactionSales : BaseEntity
{
    public int TransactionId { get; set; }
    public decimal VatableSales { get; set; }
    public decimal VatExemptSales { get; set; }
    public decimal ZeroRatedSales { get; set; }
    public decimal VatAmount { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalSales { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal AmountDue { get; set; }
    public decimal AddVat { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal TotalAmountDue { get; set; }
    public decimal Discount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal SpecialDiscount { get; set; }
    public decimal SpecialDiscountAmount { get; set; }
    public decimal SubTotal { get; set; }
    public int AddedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt{ get; set; }
    public bool IsActive { get; set; } = true;
    public string Remarks { get; set; }
    public decimal RemainingBalance { get; set; }


    public virtual User AddedByUser { get; set; }
    public virtual Transactions Transaction { get; set; }
}










