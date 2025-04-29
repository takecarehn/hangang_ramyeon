using System.Reflection;
using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Domain.Entities;
using HangangRamyeon.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HangangRamyeon.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public DbSet<Log> Logs => Set<Log>();
    public DbSet<Shop> Shops => Set<Shop>();
    public DbSet<UserShop> UserShops => Set<UserShop>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductAttribute> ProductAttributes => Set<ProductAttribute>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<ImportOrder> ImportOrders => Set<ImportOrder>();
    public DbSet<ImportOrderDetail> ImportOrderDetails => Set<ImportOrderDetail>();
    public DbSet<SaleOrder> SaleOrders => Set<SaleOrder>();
    public DbSet<SaleOrderDetail> SaleOrderDetails => Set<SaleOrderDetail>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<UserShop>(entity =>
        {
            entity.HasKey(us => new { us.UserId, us.ShopId });
        });
    }
}
