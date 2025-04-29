using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangangRamyeon.Infrastructure.Data.Configuration;

public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
{
    public void Configure(EntityTypeBuilder<SaleOrder> builder)
    {
        builder.HasKey(so => so.Id);

        builder.HasOne(so => so.Shop)
            .WithMany()
            .HasForeignKey(so => so.ShopId);
    }
}