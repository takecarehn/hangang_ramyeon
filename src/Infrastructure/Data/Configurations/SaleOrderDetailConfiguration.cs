using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangangRamyeon.Infrastructure.Data.Configuration;

public class SaleOrderDetailConfiguration : IEntityTypeConfiguration<SaleOrderDetail>
{
    public void Configure(EntityTypeBuilder<SaleOrderDetail> builder)
    {
        builder.HasKey(sod => sod.Id);

        builder.HasOne(sod => sod.SaleOrder)
            .WithMany(so => so.Details)
            .HasForeignKey(sod => sod.SaleOrderId);

        builder.HasOne(sod => sod.Product)
            .WithMany()
            .HasForeignKey(sod => sod.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}