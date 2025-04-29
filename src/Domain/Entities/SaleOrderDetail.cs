namespace HangangRamyeon.Domain.Entities;
public class SaleOrderDetail : BaseAuditableEntity
{
    public Guid SaleOrderId { get; set; }
    public SaleOrder SaleOrder { get; set; } = default!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal SalePrice { get; set; }
}

