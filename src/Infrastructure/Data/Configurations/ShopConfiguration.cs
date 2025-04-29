using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangangRamyeon.Infrastructure.Data.Configuration;

public class ShopConfiguration : IEntityTypeConfiguration<Shop>
{
    public void Configure(EntityTypeBuilder<Shop> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Address)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.HasMany(s => s.UserShops)
            .WithOne(us => us.Shop)
            .HasForeignKey(us => us.ShopId);

        builder.HasMany(s => s.Products)
            .WithOne(p => p.Shop)
            .HasForeignKey(p => p.ShopId);
    }
}
