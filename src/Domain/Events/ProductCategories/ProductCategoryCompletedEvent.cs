using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ProductCategoryCompletedEvent : BaseEvent
{
    public ProductCategory ProductCategory { get; }

    public ProductCategoryCompletedEvent(ProductCategory productCategory)
    {
        ProductCategory = productCategory;
    }
}
