using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events.Shops;
public class ShopUpdatedEvent : BaseEvent
{
    public ShopUpdatedEvent(Shop shop)
    {
        Shop = shop;
    }

    public Shop Shop { get; }
}
