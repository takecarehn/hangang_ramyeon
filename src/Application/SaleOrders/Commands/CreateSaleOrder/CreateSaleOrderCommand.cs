using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;

public record SaleOrderDetailDto(Guid ProductId, int Quantity, decimal SalePrice);

public record CreateSaleOrderCommand : IRequest<Result>
{
    public Guid ShopId { get; init; }
    public string InvoiceCode { get; init; } = default!;
    public DateTime SaleDate { get; init; }
    public decimal Discount { get; init; }
    public decimal CustomerPaid { get; init; }
    public string? Note { get; init; }
    public List<SaleOrderDetailDto> Details { get; init; } = new();
}

public class CreateSaleOrderCommandHandler : IRequestHandler<CreateSaleOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _currentUser;

    public CreateSaleOrderCommandHandler(IApplicationDbContext context, IUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Result> Handle(CreateSaleOrderCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var userId = _currentUser.Id;
            if (string.IsNullOrEmpty(userId))
            {
                return Result.Failure("Unauthorized: User ID is not available.");
            }

            var saleOrder = new SaleOrder
            {
                InvoiceCode = request.InvoiceCode,
                ShopId = request.ShopId,
                SaleDate = request.SaleDate,
                Discount = request.Discount,
                CustomerPaid = request.CustomerPaid,
                Note = request.Note,
                CreatedBy = userId,
                Created = DateTime.UtcNow,
                TotalAmount = request.Details.Sum(d => d.Quantity * d.SalePrice),
                ChangeAmount = request.CustomerPaid - (request.Details.Sum(d => d.Quantity * d.SalePrice) - request.Discount),
                Details = new List<SaleOrderDetail>()
            };

            foreach (var detail in request.Details)
            {
                var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == detail.ProductId, cancellationToken);
                if (inventory == null)
                    return Result.Failure($"Product with ID {detail.ProductId} not found in inventory.");

                if (inventory.QuantityInStock < detail.Quantity)
                    return Result.Failure($"Not enough inventory for product {detail.ProductId}. Available: {inventory.QuantityInStock}, Requested: {detail.Quantity}");

                inventory.QuantityInStock -= detail.Quantity;

                saleOrder.Details.Add(new SaleOrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    SalePrice = detail.SalePrice
                });
            }

            _context.SaleOrders.Add(saleOrder);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Result.Success(saleOrder.Id);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure($"An error occurred while creating the sale order: {ex.Message}");
        }
    }
}
