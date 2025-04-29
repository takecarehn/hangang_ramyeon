namespace HangangRamyeon.Domain.Entities;
public class SaleOrder : BaseAuditableEntity
{
    public string InvoiceCode { get; set; } = default!;
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; } = default!;
    public Guid CreatedByUserId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal CustomerPaid { get; set; }
    public decimal ChangeAmount { get; set; }
    public string? Note { get; set; }

    public ICollection<SaleOrderDetail> Details { get; set; } = new List<SaleOrderDetail>();
}

