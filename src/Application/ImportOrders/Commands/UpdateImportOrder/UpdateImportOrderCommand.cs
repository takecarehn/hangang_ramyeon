using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;
using HangangRamyeon.Domain.Entities;

public record UpdateImportOrderCommand : IRequest<Result>
{
    public Guid Id { get; init; }
    public string InvoiceCode { get; init; } = default!;
    public Guid ShopId { get; init; }
    public DateTime ImportDate { get; init; }
    public decimal Discount { get; init; }
    public string? Note { get; init; }
    public List<ImportOrderDetailDto> Details { get; init; } = new();
}

public class UpdateImportOrderCommandHandler : IRequestHandler<UpdateImportOrderCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateImportOrderCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateImportOrderCommand request, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var importOrder = await _context.ImportOrders
                .Include(io => io.Details)
                .FirstOrDefaultAsync(io => io.Id == request.Id, cancellationToken);

            if (importOrder == null)
                return Result.Failure("Import order not found.");

            importOrder.InvoiceCode = request.InvoiceCode;
            importOrder.ShopId = request.ShopId;
            importOrder.ImportDate = request.ImportDate;
            importOrder.Discount = request.Discount;
            importOrder.Note = request.Note;
            importOrder.TotalAmount = request.Details.Sum(d => d.Quantity * d.ImportPrice);

            // Remove old details and add new ones
            _context.ImportOrderDetails.RemoveRange(importOrder.Details);
            foreach (var detail in request.Details)
            {
                importOrder.Details.Add(new ImportOrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    ImportPrice = detail.ImportPrice
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            // Rollback transaction
            await transaction.RollbackAsync(cancellationToken);
            return Result.Failure($"An error occurred while updating the import order: {ex.Message}");
        }
    }
}
