using HangangRamyeon.Domain.Entities;

public class ProductCompletedEvent : BaseEvent
{
    public ProductCompletedEvent(Product product)
    {
        Product = product;
    }
    public Product Product { get; }
}