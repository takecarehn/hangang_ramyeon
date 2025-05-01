using HangangRamyeon.Domain.Entities;

public class ProductDeletedEvent : BaseEvent
{
    public ProductDeletedEvent(Product product)
    {
        Product = product;
    }
    public Product Product { get; }
}