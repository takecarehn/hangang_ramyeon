using HangangRamyeon.Domain.Entities;

public class SaleOrderCompletedEvent : BaseEvent
{
    public SaleOrderCompletedEvent(SaleOrder saleOrder)
    {
        SaleOrder = saleOrder;
    }
    public SaleOrder SaleOrder { get; }
}