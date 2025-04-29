using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events.Shops;
public class ShopCreatedEvent : BaseEvent
{
    public ShopCreatedEvent(Shop shop)
    {
        Shop = shop;
    }

    public Shop Shop { get; }
}
