using HangangRamyeon.Domain.Entities;

public class SaleOrderDeletedEvent : BaseEvent
{
    public SaleOrderDeletedEvent(SaleOrder saleOrder)
    {
        SaleOrder = saleOrder;
    }
    public SaleOrder SaleOrder { get; }
}