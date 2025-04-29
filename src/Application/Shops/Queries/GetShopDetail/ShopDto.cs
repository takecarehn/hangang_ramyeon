using HangangRamyeon.Domain.Entities;

namespace HangangRamyeon.Application.Shops.Queries.GetShopDetail;
public class ShopDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? TaxCode { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Shop, ShopDto>();
        }
    }
}
