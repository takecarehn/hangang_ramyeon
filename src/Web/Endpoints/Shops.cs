using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Common.Security;
using HangangRamyeon.Application.Shops.Commands.AssignUserToShop;
using HangangRamyeon.Application.Shops.Commands.CreateShop;
using HangangRamyeon.Application.Shops.Commands.DeleteShop;
using HangangRamyeon.Application.Shops.Commands.RemoveUserFromShop;
using HangangRamyeon.Application.Shops.Commands.UpdateShop;
using HangangRamyeon.Application.Shops.Queries.GetShopDetail;
using HangangRamyeon.Application.Shops.Queries.GetShops;
using HangangRamyeon.Domain.Constants;
using HangangRamyeon.Infrastructure.Authorization;

namespace HangangRamyeon.Web.Endpoints;

[Authorize]
public class Shops : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/shops").WithTags("Shops");

        group.MapPost("/", CreateShop)
             .RequirePermission(ClaimValues.PermissionShopCreate);

        group.MapPut("/{id}", UpdateShop)
             .RequirePermission(ClaimValues.PermissionShopUpdate);

        group.MapDelete("/{id}", DeleteShop)
             .RequirePermission(ClaimValues.PermissionShopDelete);

        group.MapGet("/", GetShops)
             .RequirePermission(ClaimValues.PermissionShopView);

        group.MapGet("/{id}", GetShopDetail)
             .RequirePermission(ClaimValues.PermissionShopView);

        group.MapPost("/{shopId}/users/{userId}", AssignUserToShop)
             .RequirePermission(ClaimValues.PermissionShopManage);

        group.MapDelete("/{shopId}/users/{userId}", RemoveUserFromShop)
             .RequirePermission(ClaimValues.PermissionShopManage);
    }

    public async Task<Result> CreateShop(ISender sender, CreateShopCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<Result> UpdateShop(ISender sender, Guid id, UpdateShopCommand command)
    {
        if (id != command.Id) return Result.Failure("Id required!");

        return await sender.Send(command);
    }

    public async Task<Result> DeleteShop(ISender sender, Guid id)
    {
        return await sender.Send(new DeleteShopCommand(id));
    }

    public async Task<Result> GetShops(ISender sender, [AsParameters] GetShopsQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<Result> GetShopDetail(ISender sender, Guid id)
    {
        return await sender.Send(new GetShopDetailQuery(id));
    }

    public async Task<Result> AssignUserToShop(ISender sender, Guid shopId, Guid userId)
    {
        var command = new AssignUserToShopCommand { ShopId = shopId, UserId = userId };
        return await sender.Send(command);
    }

    public async Task<Result> RemoveUserFromShop(ISender sender, Guid shopId, Guid userId)
    {
        var command = new RemoveUserFromShopCommand { ShopId = shopId, UserId = userId };
        return await sender.Send(command);
    }
}
