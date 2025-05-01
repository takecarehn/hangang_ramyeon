using HangangRamyeon.Application.Categories.Commands.DeleteProductCategory;
using HangangRamyeon.Application.Categories.Commands.UpdateProductCategory;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Common.Security;
using HangangRamyeon.Application.ProductCategories.Commands.CreateProductCategory;
using HangangRamyeon.Application.ProductCategories.Queries.GetProductCategories;
using HangangRamyeon.Domain.Constants;
using HangangRamyeon.Infrastructure.Authorization;
namespace HangangRamyeon.Web.Endpoints;

[Authorize]
public class ProductCategories : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/product-categories").WithTags("ProductCategories");

        group.MapPost("/", CreateProductCategory)
             .RequirePermission(ClaimValues.PermissionProductCategoryCreate);

        group.MapPut("/{id}", UpdateProductCategory)
             .RequirePermission(ClaimValues.PermissionProductCategoryUpdate);

        group.MapDelete("/{id}", DeleteProductCategory)
             .RequirePermission(ClaimValues.PermissionProductCategoryDelete);

        group.MapGet("/", GetProductCategories)
             .RequirePermission(ClaimValues.PermissionProductCategoryView);
    }

    public async Task<Result> CreateProductCategory(ISender sender, CreateProductCategoryCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> UpdateProductCategory(ISender sender, Guid id, UpdateProductCategoryCommand command)
    {
        if (id != command.Id) return Result.Failure("Id required!");
        return await sender.Send(command);
    }

    public async Task<Result> DeleteProductCategory(ISender sender, Guid id)
    {
        return await sender.Send(new DeleteProductCategoryCommand(id));
    }

    public async Task<Result> GetProductCategories(ISender sender, [AsParameters] GetProductCategoriesQuery query)
    {
        return await sender.Send(query);
    }
}
