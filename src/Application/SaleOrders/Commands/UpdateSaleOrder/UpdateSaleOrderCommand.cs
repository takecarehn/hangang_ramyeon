using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;

public record UpdateSaleOrderCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public Guid ShopId { get; init; }
    public DateTime SaleDate { get; init; }
    public decimal Discount { get; init; }
    public decimal CustomerPaid { get; init; }
    public string? Note { get; init; }
    public List<SaleOrderDetailDto> Details { get; init; } = new();
    // Các trường bổ sung nếu cần
    public string? ModifiedBy { get; init; }
    public DateTime? Modified { get; init; }
}

public class UpdateSaleOrderCommandHandler : IRequestHandler<UpdateSaleOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public UpdateSaleOrderCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(UpdateSaleOrderCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var userId = _currentUser.Id;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure("Unauthorized: User ID is not available.");
            }

            var saleOrder = await _context.SaleOrders
                .Include(x => x.Details)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (saleOrder == null)
                return Result.Failure("SaleOrder not found.");

            // Hoàn trả tồn kho cũ
            foreach (var oldDetail in saleOrder.Details)
            {
                var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == oldDetail.ProductId, cancellationToken);
                if (inventory != null)
                    inventory.QuantityInStock += oldDetail.Quantity;
            }

            saleOrder.ShopId = request.ShopId;
            saleOrder.SaleDate = request.SaleDate;
            saleOrder.Discount = request.Discount;
            saleOrder.CustomerPaid = request.CustomerPaid;
            saleOrder.Note = request.Note;
            saleOrder.LastModifiedBy = userId;
            saleOrder.LastModified = request.Modified ?? DateTime.UtcNow;
            saleOrder.TotalAmount = request.Details.Sum(d => d.Quantity * d.SalePrice);
            saleOrder.ChangeAmount = request.CustomerPaid - (request.Details.Sum(d => d.Quantity * d.SalePrice) - request.Discount);

            saleOrder.Details.Clear();

            foreach (var d in request.Details)
            {
                var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == d.ProductId, cancellationToken);
                if (inventory == null || inventory.QuantityInStock < d.Quantity)
                    return Result.Failure($"Not enough inventory for product {d.ProductId}");

                inventory.QuantityInStock -= d.Quantity;

                saleOrder.Details.Add(new SaleOrderDetail
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    SalePrice = d.SalePrice
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure($"An error occurred while updating the sale order: {ex.Message}");
        }
    }
}
