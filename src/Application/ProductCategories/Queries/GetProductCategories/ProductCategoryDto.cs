using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Application.ProductCategories.Queries.GetProductCategories;

public class ProductCategoryDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
        }
    }
}
