using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Domain.Events;

public class ProductCategoryDeletedEvent : BaseEvent
{
    public ProductCategory ProductCategory { get; }

    public ProductCategoryDeletedEvent(ProductCategory productCategory)
    {
        ProductCategory = productCategory;
    }
}
