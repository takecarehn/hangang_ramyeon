using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangangRamyeon.Infrastructure.Data.Configurations;
public class UserShopConfiguration : IEntityTypeConfiguration<UserShop>
{
    public void Configure(EntityTypeBuilder<UserShop> builder)
    {
        builder.HasKey(us => new { us.UserId, us.ShopId });
    }
}
