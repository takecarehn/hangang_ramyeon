using HangangRamyeon.Application.Common.Interfaces;
using HangangRamyeon.Application.Common.Mappings;
using HangangRamyeon.Application.Common.Models;

namespace HangangRamyeon.Application.ImportOrders.Queries.GetImportOrders;

public record GetImportOrdersQuery : IRequest<Result>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public Guid? ShopId { get; init; }
}

public class GetImportOrdersQueryHandler : IRequestHandler<GetImportOrdersQuery, Result>
{
    private readonly IApplicationDbContext _context;

    public GetImportOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(GetImportOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ImportOrders
            .Include(io => io.Shop)
            .AsQueryable();

        if (request.ShopId.HasValue)
        {
            query = query.Where(io => io.ShopId == request.ShopId.Value);
        }

        var importOrders = await query
            .OrderBy(io => io.ImportDate)
            .Select(io => new ImportOrderDto
            {
                Id = io.Id,
                InvoiceCode = io.InvoiceCode,
                ShopId = io.ShopId,
                ShopName = io.Shop.Name,
                ImportDate = io.ImportDate,
                Discount = io.Discount,
                TotalAmount = io.TotalAmount,
                Note = io.Note
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

        return Result.Success(importOrders);
    }
}

