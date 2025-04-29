namespace HangangRamyeon.Domain.Entities;
public class Shop : BaseAuditableEntity
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? TaxCode { get; set; }

    public ICollection<UserShop> UserShops { get; set; } = new List<UserShop>();
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
