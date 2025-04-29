using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events.Shops;
public class ShopDeletedEvent : BaseEvent
{
    public ShopDeletedEvent(Shop shop)
    {
        Shop = shop;
    }

    public Shop Shop { get; }
}
