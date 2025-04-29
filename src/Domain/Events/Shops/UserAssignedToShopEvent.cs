namespace HangangRamyeon.Domain.Events.Shops;
public class UserAssignedToShopEvent : BaseEvent
{
    public UserAssignedToShopEvent(Guid userId, Guid shopId)
    {
        UserId = userId;
        ShopId = shopId;
    }

    public Guid UserId { get; }
    public Guid ShopId { get; }
}
