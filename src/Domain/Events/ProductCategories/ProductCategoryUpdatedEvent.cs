using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ProductCategoryUpdatedEvent : BaseEvent
{
    public ProductCategory ProductCategory { get; }

    public ProductCategoryUpdatedEvent(ProductCategory productCategory)
    {
        ProductCategory = productCategory;
    }
}
