namespace HangangRamyeon.Domain.Entities;
public class ProductCategory : BaseAuditableEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    public ICollection<Product> Products { get; set; } = new List<Product>();
}

