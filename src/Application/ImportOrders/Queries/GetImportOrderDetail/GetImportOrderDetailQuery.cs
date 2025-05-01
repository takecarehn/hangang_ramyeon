using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Models;

public record GetImportOrderDetailQuery(Guid Id) : IRequest<Result>;

public class ImportOrderDto
{
    public Guid Id { get; set; }
    public string InvoiceCode { get; set; } = default!;
    public Guid ShopId { get; set; }
    public string ShopName { get; set; } = default!;
    public DateTime ImportDate { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }
    public List<ImportOrderDetailDto> Details { get; set; } = new();
}

public class GetImportOrderDetailQueryHandler : IRequestHandler<GetImportOrderDetailQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public GetImportOrderDetailQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(GetImportOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var importOrder = await _context.ImportOrders
            .Include(io => io.Shop)
            .Include(io => io.Details)
            .ThenInclude(d => d.Product)
            .FirstOrDefaultAsync(io => io.Id == request.Id, cancellationToken);

        if (importOrder == null)
            return Result.Failure("Import order not found.");

        var dto = new ImportOrderDto
        {
            Id = importOrder.Id,
            InvoiceCode = importOrder.InvoiceCode,
            ShopId = importOrder.ShopId,
            ShopName = importOrder.Shop.Name,
            ImportDate = importOrder.ImportDate,
            Discount = importOrder.Discount,
            TotalAmount = importOrder.TotalAmount,
            Note = importOrder.Note,
            Details = importOrder.Details.Select(d => new ImportOrderDetailDto
            {
                ProductId = d.ProductId,
                ProductName = d.Product.Name,
                Quantity = d.Quantity,
                ImportPrice = d.ImportPrice
            }).ToList()
        };

        return Result.Success(dto);
    }
}
