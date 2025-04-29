using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HangangRamyeon.Infrastructure.Data.Configuration;

public class ImportOrderConfiguration : IEntityTypeConfiguration<ImportOrder>
{
    public void Configure(EntityTypeBuilder<ImportOrder> builder)
    {
        builder.HasKey(io => io.Id);

        builder.HasOne(io => io.Shop)
            .WithMany()
            .HasForeignKey(io => io.ShopId);
    }
}