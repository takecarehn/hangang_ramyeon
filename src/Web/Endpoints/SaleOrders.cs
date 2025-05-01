using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Common.Security;
using HangangRamyeon.Domain.Constants;
using HangangRamyeon.Infrastructure.Authorization;

[Authorize]
public class SaleOrders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/saleorders").WithTags("SaleOrders");

        group.MapPost("/", CreateSaleOrder)
             .RequirePermission(ClaimValues.PermissionSaleOrderCreate);

        group.MapPut("/{id}", UpdateSaleOrder)
             .RequirePermission(ClaimValues.PermissionSaleOrderUpdate);

        group.MapDelete("/{id}", DeleteSaleOrder)
             .RequirePermission(ClaimValues.PermissionSaleOrderDelete);

        group.MapGet("/", GetSaleOrders)
             .RequirePermission(ClaimValues.PermissionSaleOrderView);

        group.MapGet("/{id}", GetSaleOrderDetail)
             .RequirePermission(ClaimValues.PermissionSaleOrderView);
    }

    public async Task<Result> CreateSaleOrder(ISender sender, CreateSaleOrderCommand command)
        => await sender.Send(command);

    public async Task<Result> UpdateSaleOrder(ISender sender, Guid id, UpdateSaleOrderCommand command)
    {
        if (id != command.Id) return Result.Failure("Id required!");
        return await sender.Send(command);
    }

    public async Task<Result> DeleteSaleOrder(ISender sender, Guid id)
        => await sender.Send(new DeleteSaleOrderCommand(id));

    public async Task<Result> GetSaleOrders(ISender sender, [AsParameters] GetSaleOrdersQuery query)
        => await sender.Send(query);

    public async Task<Result> GetSaleOrderDetail(ISender sender, Guid id)
        => await sender.Send(new GetSaleOrderDetailQuery(id));
}
