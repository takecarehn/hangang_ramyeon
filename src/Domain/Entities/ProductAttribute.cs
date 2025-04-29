namespace HangangRamyeon.Domain.Entities;
public class ProductAttribute
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Value { get; set; } = default!;
}

