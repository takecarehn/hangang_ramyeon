using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangangRamyeon.Infrastructure.Data.Configuration;

public class ImportOrderDetailConfiguration : IEntityTypeConfiguration<ImportOrderDetail>
{
    public void Configure(EntityTypeBuilder<ImportOrderDetail> builder)
    {
        builder.HasKey(iod => iod.Id);

        builder.Property(iod => iod.Quantity)
            .IsRequired();

        builder.Property(iod => iod.ImportPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(iod => iod.ImportOrder)
            .WithMany(io => io.Details)
            .HasForeignKey(iod => iod.ImportOrderId);

        builder.HasOne(iod => iod.Product)
            .WithMany()
            .HasForeignKey(iod => iod.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
