namespace HangangRamyeon.Domain.Events.Shops;
public class UserRemovedFromShopEvent : BaseEvent
{
    public UserRemovedFromShopEvent(Guid userId, Guid shopId)
    {
        UserId = userId;
        ShopId = shopId;
    }

    public Guid UserId { get; }
    public Guid ShopId { get; }
}
