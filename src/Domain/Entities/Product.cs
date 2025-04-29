namespace HangangRamyeon.Domain.Entities;
public class Product : BaseAuditableEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public ProductCategory Category { get; set; } = default!;
    public string? Description { get; set; }
    public string? Supplier { get; set; }
    public decimal Weight { get; set; }
    public string Unit { get; set; } = default!;
    public DateTime ManufactureDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal ImportPrice { get; set; }
    public decimal SalePrice { get; set; }
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; } = default!;

    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();
}

