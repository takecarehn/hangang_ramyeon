namespace HangangRamyeon.Domain.Entities;
public class UserShop
{
    public Guid UserId { get; set; }
    public Guid ShopId { get; set; }
    public Shop Shop { get; set; } = default!;
}

