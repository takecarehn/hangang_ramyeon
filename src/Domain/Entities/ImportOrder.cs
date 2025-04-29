namespace HangangRamyeon.Domain.Entities;
public class ImportOrder : BaseAuditableEntity
{
    public string InvoiceCode { get; set; } = default!;
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; } = default!;
    public Guid CreatedByUserId { get; set; }
    public DateTime ImportDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public string? Note { get; set; }

    public ICollection<ImportOrderDetail> Details { get; set; } = new List<ImportOrderDetail>();
}

