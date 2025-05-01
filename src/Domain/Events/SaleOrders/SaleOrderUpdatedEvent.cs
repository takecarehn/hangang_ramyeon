using HangangRamyeon.Domain.Entities;

public class SaleOrderUpdatedEvent : BaseEvent
{
    public SaleOrderUpdatedEvent(SaleOrder saleOrder)
    {
        SaleOrder = saleOrder;
    }
    public SaleOrder SaleOrder { get; }
}
