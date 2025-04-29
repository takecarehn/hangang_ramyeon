namespace HangangRamyeon.Domain.Entities;
public class ImportOrderDetail : BaseAuditableEntity
{
    public Guid ImportOrderId { get; set; }
    public ImportOrder ImportOrder { get; set; } = default!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal ImportPrice { get; set; }
}

