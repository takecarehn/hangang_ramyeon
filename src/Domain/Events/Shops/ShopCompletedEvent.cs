using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events.Shops;
public class ShopCompletedEvent : BaseEvent
{
    public ShopCompletedEvent(Shop shop)
    {
        Shop = shop;
    }

    public Shop Shop { get; }
}
