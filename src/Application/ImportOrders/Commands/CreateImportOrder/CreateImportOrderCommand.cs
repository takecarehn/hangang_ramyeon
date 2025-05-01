using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;

public record CreateImportOrderCommand : IRequest<Result>
{
    public string InvoiceCode { get; init; } = default!;
    public Guid ShopId { get; init; }
    public DateTime ImportDate { get; init; }
    public decimal Discount { get; init; }
    public string? Note { get; init; }
    public List<ImportOrderDetailDto> Details { get; init; } = new();
}

public record ImportOrderDetailDto
{
    public Guid ProductId { get; init; }
    public string ProductName { get; set; } = default!;
    public int Quantity { get; init; }
    public decimal ImportPrice { get; init; }
}

public class CreateImportOrderCommandHandler : IRequestHandler<CreateImportOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateImportOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateImportOrderCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var importOrder = new ImportOrder
            {
                InvoiceCode = request.InvoiceCode,
                ShopId = request.ShopId,
                ImportDate = request.ImportDate,
                Discount = request.Discount,
                Note = request.Note,
                TotalAmount = request.Details.Sum(d => d.Quantity * d.ImportPrice)
            };

            foreach (var detail in request.Details)
            {
                importOrder.Details.Add(new ImportOrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    ImportPrice = detail.ImportPrice
                });
            }

            _context.ImportOrders.Add(importOrder);
            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Result.Success(importOrder.Id);
        }
        catch (Exception ex)
        {
            // Rollback transaction
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure($"An error occurred while creating the import order: {ex.Message}");
        }
    }
}
