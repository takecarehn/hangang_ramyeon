using HangangRamyeon.Domain.Entities;

public class ProductUpdatedEvent : BaseEvent
{
    public ProductUpdatedEvent(Product product)
    {
        Product = product;
    }
    public Product Product { get; }
}