namespace HangangRamyeon.Domain.Entities;
public class Inventory
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public int QuantityInStock { get; set; }
    public DateTime LastUpdated { get; set; }
}
