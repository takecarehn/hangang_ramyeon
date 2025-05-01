using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ProductCategoryCreatedEvent : BaseEvent
{
    public ProductCategory ProductCategory { get; }

    public ProductCategoryCreatedEvent(ProductCategory productCategory)
    {
        ProductCategory = productCategory;
    }
}
