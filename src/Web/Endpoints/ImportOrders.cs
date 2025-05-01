// src/Web/Endpoints/ImportOrders.cs
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Application.Common.Security;
using HangangRamyeon.Application.ImportOrders.Queries.GetImportOrders;
using HangangRamyeon.Domain.Constants;
using HangangRamyeon.Infrastructure.Authorization;

[Authorize]
public class ImportOrders : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup("/api/import-orders").WithTags("ImportOrders");

        group.MapPost("/", CreateImportOrder)
             .RequirePermission(ClaimValues.PermissionImportOrderCreate);

        group.MapPut("/{id}", UpdateImportOrder)
             .RequirePermission(ClaimValues.PermissionImportOrderUpdate);

        group.MapDelete("/{id}", DeleteImportOrder)
             .RequirePermission(ClaimValues.PermissionImportOrderDelete);

        group.MapGet("/", GetImportOrders)
             .RequirePermission(ClaimValues.PermissionImportOrderView);

        group.MapGet("/{id}", GetImportOrderDetail)
             .RequirePermission(ClaimValues.PermissionImportOrderView);
    }

    public async Task<Result> CreateImportOrder(ISender sender, CreateImportOrderCommand command)
        => await sender.Send(command);

    public async Task<Result> UpdateImportOrder(ISender sender, Guid id, UpdateImportOrderCommand command)
    {
        if (id != command.Id) return Result.Failure("Id mismatch.");
        return await sender.Send(command);
    }

    public async Task<Result> DeleteImportOrder(ISender sender, Guid id)
        => await sender.Send(new DeleteImportOrderCommand(id));

    public async Task<Result> GetImportOrders(ISender sender, [AsParameters] GetImportOrdersQuery query)
        => await sender.Send(query);

    public async Task<Result> GetImportOrderDetail(ISender sender, Guid id)
        => await sender.Send(new GetImportOrderDetailQuery(id));
}
