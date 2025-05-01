using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Common.Security;
using HangangRamyeon.Application.Products.Commands.CreateProduct;
using HangangRamyeon.Application.Products.Commands.DeleteProduct;
using HangangRamyeon.Application.Products.Commands.UpdateProduct;
using HangangRamyeon.Application.Products.Queries.GetProductDetail;
using HangangRamyeon.Application.Products.Queries.GetProducts;
using HangangRamyeon.Domain.Constants;
using HangangRamyeon.Infrastructure.Authorization;

namespace HangangRamyeon.Web.Endpoints;

[Authorize]
public class Products : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/products").WithTags("Products");

        group.MapPost("/", CreateProduct)
             .RequirePermission(ClaimValues.PermissionProductCreate);

        group.MapPut("/{id}", UpdateProduct)
             .RequirePermission(ClaimValues.PermissionProductUpdate);

        group.MapDelete("/{id}", DeleteProduct)
             .RequirePermission(ClaimValues.PermissionProductDelete);

        group.MapGet("/", GetProducts)
             .RequirePermission(ClaimValues.PermissionProductView);

        group.MapGet("/{id}", GetProductDetail)
             .RequirePermission(ClaimValues.PermissionProductView);
    }

    public async Task<Result> CreateProduct(ISender sender, CreateProductCommand command)
        => await sender.Send(command);

    public async Task<Result> UpdateProduct(ISender sender, Guid id, UpdateProductCommand command)
    {
        if (id != command.Id) return Result.Failure("Id required!");
        return await sender.Send(command);
    }

    public async Task<Result> DeleteProduct(ISender sender, Guid id)
        => await sender.Send(new DeleteProductCommand(id));

    public async Task<Result> GetProducts(ISender sender, [AsParameters] GetProductsQuery query)
        => await sender.Send(query);

    public async Task<Result> GetProductDetail(ISender sender, Guid id)
        => await sender.Send(new GetProductDetailQuery(id));
}
