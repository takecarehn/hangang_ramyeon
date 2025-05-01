using HangangRamyeon.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HangangRamyeon.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Log> Logs { get; }
    DbSet<Shop> Shops { get; }
    DbSet<UserShop> UserShops { get; }
    DbSet<ProductCategory> ProductCategories { get; }
    DbSet<Product> Products { get; }
    DbSet<ProductImage> ProductImages { get; }
    DbSet<ProductAttribute> ProductAttributes { get; }
    DbSet<Inventory> Inventories { get; }
    DbSet<ImportOrder> ImportOrders { get; }
    DbSet<ImportOrderDetail> ImportOrderDetails { get; }
    DbSet<SaleOrder> SaleOrders { get; }
    DbSet<SaleOrderDetail> SaleOrderDetails { get; }

    DatabaseFacade Database { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
