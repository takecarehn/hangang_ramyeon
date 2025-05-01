using HangangRamyeon.Application.Products.Commands.CreateProduct;
using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Application.Products.Queries.GetProductDetail;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public string? Description { get; set; }
    public string? Supplier { get; set; }
    public decimal Weight { get; set; }
    public string Unit { get; set; } = default!;
    public DateTime ManufactureDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal ImportPrice { get; set; }
    public decimal SalePrice { get; set; }
    public Guid ShopId { get; set; }
    public string? ShopName { get; set; }
    public List<string> Images { get; set; } = new();
    public List<ProductAttributeDto> Attributes { get; set; } = new();

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Shop.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => i.ImageUrl)))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(src => src.Attributes.Select(a => new ProductAttributeDto
                {
                    Name = a.Name,
                    Value = a.Value
                })));
        }
    }
}

