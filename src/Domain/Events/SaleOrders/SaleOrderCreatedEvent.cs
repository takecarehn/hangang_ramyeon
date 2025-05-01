using HangangRamyeon.Domain.Entities;

public class SaleOrderCreatedEvent : BaseEvent
{
    public SaleOrderCreatedEvent(SaleOrder saleOrder)
    {
        SaleOrder = saleOrder;
    }
    public SaleOrder SaleOrder { get; }
}