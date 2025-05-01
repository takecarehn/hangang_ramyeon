using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

public record DeleteSaleOrderCommand(Guid Id) : IRequest<Result>;

public class DeleteSaleOrderCommandHandler : IRequestHandler<DeleteSaleOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteSaleOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteSaleOrderCommand request, CancellationToken cancellationToken)
    {
        var saleOrder = await _context.SaleOrders
            .Include(x => x.Details)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (saleOrder == null)
            return Result.Failure("SaleOrder not found.");

        // Hoàn trả tồn kho
        foreach (var detail in saleOrder.Details)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == detail.ProductId, cancellationToken);
            if (inventory != null)
                inventory.QuantityInStock += detail.Quantity;
        }

        _context.SaleOrders.Remove(saleOrder);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
